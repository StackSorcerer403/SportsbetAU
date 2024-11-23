using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using H.Socket.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BettingBot.Constants;
using BettingBot.Json;

namespace BettingBot.Controller
{
    public class SocketConnector
    {
        private SocketIoClient _socket = null;
        private onWriteLogEvent m_handlerWriteLog;
        private onWriteStatusEvent m_handlerWriteStatus;
        private onProcNewTipEvent m_handlerProcNewtip;
        private onProcNewTradeTipEvent m_handlerProcNewTradetip;
        
        public onProcUpdateNetworkStatusEvent m_handlerProcUpdateNetworkStatus;

        public List<BetburgerInfo> m_oddsChangedBetList = new List<BetburgerInfo>();

        public List<BetburgerInfo> m_receivedBetList = new List<BetburgerInfo>();

        public List<JsonTrade> m_placedTMBetList = new List<JsonTrade>();
        
        public List<JsonTip> m_placedBetList = new List<JsonTip>();

        private string _KEY = string.Empty;
        private string _IV = string.Empty;


        public SocketConnector(onWriteLogEvent onWriteLog, onWriteStatusEvent onWriteStatus, onProcNewTipEvent onProcNewTipEvent, onProcNewTradeTipEvent onProcNewTradeTipEvent)
        {
            m_handlerWriteLog = onWriteLog;
            m_handlerWriteStatus = onWriteStatus;
            m_handlerProcNewtip = onProcNewTipEvent;
            m_handlerProcNewTradetip = onProcNewTradeTipEvent;
            Init();
        }

        public void CloseSocket()
        {
            try
            {
                _socket.DisconnectAsync();
            }
            catch
            {

            }
        }

        public void Init()
        {
        }

        public bool isBlackList(JsonTip newitem)
        {
            try
            {

                foreach (var item in m_placedBetList)
                {
                    if (item.id == newitem.id)
                        return true;
                }
            }
            catch (Exception)
            {

            }
            return false;
        }

        public bool isBlackList(JsonTrade newitem)
        {
            try
            {

                foreach (var item in m_placedTMBetList)
                {
                    if (item.id == newitem.id)
                        return true;
                }
            }
            catch (Exception)
            {

            }
            return false;
        }

        async public void startListening()
        {
            Setting setting = Setting.instance;
            if (_socket != null)
            {
                await _socket.DisconnectAsync();
                _socket = null;
            }

            _socket = new SocketIoClient();

            _socket.Connected += async (sender, e) =>
            {
                if (GlobalConstants.validationState != ValidationState.SUCCESS)
                {
                    SendPresentInfo(-1);
                }
                Setting.instance.isOnline = true;
                m_handlerProcUpdateNetworkStatus(true);
            };

            _socket.Disconnected += async (sender, e) =>
            {
                try
                {
                    Setting.instance.isOnline = false;
                    m_handlerProcUpdateNetworkStatus(false);
                }
                catch
                {
                }
            };

            _socket.ErrorReceived += async (sender, e) =>
            {
                Setting.instance.isOnline = false;
                m_handlerProcUpdateNetworkStatus(false);
            };



            _socket.On("postLiveBFOdds", (data) =>
            {
                try
                {
                    Setting.instance.EXData = data.ToString();
                    Console.WriteLine("postLiveBFOdds");
                }
                catch
                {
                }
            });
            await _socket.ConnectAsync(new Uri("http://172.86.76.129:8888"));
        }


        public void SendPresentInfo(double curbalance)
        {
        }
    }
}
