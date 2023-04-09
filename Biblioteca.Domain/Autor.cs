using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain.Models {
    public class Autor {

        public Guid Id;
        public string Nome;
        public DateTime Nascimento;
        public DateTime? Falecimento;

    }
}
