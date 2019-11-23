using Notes.Server.NotesServiceImplementation;
using System;
using System.ServiceModel;
using System.ServiceProcess;
using log4net;
using log4net.Config;

namespace Notes.Server.WCFServer
{
    partial class NotesWCFService : ServiceBase
    {
        internal const string CurrentServiceName = "NotesService";
        internal const string CurrentServiceDisplayName = "Notes Service";
        internal const string CurrentServiceSource = "NotesServiceSource";
        internal const string CurrentServiceLogName = "NotesServiceLogName";
        internal const string CurrentServiceDescription = "Notes for learning purposes.";
        private ServiceHost _serviceHost = null;

        public NotesWCFService()
        {
            InitializeComponent();
            ServiceName = CurrentServiceName;
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //TODO implement Logging
        }

        protected override void OnStart(string[] args)
        {
#if DEBUG
            RequestAdditionalTime(120 * 1000);
            //for (int i = 0; i < 100; i++)
            //{
            //    Thread.Sleep(1000);
            //}
#endif
            _serviceHost?.Close();
            try
            {
                _serviceHost = new ServiceHost(typeof(NotesService));
                _serviceHost.Open();
            }
            catch (Exception ex)
            {
                //TODO implement Logging
                throw;
            }
        }

        protected override void OnStop()
        {
            RequestAdditionalTime(120 * 1000);
            try
            {
                _serviceHost.Close();
            }
            catch (Exception ex)
            {
                //TODO add Logging
            }
        }
    }
}
