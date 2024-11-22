using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingBot.Controller
{
    public class LogMng
    {
        protected onWriteStatusEvent m_handlerWriteStatus;
        protected onWriteLogEvent m_handlerWriteLog;
        private static LogMng _instance = null;
        public static LogMng instance
        {
            get
            {
                return _instance;
            }
        }

        public onWriteStatusEvent PrintLog
        {
            get
            {
                return m_handlerWriteStatus;
            }
        }
        public onWriteLogEvent RecordLog
        {
            get
            {
                return m_handlerWriteLog;
            }
        }

        public LogMng(onWriteStatusEvent onWriteStatus, onWriteLogEvent onWriteLog) 
        {
            m_handlerWriteStatus = onWriteStatus;
            m_handlerWriteLog = onWriteLog;
        }
        static public void CreateInstance(onWriteStatusEvent onWriteStatus, onWriteLogEvent onWriteLog)
        {
            _instance = new LogMng(onWriteStatus, onWriteLog);
        }
    }
}
