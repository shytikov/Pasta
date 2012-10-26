using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;

namespace Basta.Warehouse
{
    interface IDataAccess
    {
        IDocumentSession DocumentSession { get; set; }
    }
}
