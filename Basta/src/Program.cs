using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Nancy;
using Nancy.Hosting.Wcf;

namespace Basta
{
    class Program
    {
        // TODO: move address setting to app.config

        protected static Uri BaseUri
        {
            get
            {
                return new Uri(
                    String.Format("http://{0}:{1}/", 
                    ConfigurationManager.AppSettings["host"], 
                    ConfigurationManager.AppSettings["port"]));
            }
        }

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

        private static WebServiceHost CreateAndOpenWebServiceHost()
        {
            var host = new WebServiceHost(
                new NancyWcfGenericService(new DefaultNancyBootstrapper()),
                BaseUri);

            var binding = new WebHttpBinding();
            binding.MaxReceivedMessageSize = 2147483647;
            binding.MaxBufferSize = 2147483647;

            host.AddServiceEndpoint(typeof(NancyWcfGenericService), binding, "");
            host.Open();
            
            return host;
        }
    }
}
