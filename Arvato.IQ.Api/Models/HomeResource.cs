using Arvato.IQ.Core.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arvato.IQ.Api.Models
{

    /// <summary>
    /// Api Introductory to the clients
    /// </summary>
    public class HomeResource : ApiResource
    {
        public HomeResource() : base()
        {
        }

        /// <summary>
        /// Service Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Service Description
        /// </summary>
        public string Description {
            get; set;

        }
    }
}