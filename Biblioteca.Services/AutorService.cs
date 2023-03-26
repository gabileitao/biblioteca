using Biblioteca.Domain.Models;
using Biblioteca.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Services {
    public class AutorService {

        public AutorRepository AutorRepository = new AutorRepository();

        public Autor[] FindAll() {
            return AutorRepository.FindAll();
        }

    }
}