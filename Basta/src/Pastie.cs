using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Basta
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
        public DateTime? Creation { get; set; }

        /// <summary>
        /// Moment in time after which pastie become inaccessible
        /// </summary>
        public DateTime? Expiration { get; set; }

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

        /// <summary>
        /// Checks wherever pastie get expired
        /// </summary>
        /// <returns></returns>
        public bool IsExpired()
        {
            if (this.Expiration <= DateTime.UtcNow)
            {
                return true;
            }

            return false;
        }
    }
}