using Arvato.IQ.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Arvato.IQ.Api.Controllers
{
    public class HomeController : ApiController
    {

        private ResourcesFactory resourceFactory;

        protected ResourcesFactory ResourceFactory {
            get {
                if (resourceFactory == null)
                {
                    resourceFactory = new ResourcesFactory(Request);
                }
                return resourceFactory;
            }
        }

        public HomeController()
        {

        }

        public IHttpActionResult Get()
        {
            return Ok(ResourceFactory.CreateHomeResource("Arvato Stories Api", "Full Service & Api Description Here sure"));
        }
    }
}
