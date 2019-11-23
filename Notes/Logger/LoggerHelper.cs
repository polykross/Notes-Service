using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Layout;
using System;

namespace Notes.Logger
{
    public class LoggerHelper
    {
        #region Fields
        private static RollingFileAppender _rollingFileAppender;
        private const string Layout = "%date{dd MMM yyyy HH:mm:ss} [%level] [%class] [%method] -> %message%newline";
        #endregion

        public LoggerHelper()
        {
        }

        #region API
        public static ILog GetLogger(Type type)
        {
            if (_rollingFileAppender == null)
            {
                _rollingFileAppender = GetFileAppender();
            }

            BasicConfigurator.Configure(_rollingFileAppender);

            return LogManager.GetLogger(type);
        }
        #endregion

        #region Util
        private static PatternLayout GetPatternLayout()
        {
            var patternLayout = new PatternLayout
            {
                ConversionPattern = Layout
            };
            patternLayout.ActivateOptions();
            return patternLayout;
        }

        private static RollingFileAppender GetFileAppender()
        {
            var fileAppender = new RollingFileAppender
            {
                Name = "FileAppender",
                AppendToFile = true,
                File = "NotesLog4Net.log",
                Layout = GetPatternLayout(),
                Threshold = Level.All,
                MaximumFileSize = "10MB",
                MaxSizeRollBackups = 100
            };
            fileAppender.ActivateOptions();
            return fileAppender;
        }
        #endregion
    }
}
