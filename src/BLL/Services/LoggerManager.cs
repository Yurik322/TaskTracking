﻿using System;
using System.Collections.Generic;
using System.Text;
using BLL.Interfaces;
using Microsoft.Extensions.Logging;
using NLog;

namespace BLL.Services
{
    /// <summary>
    /// Class for logger manager services.
    /// </summary>
    public class LoggerManager : ILoggerManager
    {
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
