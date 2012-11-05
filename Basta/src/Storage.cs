using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Isam.Esent.Collections.Generic;

namespace Basta
{
    public class Storage
    {
        private static PersistentDictionary<string, Pastie> instance;

        public static PersistentDictionary<string, Pastie> Instance
        {
            get
            {
                if (instance == null)
                    Initialize();
                return instance;
            }
        }

        /// <summary>
        /// Initializes instance of PersistentDictionary based on Esent storage
        /// </summary>
        public static void Initialize()
        {
            // TODO: move db folder setting to app.config
            instance = new PersistentDictionary<string, Pastie>("db");
        }

        /// <summary>
        /// Generates unique id based on first five symbol of random GUID
        /// </summary>
        /// <returns></returns>
        public static string GenerateId()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                .ToLower()
                .Replace("l", "")
                .Replace("o", "")
                .Replace("1", "")
                .Replace("0", "")
                .Replace("g", "")
                .Replace("b", "")
                .Replace("9", "")
                .Replace("6", "")
                .Replace("=", "")
                .Replace("+", "")
                .Replace("/", "")
                .Remove(5);
        }
    }
}