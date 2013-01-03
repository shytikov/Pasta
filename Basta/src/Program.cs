using System;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceProcess;
using Nancy;
using Nancy.Hosting.Wcf;

namespace Basta
{
    class Program : ServiceBase
    {
        static void Main(string[] args)
        {
            using (CreateAndOpenWebServiceHost())
            {
                using (Storage.Data)
                {
                    Console.WriteLine("Service is now running on: {0}", BaseUri);
                    Console.ReadLine();
                }
            }
        }

        #region Private members
        private static Uri BaseUri
        {
            get
            {
                return new Uri(
                    String.Format("http://{0}:{1}/",
                    ConfigurationManager.AppSettings["host"],
                    ConfigurationManager.AppSettings["port"]));
            }
        }

        private static WebServiceHost CreateAndOpenWebServiceHost()
        {
            var host = new WebServiceHost(
                new NancyWcfGenericService(new DefaultNancyBootstrapper()),
                BaseUri);

            var binding = new WebHttpBinding();
            binding.MaxReceivedMessageSize = 2097152;
            binding.MaxBufferSize = 2097152;

            host.AddServiceEndpoint(typeof(NancyWcfGenericService), binding, BaseUri);
            host.Open();
            
            return host;
        }
        #endregion
    }
}
