﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class Pastie
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Pastie()
        {
            this.Id = RefreshId();
        }

        /// <summary>
        /// Unique id of the pastie stored
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Content of the pastie
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Moment in time when the paste was created
        /// </summary>
        public DateTime Creation { get; set; }

        /// <summary>
        /// Moment in time after which pastie become inaccessible
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Generates unique id based on first five symbol of random GUID
        /// </summary>
        /// <returns></returns>
        public string RefreshId()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                .ToLower()
                .Replace("=", "")
                .Replace("+", "")
                .Replace("/", "")
                .Remove(5);
        }
    }
}