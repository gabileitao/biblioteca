using Biblioteca.Domain.Models;
using Biblioteca.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services {
    public class AutorService {

        public AutorRepository AutorRepository = new AutorRepository();

        public Autor[] FindAll() {
            return AutorRepository.FindAll();
        }

        public Autor[] FindPerName(string name) {

            return AutorRepository.FindPerName(name);
        }

        public Autor[] FindPerId(Guid id) {

            return AutorRepository.FindPerId(id);
        }

        public bool AuthorExists(Guid id) {

            return AutorRepository.AuthorExists(id);
        }

        public Autor AddAuthor(Autor autor) {

            return AutorRepository.AddAuthor(autor); ;
        }

        public Autor AttAuthor(Autor autor) {

            return AutorRepository.AttAuthor(autor);
        }

        public Autor DeleteAuthor(Autor autor) {

            return AutorRepository.RemoveAuthor(autor);
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

    }
}