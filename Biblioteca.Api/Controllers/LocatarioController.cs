using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Biblioteca.Domain;
using Biblioteca.Domain.Models;
using Biblioteca.Services;

namespace Biblioteca.Api.Controllers {
    [EnableCors("*", "*", "*")]
    public class LocatarioController : ApiController {

        public LocatarioService LocatarioService = new LocatarioService();

        [HttpGet, Route("locatario")]
        public IHttpActionResult GetAll() {
            return Ok(LocatarioService.FindAll());
        }

        [HttpGet, Route("locatario")]
        public IHttpActionResult GetPerName([FromUri] string name) {
            return Ok(LocatarioService.FindPerName(name));
        }

        [HttpGet, Route("locatario")]
        public IHttpActionResult GetPerId([FromUri] Guid id) {
            return Ok(LocatarioService.FindPerId(id));
        }

        [HttpPost, Route("locatario")]
        public IHttpActionResult PostObra([FromBody] Locatario locatario) {

            try {
                var erros = LocatarioService.IsValid(locatario);
                if (erros.Length == 0) {
                    if (!LocatarioService.Exists(locatario.Id)) {
                        LocatarioService.Add(locatario);
                        return Ok(locatario);
                    } else {
                        return BadRequest("Locatario já existe com esse id");
                    }
                } else {
                    return BadRequest(string.Join(", ", erros));
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("locatario")]
        public IHttpActionResult PutObra([FromBody] Locatario locatario) {

            try {

                var erros = LocatarioService.IsValid(locatario);
                if (erros.Length == 0) {
                    if (LocatarioService.Exists(locatario.Id)) {
                        LocatarioService.Att(locatario);
                        return Ok(locatario);
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

        [HttpDelete, Route("locatario")]
        public IHttpActionResult DeleteObra([FromBody] Locatario locatario) {
            try {
                if (LocatarioService.Exists(locatario.Id)) {
                    LocatarioService.Remove(locatario);
                    return Ok(locatario);
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }
        }

    }
}