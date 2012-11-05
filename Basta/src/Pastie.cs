using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Basta
{
    [Serializable]
    public struct Pastie
    {
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

        /*
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
        */
    }
}