﻿using log4net;
using Notes.Logger;
using Notes.Server.NotesServiceImplementation;
using System;
using System.ServiceModel;
using System.ServiceProcess;

namespace Notes.Server.WCFServer
{
    partial class NotesWCFService : ServiceBase
    {
        internal const string CurrentServiceName = "NotesService";
        internal const string CurrentServiceDisplayName = "Notes Service";
        internal const string CurrentServiceSource = "NotesServiceSource";
        internal const string CurrentServiceLogName = "NotesServiceLogName";
        internal const string CurrentServiceDescription = "Notes for learning purposes.";
        private ServiceHost _serviceHost;
        private readonly ILog _logger;

        public NotesWCFService()
        {
            _logger = LoggerHelper.GetLogger(typeof(NotesWCFService));
            InitializeComponent();
            ServiceName = CurrentServiceName;
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Error($"Unhandled exception, sender: {sender}, exception: {e}");
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
                _logger.Error($"Exception during service hosting: {ex}");
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
                _logger.Error(ex.ToString());
                throw;
            }
        }
    }
}
