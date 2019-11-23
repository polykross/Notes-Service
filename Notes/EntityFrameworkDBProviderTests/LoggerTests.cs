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
        public void Log4NetTest()
        {
            var patternLayout = new PatternLayout
            {
                ConversionPattern = "%date{dd MMM yyyy HH:mm:ss} | %class | [%level]: %message%newline"
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

            ILog logger = LogManager.GetLogger(typeof(LoggerTests));

            logger.Fatal("Fatal");
            logger.Error("Error");
            logger.Warn("Warn");
            logger.Info("Info");
            logger.Debug("Debug");
        }
    }
}
