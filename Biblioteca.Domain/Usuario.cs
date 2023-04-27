using Biblioteca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain {
    public class Usuario {

        public Guid Id;
        public string Nome;
        public string Cpf;
        public string Login;
        public string Senha;
        public DateTime Nascimento;

    }
}