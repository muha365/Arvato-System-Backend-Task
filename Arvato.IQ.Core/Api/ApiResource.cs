using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Core.Api
{
    /// <summary>
    /// Represent abstract class for linked resources
    /// </summary>
    public abstract class ApiResource
    {

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApiResource()
        {
            Links = new List<ResourceLink>();
        }

        /// <summary>
        /// Links for the resource available operations and actions
        /// </summary>
        public List<ResourceLink> Links { get; set; }


    }
}