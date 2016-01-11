using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web;

namespace DATEXIIToolkit.Common
{
    enum LOGGING_LEVEL {
        NO_LOGGING = 0,
        ERROR = 1,
        WARNING = 2,
        INFO = 3,
        DEBUG = 4,
        TRACE = 5
    }

    public class LogWriter
    {
        private static LogWriter instance;

        private static string LOG_FILE = "\\datexiiToolkit";
        private static string LOG_FILE_EXT = ".log";
        private static Queue<string> logQueue;
        private static Timer logTimer;
        private int LOGGING_TIMER_PERIOD = 1000;

        private LogWriter()
        {
            logQueue = new Queue<string>();
            logTimer = new System.Timers.Timer();
            logTimer.Elapsed += new ElapsedEventHandler(LogEvent);
            logTimer.Interval = LOGGING_TIMER_PERIOD;
            logTimer.AutoReset = false;
            logTimer.Enabled = true;
        }

        public static LogWriter GetInstance()
        {
            if (instance == null)
            {
                instance = new LogWriter();
            }
            return instance;
        }

        public void stopLogging()
        {
            logTimer.Stop();
        }

        private static void LogEvent(object source, ElapsedEventArgs e)
        {
            string logLine;
            lock (logQueue)
            {
                if (logQueue.Count > 0)
                {
                    logLine = logQueue.Dequeue();
                }
                else
                {
                    logLine = null;
                }
            }

            FileStream logFileStream = null;

            while (logLine != null)
            {

                if (logFileStream == null)
                {
                    string fileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + LOG_FILE + DateTime.Now.ToString("yyyy-MM-dd") + LOG_FILE_EXT;
                    if (File.Exists(fileName)) {
                        logFileStream = File.Open(fileName, FileMode.Append);
                    } else {
                        logFileStream = File.OpenWrite(fileName);
                    }
                }
                logLine = logLine + " \r\n";
                logFileStream.Write(ASCIIEncoding.ASCII.GetBytes(logLine), 0, logLine.Length);

                lock (logQueue)
                {
                    if (logQueue.Count > 0)
                    {
                        logLine = logQueue.Dequeue();
                    }
                    else {
                        logLine = null;
                    }
                }
            }

            if (logFileStream != null)
            {
                logFileStream.Flush();
                logFileStream.Close();
            }

            logTimer.Enabled = true;
        }

        public void QueueLog(string level, string logText)
        {
            string logLine = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "," + level + "," + logText;
            lock (logQueue)
            {
                logQueue.Enqueue(logLine);
            }
        }
    }

    public class LogWrapper
    {
        private LogWriter logWriter;
        private LOGGING_LEVEL logLevel;
        private string className;

        public LogWrapper(string className)
        {
            logLevel = (LOGGING_LEVEL)Int32.Parse(ConfigurationManager.AppSettings["logLevel"]);
            logWriter = LogWriter.GetInstance();
            this.className = className;
        }

        public void Error(string logText)
        {
            if (logLevel >= LOGGING_LEVEL.ERROR)
            {
                logWriter.QueueLog("ERROR", className + " " + logText);
            }
        }

        public void Warning(string logText)
        {
            if (logLevel >= LOGGING_LEVEL.WARNING)
            {
                logWriter.QueueLog("WARNING", className + " " + logText);
            }
        }

        public void Info(string logText)
        {
            if (logLevel >= LOGGING_LEVEL.INFO)
            {
                logWriter.QueueLog("INFO", className + " " + logText);
            }
        }

        public void Debug(string logText)
        {
            if (logLevel >= LOGGING_LEVEL.DEBUG)
            {
                logWriter.QueueLog("DEBUG", className + " " + logText);
            }
        }

        public void Trace(string logText)
        {
            if (logLevel >= LOGGING_LEVEL.TRACE)
            {
                logWriter.QueueLog("TRACE", className + " " + logText);
            }
        }

        public bool isInfo()
        {
            if (logLevel >= LOGGING_LEVEL.INFO)
            {
                return true;
            }
            return false;
        }

        public bool isDebug()
        {
            if (logLevel >= LOGGING_LEVEL.DEBUG)
            {
                return true;
            }
            return false;
        }

        public bool isTrace()
        {
            if (logLevel >= LOGGING_LEVEL.TRACE)
            {
                return true;
            }
            return false;
        }
    }
}