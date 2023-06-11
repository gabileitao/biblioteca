using Biblioteca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain {
    public class Livro {

        public Guid Id;
        public string Barcode;
        public Guid Idobra;
        public DateTime Doacao;

    }
}