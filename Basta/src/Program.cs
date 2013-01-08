using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using Nancy;
using Nancy.Hosting.Wcf;

namespace Basta
{
    class Program : ServiceBase
    {
        public Program()
        {
            this.ServiceName = ConfigurationManager.AppSettings["name"];
        }

        static void Main(string[] args)
        {
            ServiceBase.Run(new Program());
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            if (ServiceHost != null)
            {
                ServiceHost.Close();
            }

            ServiceUri = CreateServiceUri();
            ServiceHost = CreateServiceHost(ServiceUri);
        }

        protected override void OnStop()
        {
            base.OnStop();

            if (ServiceHost != null)
            {
                ServiceHost.Close();
                ServiceHost = null;
            }
        }

        #region Public members
        public Uri ServiceUri
        {
            get;
            private set;
        }

        public WebServiceHost ServiceHost
        {
            get;
            private set;
        }
        #endregion

        #region Private members
        private static Uri CreateServiceUri()
        {
            return new Uri(
                String.Format("http://{0}:{1}/",
                ConfigurationManager.AppSettings["host"],
                ConfigurationManager.AppSettings["port"]));
        }

        private static WebServiceHost CreateServiceHost(Uri uri)
        {
            var host = new WebServiceHost(
                new NancyWcfGenericService(new DefaultNancyBootstrapper()),
                uri);

            var binding = new WebHttpBinding();
            binding.MaxReceivedMessageSize = 2097152;
            binding.MaxBufferSize = 2097152;

            host.AddServiceEndpoint(typeof(NancyWcfGenericService), binding, uri);
            host.Open();

            return host;
        }
        #endregion
    }
}
