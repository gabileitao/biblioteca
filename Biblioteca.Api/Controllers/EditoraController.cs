using System;
using System.Web.Http;
using System.Web.Http.Cors;
using Biblioteca.Domain;
using Biblioteca.Services;

namespace Biblioteca.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class EditoraController : ApiController {

        public EditoraService EditoraService = new EditoraService();

        [HttpGet, Route("editora")]
        public IHttpActionResult GetAll() {
            return Ok(EditoraService.FindAll());
        }

        [HttpGet, Route("editora")]
        public IHttpActionResult GetPerName([FromUri] string name) {
            return Ok(EditoraService.FindPerName(name));
        }

        [HttpGet, Route("editora")]
        public IHttpActionResult GetPerId([FromUri] Guid id) {
            return Ok(EditoraService.FindPerId(id));
        }

        [HttpPost, Route("editora")]
        public IHttpActionResult PostEditora([FromBody] Editora editora) {
            try {

                var erros = EditoraService.IsValid(editora);
                if(erros.Length == 0) {
                    if(!EditoraService.Exists(editora.Id)) {
                        EditoraService.Add(editora);
                        return Ok(editora);
                    } else {
                        return BadRequest("Editora já existe com esse id.");
                    }
                } else {
                    return BadRequest(string.Join(", ", erros));
                }

            } catch (Exception e) {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("editora")]
        public IHttpActionResult PutEditora([FromBody] Editora editora) {
            try {
                var erros = EditoraService.IsValid(editora);
                if (erros.Length == 0) {
                    if (EditoraService.Exists(editora.Id)) {
                        EditoraService.Att(editora);
                        return Ok(editora);
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

        [HttpDelete, Route("editora")]
        public IHttpActionResult DeleteEditora([FromBody] Editora editora) {
            try {
                if (EditoraService.Exists(editora.Id)) {
                    EditoraService.Remove(editora);
                    return Ok(editora);
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }
        }

    }
}