using Biblioteca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain {
    public class Obra {

        public Guid Id;
        public string Titulo; 
        public string Isbn;
        public string Genero;
        public Guid Idautor;
        public Guid Ideditora;

    }
}
