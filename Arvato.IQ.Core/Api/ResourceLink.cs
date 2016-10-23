using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Core.Api
{

    /// <summary>
    /// class represent resource link 
    /// </summary>
    public class ResourceLink
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ResourceLink()
        {
        }

        /// <summary>
        /// constructor 
        /// </summary>
        /// <param name="rel">the relation between the resource and href</param>
        /// <param name="href">hypertext reference</param>
        /// <param name="method">the http method </param>
        public ResourceLink(string rel, string href, string method)
        {
            //validate rel parameter
            if (rel == null)
            {
                throw new ArgumentNullException( nameof(rel),"{0} parameter can't be null");
            }
            if (string.Empty == rel)
            {
                throw new ArgumentException("{0} parameter can't be empty string", nameof(rel));
            }
            this.Rel = rel;

            // validate url parameter
            if (href == null)
            {
                throw new ArgumentNullException(nameof(href),"{0} parameter can't be null");
            }
            if (string.Empty == href)
            {
                throw new ArgumentException("{0} parameter can't be empty string", nameof(href));
            }
            this.Href = href;

            // validate method parameter
            if (method == null)
            {
                throw new ArgumentNullException(nameof(method), "{0} parameter can't be null");
            }
            if (string.Empty == method)
            {
                throw new ArgumentException("{0} parameter can't be empty string", nameof(method));
            }
            this.Method = method;
        }

        /// <summary>
        /// Relation between the resource and href
        /// </summary>
        public string Rel { get; set; }

        /// <summary>
        /// Hypertext reference
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Http method 
        /// </summary>
        public String Method { get; set; }
    }
}
