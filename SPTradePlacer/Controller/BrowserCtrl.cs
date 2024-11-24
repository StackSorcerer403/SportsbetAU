using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Web;
using MasterDevs.ChromeDevTools;
using ChromeDevTools.Protocol.Chrome.Emulation;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Runtime;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Page;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Target;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Schema;
using MasterDevs.ChromeDevTools.Protocol.Chrome.Network;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BettingBot.Controller
{
    public class BrowserCtrl
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Windows API for enumerating windows and getting window info
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        // Set the window to TopMost
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static readonly IntPtr HWND_TOP = new IntPtr(0); // HWND_TOP is for showing on top temporarily
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const int SW_SHOW = 5; // To make sure the window is visible

        // Delegate for EnumWindows function
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);


        protected CookieContainer m_cookieContainer;

        IChromeProcess chromeProcess = null;
        IChromeProcess _browserObj = null;
        IChromeSession session = null;
        ChromeSessionFactory _chromeSessionFactory = null;
        object _lockerSession = new object();
        UserAgentMetadata _userAgentMetadata = null;
        List<string> _args = new List<string>()
            {
                //"--headless --disable-gpu",
                "--no-first-run","--disable-default-apps","--no-default-browser-check","--disable-breakpad",
                "--disable-crash-reporter","--no-crash-upload","--deny-permission-prompts",
                "--autoplay-policy=no-user-gesture-required","--disable-prompt-on-repost",
                "--disable-search-geolocation-disclosure","--password-store=basic","--use-mock-keychain",
                "--force-color-profile=srgb","--disable-blink-features=AutomationControlled","--disable-infobars",
                "--disable-session-crashed-bubble","--disable-renderer-backgrounding",
                "--disable-backgrounding-occluded-windows","--disable-background-timer-throttling",
                "--disable-ipc-flooding-protection","--disable-hang-monitor","--disable-background-networking",
                "--metrics-recording-only","--disable-sync","--disable-client-side-phishing-detection",
                "--disable-component-update","--disable-features=TranslateUI,enable-webrtc-hide-local-ips-with-mdns,OptimizationGuideModelDownloading,OptimizationHintsFetching",
                /*"--disable-web-security","--start-maximized"*/
            };


        protected HttpClient m_httpClient = null;
        TelegramBotClient m_botClient = null;
        private DateTime _lastCheckedTime;
        protected List<string> sentPlayerList = new List<string>();
        private static BrowserCtrl _instance = null;
        public double _bettingBalance;

        public BrowserCtrl()
        {
            m_botClient = new TelegramBotClient("704271441:AAG-Z5oqj36ERnEPyKz2ODr5yyQZhy6DlZw");
            m_cookieContainer = new CookieContainer();
            InitHttpClient();
        }

        public static BrowserCtrl instance
        {
            get
            {
                return _instance;
            }
        }

        static public void CreateInstance()
        {
            _instance = new BrowserCtrl();
        }

        protected void InitHttpClient()
        {
            HttpClientHandler handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }; ;
            handler.CookieContainer = m_cookieContainer;
            handler.AllowAutoRedirect = true;
            m_httpClient = new HttpClient(handler);
            m_httpClient.Timeout = new TimeSpan(0, 0, 100);
            ServicePointManager.DefaultConnectionLimit = 2;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            m_httpClient.DefaultRequestHeaders.ExpectContinue = false;
            ChangeDefaultHeaders();
        }
        private string getChromePath()
        {
            string ret = string.Empty;
            try
            {
                string cmd = (string)Registry.ClassesRoot.OpenSubKey(@"ChromeHTML\shell\open\command").GetValue(null);
                ret = Regex.Match(cmd, @"""(.*?)""").Groups[1].Value;
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"{MethodBase.GetCurrentMethod().Name} error - {ex.Message}");
            }
            return ret;
        }
        public void InitializeBrowser(string domain)
        {
            string _chromePath = getChromePath();

            string user_dir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            user_dir = user_dir + "\\Chrome_data\\";


            var chromeProcessFactory = new ChromeProcessFactory(new StubbornDirectoryCleaner(), _chromePath);

            _browserObj = chromeProcessFactory.Create(
                new ChromeBrowserSettings() { Port = 9222, Args = _args.ToArray(), DataDir = user_dir}
                );
            InitializeChromeSession(domain);
        }

        protected void InitializeChromeSession(string domain)
        {
            if (_browserObj is null)
            {
                return;
            }

            var sessionInfo = _browserObj.GetSessionInfo().Result.LastOrDefault(c => c.Type == "page");
            _chromeSessionFactory = new ChromeSessionFactory();

            session = _chromeSessionFactory.Create(sessionInfo.WebSocketDebuggerUrl) as ChromeSession;
            var resultUserAgentBrands = session.SendAsync(new EvaluateCommand() { Expression = "JSON.stringify(window.navigator.userAgentData.brands)" }).Result;

            if (resultUserAgentBrands.Result.Result.Value == null)
            {
                //Пустая страница почему-то
                //NavigateInvoke("chrome://new-tab-page");
                Thread.Sleep(2000);
                resultUserAgentBrands = session.SendAsync(new EvaluateCommand() { Expression = "JSON.stringify(window.navigator.userAgentData.brands)" }).Result;
            }

            _userAgentMetadata = new UserAgentMetadata()
            {
                Platform = "Windows",
                PlatformVersion = "",
                Architecture = "",
                Model = "",
                Mobile = false
            };

            InitSession(ref session, "about:blank");
            session.SendAsync(new NavigateCommand
            {
                Url = domain
            }).Wait();


        }


        // EnumWindows function to find the window by process ID
        public static IntPtr FindWindowByProcessId(int processId)
        {
            IntPtr foundWindow = IntPtr.Zero;

            EnumWindows((hWnd, lParam) =>
            {
                // Get the process ID associated with the window handle
                GetWindowThreadProcessId(hWnd, out uint windowProcessId);

                // Compare it with the target process ID
                if (windowProcessId == processId)
                {
                    StringBuilder windowText = new StringBuilder(256);
                    GetWindowText(hWnd, windowText, 256);
                    Console.WriteLine($"Found window: {windowText} (Process ID: {windowProcessId})");

                    foundWindow = hWnd;
                    return false; // Stop enumerating
                }

                return true; // Continue enumerating
            }, IntPtr.Zero);

            return foundWindow;
        }

        public void ExitSession()
        {
            try
            {
                lock (_lockerSession)
                {
                    var allSessions = _browserObj.GetSessionInfo().Result;
                    foreach (var session in allSessions)
                    {
                        // Close all other sessions
                        this.session.SendAsync(new CloseTargetCommand() { TargetId = session.Id }).Wait();
                    }
                }
            }
            catch
            {

            }
        }
        private void InitSession(ref IChromeSession chromeSession, string url)
        {
            lock (_lockerSession)
            {
                var targetInfo = chromeSession.SendAsync(new CreateTargetCommand() { Url = url }).Result;

                var allSessions = _browserObj.GetSessionInfo().Result;
                foreach (var session in allSessions)
                {
                    // Close all other sessions
                    if (session.Id != targetInfo.Result.TargetId)
                    {
                        //chromeSession.SendAsync(new CloseTargetCommand() { TargetId = session.Id }).Wait();
                    }
                    else
                    {

                        chromeSession = _chromeSessionFactory.Create(session.WebSocketDebuggerUrl) as ChromeSession;

                        var domEnableResult = chromeSession.SendAsync<MasterDevs.ChromeDevTools.Protocol.Chrome.DOM.EnableCommand>().Result;
                        var networkEnableResult = chromeSession.SendAsync<MasterDevs.ChromeDevTools.Protocol.Chrome.Network.EnableCommand>().Result;
                        var pageEnableResult = chromeSession.SendAsync<MasterDevs.ChromeDevTools.Protocol.Chrome.Page.EnableCommand>().Result;

                        chromeSession.Subscribe<MasterDevs.ChromeDevTools.Protocol.Chrome.Network.WebSocketFrameReceivedEvent>(e =>
                        {
                        });

                        chromeSession.Subscribe<MasterDevs.ChromeDevTools.Protocol.Chrome.Network.WebSocketFrameSentEvent>(e =>
                        {
                            //LogMng.instance.PrintLog("WebSocket Frame Sent: " + e.Response.PayloadData);
                        });

                        chromeSession.Subscribe<RequestWillBeSentEvent>(sendedRequest =>
                        {

                        });

                        var targets = chromeSession.SendAsync(new SetDiscoverTargetsCommand() { Discover = true }).Result;

                        //finish page load
                        chromeSession.Subscribe<LoadEventFiredEvent>(loadEvent =>
                        {
                            // we cannot block in event handler, hence the task
                            Task.Run(async () =>
                            {
                                Console.WriteLine("LoadEventFiredEvent: " + loadEvent.Timestamp);
                                Console.WriteLine("Page Loaded");
                            });
                        });

                        chromeSession.Subscribe<ResponseReceivedEvent>(e =>
                        {
                            Task.Run(async () =>
                            {
                                var resp_url = e.Response.Url;
                                bool isFiltered = false;
                                if (resp_url.Contains("https://wapi.winamax.es") 
                                //|| resp_url.Contains("https://lmt.fn.sportradar.com")
                                || resp_url.Contains("https://www.winamax.es"))
                                {
                                    if (
                                        resp_url.Contains("/authentication/token/authorize") 
                                        //|| resp_url.Contains("/match_bookmakerodds/")
                                        || resp_url.Contains("betting/validate_betslip.php")
                                    )
                                    {
                                        isFiltered = true;
                                    }
                                }

                                if (isFiltered)
                                {
                                    try
                                    {
                                        var result = (await this.session.SendAsync(new GetResponseBodyCommand() { RequestId = e.RequestId })).Result;
                                        if(result != null)
                                        {
                                            string responseBody = result.Body;
                                            //LogMng.instance.PrintLog(resp_url);
                                            //LogMng.instance.PrintLog(responseBody);
                                            if (resp_url.Contains("betting/validate_betslip.php"))
                                            {
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    
                                }
                                
                            });
                        });
                        chromeSession.Subscribe<FrameStartedLoadingEvent>(frameStarted =>
                        {

                        });

                        chromeSession.Subscribe<FrameResizedEvent>(e =>
                        {
                            Task.Run(async () =>
                            {
                                Console.WriteLine("FrameResizedEvent: ");
                                Console.WriteLine("Page Loaded");
                            });
                        });
                        //can be FrameStoppedLoadingEvent or LoadEventFiredEvent
                        chromeSession.Subscribe<FrameStoppedLoadingEvent>(frameStopped =>
                        {

                        });

    


                        chromeSession.Subscribe<FrameNavigatedEvent>(frameNavigated =>
                        {
                            try
                            {

                            }
                            catch
                            {

                            }
                        });
                        chromeSession.Subscribe<ExecutionContextCreatedEvent>(executionContext =>
                        {
                            try
                            {
                                Task.Run(async () =>
                                {
                                    var auxData = executionContext.Context.AuxData as JObject;
                                    var frameId = auxData["frameId"].Value<string>();
                                });
                            }
                            catch
                            {
                            }
                        });

                        chromeSession.Subscribe<ExecutionContextDestroyedEvent>(contextDestroyed =>
                        {
                            try
                            {

                            }
                            catch
                            {
                            }
                        });

                        chromeSession.Subscribe<FrameDetachedEvent>(frameDetached =>
                        {

                        });
                    }
                }
            }
        }
        public int StartWorker()
        {
            try
            {

            }
            catch
            {

            }
            return 0;
        }
        public string ExecuteScript(string jsCode, bool requiredResult = false)
        {
            string result = string.Empty;
            try
            {
                if (!requiredResult)
                    session.SendAsync(new EvaluateCommand() { Expression = jsCode }).Wait();
                else
                {
                    var script = session.SendAsync(new EvaluateCommand() { Expression = jsCode }).Result.Result;
                    if (script.Result.Value == null)
                        return result;

                    result = script.Result.Value.ToString();
                }

            }
            catch (Exception ex)
            {
                LogMng.instance.PrintLog(ex.ToString());
            }
            return result;
        }
        
        public Bitmap GetScreenShot()
        {
            Bitmap bitmap = null;
            try
            {
                var screenshotRequest = new CaptureScreenshotCommand();
                var screenshotResponse = session.SendAsync(screenshotRequest).Result.Result;
                var screenshotBytes = Convert.FromBase64String(screenshotResponse.Data);
                var ms = new MemoryStream(screenshotBytes);
                bitmap = new Bitmap(ms);
                bitmap.Save("output.png", System.Drawing.Imaging.ImageFormat.Png);
                Console.WriteLine("Bitmap saved as output.png");
            }
            catch
            {

            }
            return bitmap;
        }
    
        public static void CloseOldBrowser()
        {
            int port = 9222; // 

            try
            {
                var processId = GetProcessIdUsingPort(port);

                if (processId != -1)
                {
                    KillProcessById(processId);
                    Console.WriteLine($"Process using port {port} (PID: {processId}) has been terminated.");
                }
                else
                {
                    Console.WriteLine($"No process is using port {port}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public bool DoOpenPage(string domain)
        {
            try
            {
                InitializeBrowser(domain);
                return true;
            }
            catch (Exception ex)
            {
                LogMng.instance.PrintLog("Exception in doLogin: " + ex.ToString());
            }
            return false;
        }

        protected virtual void ChangeDefaultHeaders()
        {
            m_httpClient.DefaultRequestHeaders.Clear();
            Setting.instance.loadSettingInfo();
            //m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", Setting.instance.userToken);
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
            m_httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Connection", "keep-alive");
        }

        static int GetProcessIdUsingPort(int port)
        {
            // netstat
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "netstat";
                process.StartInfo.Arguments = "-ano";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                // netstat
                foreach (string line in output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (line.Contains($":{port}"))
                    {
                        string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length > 4 && int.TryParse(parts[parts.Length - 1], out int pid))
                        {
                            return pid;
                        }
                    }
                }
            }

            return -1; 
        }

        static void KillProcessById(int processId)
        {
            Process process = Process.GetProcessById(processId);
            process.Kill();
        }

        public void SetTopMost()
        {
            try
            {
                long processId = ((LocalChromeProcess)_browserObj).processId;
                IntPtr chromeWindowHandle = FindWindowByProcessId((int)processId);
                if (chromeWindowHandle != IntPtr.Zero)
                {
                    Console.WriteLine("Chrome window found, setting it to TopMost...");
                    ShowWindow(chromeWindowHandle, SW_SHOW);
                    SetForegroundWindow(chromeWindowHandle);  // Brings the window to the foreground

                    // Step 4: Set the window to TopMost (using the SetWindowPos API)
                    SetWindowPos(chromeWindowHandle, HWND_TOP, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE);
                }
                else
                {
                    Console.WriteLine("Chrome window not found.");
                }
            }
            catch
            {
            }
        }
    }
}
