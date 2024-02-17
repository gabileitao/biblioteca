using Biblioteca.Domain.Models;
using Biblioteca.Repositories;
using System;
using System.Collections.Generic;

namespace Biblioteca.Services
{
    public class AutorService {

        public AutorRepository AutorRepository = new AutorRepository();

        public Autor[] FindAll() {
            return AutorRepository.FindAll();
        }

        public Autor[] FindPerName(string name) {

            return AutorRepository.FindPerName(name);
        }

        public Autor FindPerId(Guid id) {

            return AutorRepository.FindPerId(id);
        }

        public Autor Add(Autor autor) {

            return AutorRepository.Add(autor); ;
        }

        public Autor Att(Autor autor) {

            return AutorRepository.Att(autor);
        }

        public bool Delete(Guid autorId) {

            return AutorRepository.Remove(autorId);
        }

        public string[] IsValidAuthor(Autor autor) {
            var erros = new List<string>();

            if (autor.Nome == null || autor.Nome == "") {
                erros.Add("Nome do autor não pode estar vazio.");
            }

            if (autor.Nascimento == null) {
                erros.Add("Nascimento não pode ser nulo.");
            }

            if ((autor.Nascimento != null || autor.Falecimento != null) && autor.Nascimento >= autor.Falecimento) {
                erros.Add("Data de nascimento não pode ser maior ou igual a data de falecimento.");
            }

            return erros.ToArray();

        }

        public bool Exists(Guid id) {

            return AutorRepository.Exists(id);
        }

    }
}