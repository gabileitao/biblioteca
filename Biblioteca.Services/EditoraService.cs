using Biblioteca.Domain;
using Biblioteca.Domain.Models;
using Biblioteca.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services {
    public class EditoraService{

        public EditoraRepository EditoraRepository = new EditoraRepository();

        public Editora[] FindAll() {
            return EditoraRepository.FindAll();
        }

        public Editora[] FindPerName(string name) {
            return EditoraRepository.FindPerName(name);
        }

        public Editora[] FindPerId(Guid id) {
            return EditoraRepository.FindPerId(id);
        }

        public Editora Add(Editora editora) {
            return EditoraRepository.Add(editora);
        }

        public Editora Att(Editora editora) {
            return EditoraRepository.Att(editora);
        }

        public Editora Remove(Editora editora) {
            return EditoraRepository.Remove(editora);
        }

        public string[] IsValid(Editora editora) {
            var erros = new List<string>();

            if (editora.Nome == null || editora.Nome == "") {
                erros.Add("Nome da editora não pode estar vazio.");
            }

            if (editora.CNPJ == null || editora.CNPJ == "") {
                erros.Add("CNPJ não pode ser nulo.");
            }

            return erros.ToArray();
        }

        public bool Exists(Guid id) {

            return EditoraRepository.Exists(id);
        }
    }
}
