using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using Biblioteca.Domain.Models;

namespace Biblioteca.Repositories {
    public class AutorRepository {

        public string ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public Autor[] FindAll() {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "select * from autor";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Autor> autores = new List<Autor>();

            //serialização dos dados da base.
            //conceito de retirada de dados da base e colocar na memória em formato de objeto.
            while (reader.Read()) {
                Autor autor = new Autor();
                //autor.Id = reader.GetInt32(0);
                //autor.Nome = reader.GetString(1);
                //autor.Nascimento = reader.GetDateTime(2);
                //autor.Falecimento = reader.GetDateTime(3);
                autor.Id = reader.GetGuid(reader.GetOrdinal("id"));
                autor.Nome = reader.GetString(reader.GetOrdinal("nome"));
                autor.Nascimento = reader.GetDateTime(reader.GetOrdinal("nascimento"));
                if (!reader.IsDBNull(reader.GetOrdinal("falecimento"))) {
                    autor.Falecimento = reader.GetDateTime(reader.GetOrdinal("falecimento"));
                }
                autores.Add(autor);
            }

            connection.Close();

            return autores.ToArray();
        }

        public Autor[] FindPerName(string name) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from autor where nome like '%{name}%'";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Autor> autores = new List<Autor>();

            while (reader.Read())
            {
                Autor autor = new Autor();
                autor.Id = reader.GetGuid(0);
                autor.Nome = reader.GetString(1);
                autor.Nascimento = reader.GetDateTime(2);
                //autor.Falecimento = reader.GetDateTime(3);
                if (!reader.IsDBNull(reader.GetOrdinal("falecimento")))
                {
                    autor.Falecimento = reader.GetDateTime(reader.GetOrdinal("falecimento"));
                }
                autores.Add(autor);
            }

            connection.Close();

            return autores.ToArray();
        }

        //Post - inserir dados

        public Autor AddAuthor(Autor autor) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            autor.Id = Guid.NewGuid();

            var falecimento = autor.Falecimento != null ? $"'{autor.Falecimento}'" : "null";

            string query = $"insert into autor values ('{autor.Id}', '{autor.Nome}', '{autor.Nascimento}', {falecimento})";

            SqlCommand command = new SqlCommand(query, connection);

            int affectedLines = command.ExecuteNonQuery();

            //if(affectedLines > 0) {
            //    return autor;
            //}

            return autor;
        }

        //Put - atualizar dados

    }
}
