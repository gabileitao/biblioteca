using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Biblioteca.Services;

namespace Biblioteca.Api.Controllers {
    public class AutorController : ApiController {

        public AutorService AutorService = new AutorService();

        [HttpGet, Route ("autor")]
        public IHttpActionResult GetAutor() {
            return Ok(AutorService.FindAll());
        }

    }
}