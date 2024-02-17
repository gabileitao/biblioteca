using Biblioteca.Domain;
using Biblioteca.Repositories;
using System;
using System.Collections.Generic;

namespace Biblioteca.Services
{
    public class LivroService {

        public LivroRepository LivroRepository = new LivroRepository();

        public Livro[] FindAll() { 
            return LivroRepository.FindAll();
        }

        public Livro[] FindPerBarcode(string barcode) {
            return LivroRepository.FindPerBarcode(barcode);
        }

        public Livro[] FindPerId(Guid id) {
            return LivroRepository.FindPerId(id);
        }

        public Livro Add(Livro livro) {
            //livro.Doacao = livro.Doacao == null ? livro.Doacao = DateTime.Now;

            if(livro.Doacao == null) {
                livro.Doacao = DateTime.Now;
            }
                        
            return LivroRepository.Add(livro);
        }

        public Livro Att(Livro livro) {
            return LivroRepository.Att(livro);
        }

        public Livro Remove(Livro livro) {
            return LivroRepository.Remove(livro);
        }

        public string[] IsValid(Livro livro) {
            var erros = new List<string>();

            if (livro.Barcode == null || livro.Barcode == "") {
                erros.Add("É necessário inserir um código de barras.");
            }

            if (livro.Idobra == null || livro.Idobra == Guid.Empty) {
                erros.Add("É necessário indicar uma obra existente.");
            }

            return erros.ToArray();
        }

        public bool Exists(Guid id) {
            return LivroRepository.Exists(id);
        }

        public bool Exists(string barcode) {
            return LivroRepository.Exists(barcode);
        }
    }
}