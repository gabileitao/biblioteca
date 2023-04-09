using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Biblioteca.Domain.Models;
using Biblioteca.Services;

namespace Biblioteca.Api.Controllers {
    [EnableCors("*", "*", "*")]
    public class AutorController : ApiController {

        public AutorService AutorService = new AutorService();

        [HttpGet, Route("autor")]
        public IHttpActionResult GetAuthor() {
            return Ok(AutorService.FindAll());
        }

        [HttpGet, Route("autor")]
        public IHttpActionResult GetAuthor([FromUri] string name) {
            return Ok(AutorService.FindPerName(name));
        }

        [HttpPost, Route("autor")]
        public IHttpActionResult PostAuthor([FromBody] Autor autor) {
            try {
                var erros = AutorService.IsValidAuthor(autor);
                if (erros.Length == 0) {
                    Autor autorResposta = AutorService.AddAuthor(autor);
                    return Ok(autorResposta);
                } else { 
                    return BadRequest(string.Join(", ", erros));
                }

            } catch(Exception e) {
                return InternalServerError(e);
            }

        }
    }
}