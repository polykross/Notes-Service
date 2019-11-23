using System;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes.Logger;

namespace Notes.IntegrationTests
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void Log4NetFileAppenderTest()
        {
            var logger = LoggerHelper.GetLogger(typeof(LoggerTests));

            logger.Fatal("Fatal");
            logger.Error("Error");
            logger.Warn("Warn");
            logger.Info("Info");
            logger.Debug("Debug");
        }
    }
}
