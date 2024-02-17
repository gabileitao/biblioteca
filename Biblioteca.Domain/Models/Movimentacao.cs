using System;

namespace Biblioteca.Domain
{
    public class Movimentacao {

        public Guid Id;
        public DateTime? Datamovimentacao;
        public Guid Idlivro;
        public Guid Idlocatario;
        public Guid Idusuario;
        public string Tipo;

    }
}