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

        //public Livro[] FindPerBarcode(string barcode) {
        //    return MovimentacaoRepository.FindPerBarcode(barcode);
        //}

        //public Livro[] FindPerId(Guid id) {
        //    return MovimentacaoRepository.FindPerId(id);
        //}

        public Movimentacao Add(Movimentacao movimentacao) {
          
            if(movimentacao.Datamovimentacao == null) {
                movimentacao.Datamovimentacao = DateTime.Now;
            }
                        
            return MovimentacaoRepository.Add(movimentacao);
        }

        public Livro Att(Livro livro) {
            return LivroRepository.Att(livro);
        }

        public Livro Remove(Livro livro) {
            return LivroRepository.Remove(livro);
        }

        public string[] IsValid(Movimentacao movimentacao) {
            var erros = new List<string>();

            if (movimentacao.Idlivro == null || movimentacao.Idlivro == Guid.Empty) {
                erros.Add("É necessário inserir um livro.");
            }

            if (movimentacao.Idlocatario == null || movimentacao.Idlocatario == Guid.Empty) {
                erros.Add("É necessário indicar o id do cliente.");
            }

            if (movimentacao.Idusuario == null || movimentacao.Idusuario == Guid.Empty) {
                erros.Add("É necessário indicar o id do funcionário.");
            }

            return erros.ToArray();
        }

        //public bool Exists(Guid id) {
        //    return LivroRepository.Exists(id);
        //}

        //public bool Exists(string barcode) {
        //    return LivroRepository.Exists(barcode);
        //}
    }
}