using System;
using System.Web.Http;
using System.Web.Http.Cors;
using Biblioteca.Domain.Models;
using Biblioteca.Services;

namespace Biblioteca.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class AutorController : ApiController {

        public AutorService AutorService = new AutorService();

        [HttpGet, Route("autor")]
        public IHttpActionResult GetAuthor() {
            return Ok(AutorService.FindAll());
        }

        [HttpGet, Route("autor")]
        public IHttpActionResult GetAuthorPerName([FromUri] string name) {
            return Ok(AutorService.FindPerName(name));
        }

        [HttpGet, Route("autor")]
        public IHttpActionResult GetPerId(Guid id) {
            return Ok(AutorService.FindPerId(id));
        }

        [HttpPost, Route("autor")]
        public IHttpActionResult PostAuthor([FromBody] Autor autor) {

            try {
                var erros = AutorService.IsValidAuthor(autor);
                if (erros.Length == 0) {
                    if (!AutorService.Exists(autor.Id)) {
                        AutorService.Add(autor);
                        return Ok(autor);
                    } else {
                        return BadRequest("Autor já existe com esse id");
                    }
                } else {
                    return BadRequest(string.Join(", ", erros));
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }

        }

        [HttpPut, Route("autor")]
        public IHttpActionResult PutAuthor([FromBody] Autor autor) {

            try {
                var erros = AutorService.IsValidAuthor(autor);
                if (erros.Length == 0) {
                    if (AutorService.Exists(autor.Id)) {
                        AutorService.Att(autor);
                        return Ok(autor);
                    } else {
                        return NotFound();
                    }
                } else {
                    return BadRequest(string.Join(", ", erros));
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }

        }

        [HttpDelete, Route("autor")]
        public IHttpActionResult DeleteAuthor([FromUri] Guid id) {

            try {
                if (AutorService.Exists(id)) {
                    var success = AutorService.Delete(id);
                    return Ok(success);
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }

        }
    }
}