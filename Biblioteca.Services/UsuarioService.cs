using Biblioteca.Domain;
using Biblioteca.Repositories;
using System;
using System.Collections.Generic;

namespace Biblioteca.Services
{
    public class UsuarioService {

        public UsuarioRepository UsuarioRepository = new UsuarioRepository();

        public Usuario[] FindAll() { 
            return UsuarioRepository.FindAll();
        }

        public Usuario[] FindPerName(string name) {
            return UsuarioRepository.FindPerName(name);
        }

        public Usuario[] FindPerId(Guid id) {
            return UsuarioRepository.FindPerId(id);
        }

        public Usuario Add(Usuario usuario) {
            return UsuarioRepository.Add(usuario);
        }

        public Usuario Att(Usuario usuario) {
            return UsuarioRepository.Att(usuario);
        }

        public Usuario Remove(Usuario usuario) {
            return UsuarioRepository.Remove(usuario);
        }

        public string[] IsValid(Usuario usuario) {
            var erros = new List<string>();

            if (usuario.Nome == null || usuario.Nome == "") {
                erros.Add("É necessário o nome do usuário.");
            }

            if(usuario.Cpf == null || usuario.Cpf == "") {
                erros.Add("É necessário o CPF da pessoa.");
            }

            if(usuario.Login == null || usuario.Login == "") {
                erros.Add("É necessário existir um Login.");
            }

            if (usuario.Senha == null || usuario.Senha == "") {
                erros.Add("É necessário existir uma senha.");
            }

            if (usuario.Nascimento == null) {
                erros.Add("É necessária a data de nascimento.");
            }

            return erros.ToArray();
        }

        public bool Exists(Guid id) {
            return UsuarioRepository.Exists(id);
        }

    }
}