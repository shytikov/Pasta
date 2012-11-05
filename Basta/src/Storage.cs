using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Isam.Esent.Collections.Generic;
using Nancy.Json;

namespace Basta
{
    public class Storage
    {
        private static PersistentDictionary<string, string> data;
        private static JavaScriptSerializer worker;

        public static PersistentDictionary<string, string> Data
        {
            get
            {
                if (data == null)
                {
                    InitializeData();
                }
                return data;
            }
        }

        public static JavaScriptSerializer Worker
        {
            get
            {
                if (worker == null)
                {
                    InitializeWorker();
                }
                return worker;
            }
        }

        /// <summary>
        /// Initializes instance of PersistentDictionary based on Esent storage
        /// </summary>
        private static void InitializeData()
        {
            // TODO: move db folder setting to app.config
            data = new PersistentDictionary<string, string>("db");
        }

        /// <summary>
        /// Initializes instance of worker to serialize / deserialize objects from / to JSON
        /// </summary>
        private static void InitializeWorker()
        {
            worker = new JavaScriptSerializer();
        }
    }
}