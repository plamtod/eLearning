using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Learning.Web.Controllers
{


    public class BaseApiController : ApiController
    {
        private ModelFactory modelFactory;
        
        protected ModelFactory ModelFactory { 
            get {
                if (modelFactory == null)
                {
                    return modelFactory = new ModelFactory(Request);
                }

                return modelFactory;
            } 
            
        }
       
        public BaseApiController()
        {
           
        }

    }
}
