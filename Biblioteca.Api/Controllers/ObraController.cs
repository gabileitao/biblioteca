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
    public class ObraController : ApiController {

        public ObraService ObraService = new ObraService();

        [HttpGet, Route("obra")]
        public IHttpActionResult GetAll() {
            return Ok(ObraService.FindAll());
        }

        [HttpGet, Route("obra")]
        public IHttpActionResult GetPerTitle([FromUri] string title) {
            return Ok(ObraService.FindPerTitle(title));
        }

        [HttpGet, Route("obra")]
        public IHttpActionResult GetPerId([FromUri] Guid id) {
            return Ok(ObraService.FindPerId(id));
        }

        [HttpPost, Route("obra")]
        public IHttpActionResult PostObra([FromBody] Obra obra) {

            try {
                var erros = ObraService.IsValid(obra);
                if (erros.Length == 0) {
                    if (!ObraService.Exists(obra.Id)) {
                        ObraService.Add(obra);
                        return Ok(obra);
                    } else {
                        return BadRequest("Obra já existe com esse id");
                    }
                } else {
                    return BadRequest(string.Join(", ", erros));
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("obra")]
        public IHttpActionResult PutObra([FromBody] Obra obra) {

            try {

                var erros = ObraService.IsValid(obra);
                if (erros.Length == 0) {
                    if (ObraService.Exists(obra.Id)) {
                        ObraService.Att(obra);
                        return Ok(obra);
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

        [HttpDelete, Route("obra")]
        public IHttpActionResult DeleteObra([FromBody] Obra obra) {
            try {
                if (ObraService.Exists(obra.Id)) {
                    ObraService.Remove(obra);
                    return Ok(obra);
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }
        }

    }
}