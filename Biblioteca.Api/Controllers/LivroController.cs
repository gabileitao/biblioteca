using System;
using System.Web.Http;
using System.Web.Http.Cors;
using Biblioteca.Domain;
using Biblioteca.Services;

namespace Biblioteca.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class LivroController : ApiController {

        public LivroService LivroService = new LivroService();

        [HttpGet, Route("livro")]
        public IHttpActionResult GetAll() {
            return Ok(LivroService.FindAll());
        }

        [HttpGet, Route("livro")]
        public IHttpActionResult GetPerBarcode([FromUri] string barcode) {
            return Ok(LivroService.FindPerBarcode(barcode));
        }

        [HttpGet, Route("livro")]
        public IHttpActionResult GetPerId([FromUri] Guid id) {
            return Ok(LivroService.FindPerId(id));
        }

        [HttpPost, Route("livro")]
        public IHttpActionResult PostLivro([FromBody] Livro livro) {

            try {
                var erros = LivroService.IsValid(livro);
                if (erros.Length == 0) {
                    if (!LivroService.Exists(livro.Id)) {
                        if (!LivroService.Exists(livro.Barcode)) {
                            LivroService.Add(livro);
                            return Ok(livro);
                        } else {
                            return BadRequest("Este código de barras já existe");
                        }
                    } else {
                        return BadRequest("Esta copia já existe com esse id");
                    }
                } else {
                    return BadRequest(string.Join(", ", erros));
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }
        }

        [HttpPut, Route("livro")]
        public IHttpActionResult PutLivro([FromBody] Livro livro) {

            try {

                var erros = LivroService.IsValid(livro);
                if (erros.Length == 0) {
                    if (LivroService.Exists(livro.Id)) {
                        LivroService.Att(livro);
                        return Ok(livro);
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

        [HttpDelete, Route("livro")]
        public IHttpActionResult DeleteLivro([FromBody] Livro livro) {
            try {
                if (LivroService.Exists(livro.Id)) {
                    LivroService.Remove(livro);
                    return Ok(livro);
                } else {
                    return NotFound();
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }
        }

    }
}