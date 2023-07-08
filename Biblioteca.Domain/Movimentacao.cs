using Biblioteca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Domain {
    public class Movimentacao {

        public DateTime? Datamovimentacao;
        public Guid Idlivro;
        public Guid Idlocatario;
        public Guid Idusuario;

    }
}