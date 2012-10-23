﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Basta.Models
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

    public class Statistic
    {
        /// <summary>
        /// Total number of pasties in the system
        /// </summary>
        public int Total { get; private set; }

        /// <summary>
        /// Number of pasties posted today
        /// </summary>
        public int Today { get; private set; }
    }
}