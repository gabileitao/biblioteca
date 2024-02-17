using System;
using System.Web.Http;
using System.Web.Http.Cors;
using Biblioteca.Domain;
using Biblioteca.Services;

namespace Biblioteca.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UsuarioController : ApiController {

        public UsuarioService UsuarioService = new UsuarioService();

        [HttpGet, Route("usuario")]
        public IHttpActionResult GetAll() {
            return Ok(UsuarioService.FindAll());
        }

        [HttpGet, Route("usuario")]
        public IHttpActionResult GetPerName([FromUri] string name) {
            return Ok(UsuarioService.FindPerName(name));
        }

        [HttpGet, Route("usuario")]
        public IHttpActionResult GetPerId([FromUri] Guid id) {
            return Ok(UsuarioService.FindPerId(id));
        }

        [HttpPost, Route("usuario")]
        public IHttpActionResult PostObra([FromBody] Usuario usuario) {

            try {
                var erros = UsuarioService.IsValid(usuario);
                if (erros.Length == 0) {
                    if (!UsuarioService.Exists(usuario.Id)) {
                        UsuarioService.Add(usuario);
                        return Ok(usuario);
                    } else {
                        return BadRequest("Usuario já existe com esse id");
                    }
                } else {
                    return BadRequest(string.Join(", ", erros));
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("usuario")]
        public IHttpActionResult PutObra([FromBody] Usuario usuario) {

            try {

                var erros = UsuarioService.IsValid(usuario);
                if (erros.Length == 0) {
                    if (UsuarioService.Exists(usuario.Id)) {
                        UsuarioService.Att(usuario);
                        return Ok(usuario);
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

        [HttpDelete, Route("usuario")]
        public IHttpActionResult DeleteObra([FromBody] Usuario usuario) {
            try {
                if (UsuarioService.Exists(usuario.Id)) {
                    UsuarioService.Remove(usuario);
                    return Ok(usuario);
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }
        }

    }
}