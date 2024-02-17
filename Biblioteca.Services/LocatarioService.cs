using Biblioteca.Domain;
using Biblioteca.Repositories;
using System;
using System.Collections.Generic;

namespace Biblioteca.Services
{
    public class LocatarioService {

        public LocatarioRepository LocatarioRepository = new LocatarioRepository();

        public Locatario[] FindAll() { 
            return LocatarioRepository.FindAll();
        }

        public Locatario[] FindPerName(string name) {
            return LocatarioRepository.FindPerName(name);
        }

        public Locatario[] FindPerId(Guid id) {
            return LocatarioRepository.FindPerId(id);
        }

        public Locatario Add(Locatario locatario) {
            return LocatarioRepository.Add(locatario);
        }

        public Locatario Att(Locatario locatario) {
            return LocatarioRepository.Att(locatario);
        }

        public Locatario Remove(Locatario locatario) {
            return LocatarioRepository.Remove(locatario);
        }

        public string[] IsValid(Locatario locatario) {
            var erros = new List<string>();

            if (locatario.Nome == null || locatario.Nome == "") {
                erros.Add("É necessário o nome do locatario.");
            }

            if(locatario.Cpf == null || locatario.Cpf == "") {
                erros.Add("É necessário o CPF da pessoa.");
            }

            if(locatario.Email == null || locatario.Email == "") {
                erros.Add("É necessário um e-mail.");
            }

            if (locatario.Nascimento == null) {
                erros.Add("É necessária a data de nascimento.");
            }

            return erros.ToArray();
        }

        public bool Exists(Guid id) {
            return LocatarioRepository.Exists(id);
        }

    }
}