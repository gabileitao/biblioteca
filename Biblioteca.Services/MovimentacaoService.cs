using Biblioteca.Domain;
using Biblioteca.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services {
    public class MovimentacaoService {

        public MovimentacaoRepository MovimentacaoRepository = new MovimentacaoRepository();

        public Movimentacao[] FindAll() { 
            return MovimentacaoRepository.FindAll();
        }

        public Movimentacao[] FindPerDate(DateTime date) {
            return MovimentacaoRepository.FindPerDate(date);
        }

        public Movimentacao[] FindPerId(Guid id) {
            return MovimentacaoRepository.FindPerId(id);
        }

        public Movimentacao[] FindBookStatus(Guid idlivro) {
            return MovimentacaoRepository.FindBookStatus(idlivro);
        }

        public Movimentacao Add(Movimentacao movimentacao) {
          
            if(movimentacao.Datamovimentacao == null) {
                movimentacao.Datamovimentacao = DateTime.Now;
            }
                        
            return MovimentacaoRepository.Add(movimentacao);
        }

        public string[] IsValid(Movimentacao movimentacao) {
            var erros = new List<string>();

            if (movimentacao.Idlivro == null || movimentacao.Idlivro == Guid.Empty) {
                erros.Add("É necessário inserir um id válido.");
            }

            if (movimentacao.Idlocatario == null || movimentacao.Idlocatario == Guid.Empty) {
                erros.Add("É necessário indicar o id do cliente.");
            }

            if (movimentacao.Idusuario == null || movimentacao.Idusuario == Guid.Empty) {
                erros.Add("É necessário indicar o id do funcionário.");
            }

            return erros.ToArray();
        }

        public bool Exists(Guid id) {
            return MovimentacaoRepository.Exists(id);
        }

        public bool ExistsMov(Guid idlivro) {
            return MovimentacaoRepository.ExistsMov(idlivro);
        }

    }
}