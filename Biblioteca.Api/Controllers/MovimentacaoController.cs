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
    public class MovimentacaoController : ApiController {

        public MovimentacaoService MovimentacaoService = new MovimentacaoService();

        [HttpGet, Route("movimentacao")]
        public IHttpActionResult GetAll() {
            return Ok(MovimentacaoService.FindAll());
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
        public IHttpActionResult PostLivro([FromBody] Movimentacao movimentacao) {
            try {
                var erros = MovimentacaoService.IsValid(movimentacao);
                if (erros.Length == 0) {
                    MovimentacaoService.Add(movimentacao);
                    return Ok(movimentacao);
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