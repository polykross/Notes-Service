using System;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Notes.IntegrationTests
{
    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void Log4NetConsoleAppenderTest()
        {
            var patternLayout = new PatternLayout
            {
                ConversionPattern = "%date{dd MMM yyyy HH:mm:ss} [%level] [%class] [%method] -> %message%newline"
            };
            patternLayout.ActivateOptions();

            var consoleAppender = new ConsoleAppender()
            {
                Name = "ConsoleAppender",
                Layout = patternLayout,
                Threshold = Level.All
            };

            consoleAppender.ActivateOptions();
            BasicConfigurator.Configure(consoleAppender);

            var logger = LogManager.GetLogger(typeof(LoggerTests));

            logger.Fatal("Fatal");
            logger.Error("Error");
            logger.Warn("Warn");
            logger.Info("Info");
            logger.Debug("Debug");
        }
    
        [TestMethod]
        public void Log4NetFileAppenderTest()
        {
            var patternLayout = new PatternLayout
            {
                ConversionPattern = "%date{dd MMM yyyy HH:mm:ss} [%level] [%class] [%method] -> %message%newline"
            };
            patternLayout.ActivateOptions();

            var fileAppender = new RollingFileAppender()
            {
                Name = "FileAppender",
                Layout = patternLayout,
                Threshold = Level.All,
                AppendToFile = true,
                File = "NotesLog4Net.log",
                MaximumFileSize = "10MB",
                MaxSizeRollBackups = 100
            };

            fileAppender.ActivateOptions();
            BasicConfigurator.Configure(fileAppender);

            var logger = LogManager.GetLogger(typeof(LoggerTests));

            logger.Fatal("Fatal");
            logger.Error("Error");
            logger.Warn("Warn");
            logger.Info("Info");
            logger.Debug("Debug");
        }
    }
}
