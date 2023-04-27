using Biblioteca.Domain;
using Biblioteca.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services {
    public class ObraService {

        public ObraRepository ObraRepository = new ObraRepository();

        public Obra[] FindAll() { 
            return ObraRepository.FindAll();
        }

        public Obra[] FindPerTitle(string title) {
            return ObraRepository.FindPerTitle(title);
        }

        public Obra[] FindPerId(Guid id) {
            return ObraRepository.FindPerId(id);
        }

        public Obra Add(Obra obra) {
            return ObraRepository.Add(obra);
        }

        public Obra Att(Obra obra) {
            return ObraRepository.Att(obra);
        }

        public Obra Remove(Obra obra) {
            return ObraRepository.Remove(obra);
        }

        public string[] IsValid(Obra obra) {
            var erros = new List<string>();

            if (obra.Titulo == null || obra.Titulo == "") {
                erros.Add("É necessário o título da obra.");
            }

            if(obra.Isbn == null || obra.Isbn == "") {
                erros.Add("É necessário o isbn da obra.");
            }

            if(obra.Genero == null || obra.Genero == "") {
                erros.Add("Adicione o gênero da obra.");
            }

            if(obra.Idautor == null || obra.Idautor == Guid.Empty) {
                erros.Add("É necessário indicar um autor existente.");
            }

            if (obra.Ideditora == null || obra.Ideditora == Guid.Empty) {
                erros.Add("É necessário indicar uma editora existente.");
            }

            return erros.ToArray();
        }

        public bool Exists(Guid id) {
            return ObraRepository.Exists(id);
        }

    }
}