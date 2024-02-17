using System;
using System.Web.Http;
using System.Web.Http.Cors;
using Biblioteca.Domain;
using Biblioteca.Services;

namespace Biblioteca.Api.Controllers
{
    [EnableCors("*", "*", "*")]
    public class MovimentacaoController : ApiController {

        public MovimentacaoService MovimentacaoService = new MovimentacaoService();
        public LocatarioService LocatarioService = new LocatarioService();
        public LivroService LivroService = new LivroService();

        [HttpGet, Route("movimentacao")]
        public IHttpActionResult GetAll() {
            return Ok(MovimentacaoService.FindAll());
        }

        [HttpGet, Route("movimentacao")]
        public IHttpActionResult GetPerDate([FromUri] DateTime date) {
            return Ok(MovimentacaoService.FindPerDate(date));
        }

        [HttpGet, Route("movimentacao")]
        public IHttpActionResult GetPerId([FromUri] Guid id) {
            return Ok(MovimentacaoService.FindPerId(id));
        }

        [HttpGet, Route("movimentacao")]
        public IHttpActionResult GetBookStatus([FromUri] Guid idlivro) {

            if (LivroService.Exists(idlivro)) {
                if (MovimentacaoService.ExistsMov(idlivro)) {
                    return Ok(MovimentacaoService.FindBookStatus(idlivro));
                } else {
                    return BadRequest("Não existem movimentações com essa copia.");
                }
            } else {
                return BadRequest("Copia não existe.");
            }

        }

        [HttpPost, Route("movimentacao")]
        public IHttpActionResult PostMovimentacao([FromBody] Movimentacao movimentacao) {
            try {
                var erros = MovimentacaoService.IsValid(movimentacao);
                if (erros.Length == 0) {
                    if (!MovimentacaoService.Exists(movimentacao.Id)) {
                        if (LocatarioService.Exists(movimentacao.Idlocatario)) {
                            MovimentacaoService.Add(movimentacao);
                            return Ok(movimentacao);
                        } else {
                            return BadRequest("Locatario não existe.");
                        }
                    } else {
                        return BadRequest("Movimentação já existe com esse Id.");
                    }
                } else {
                    return BadRequest(string.Join(", ", erros));
                }
            } catch (Exception e) {
                return InternalServerError(e);
            }
        }

    }
}