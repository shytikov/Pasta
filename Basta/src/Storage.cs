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

        public static void Initialize()
        {
            instance = new PersistentDictionary<string, Pastie>("db");
        }
    }
}