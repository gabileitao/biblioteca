using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Biblioteca.Domain.Models;

namespace Biblioteca.Repositories
{
    public class AutorRepository {

        public string ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public Autor[] FindAll() {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "select * from autor where ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Autor> autores = new List<Autor>();

            //serialização dos dados da base.
            //conceito de retirada de dados da base e colocar na memória em formato de objeto.
            while (reader.Read()) {
                Autor autor = new Autor();
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

            string query = $"select * from autor where nome like '%{name}%' and ativo = 1";

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

        public Autor FindPerId(Guid id) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from autor where id = '{id}' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Autor> autores = new List<Autor>();

            while (reader.Read()) {
                Autor autor = new Autor();
                autor.Id = reader.GetGuid(0);
                autor.Nome = reader.GetString(1);
                autor.Nascimento = reader.GetDateTime(2);
                if (!reader.IsDBNull(reader.GetOrdinal("falecimento"))) {
                    autor.Falecimento = reader.GetDateTime(reader.GetOrdinal("falecimento"));
                }
                autores.Add(autor);
            }

            connection.Close();

            var autoresArray = autores.ToArray();


            if (autoresArray.Length == 1){
                return autoresArray[0];
            }
            else{
                return null;
            }
        }

        //Post - inserir dados
        public Autor Add(Autor autor) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            autor.Id = Guid.NewGuid();

            var falecimento = autor.Falecimento != null ? $"'{autor.Falecimento}'" : "null";

            string query = $"insert into autor values ('{autor.Id}', '{autor.Nome}', '{autor.Nascimento}', {falecimento}, 1)";

            SqlCommand command = new SqlCommand(query, connection);

            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return autor;
        }

        //Put - atualizar dados
        public Autor Att(Autor autor)  {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            var falecimento = autor.Falecimento != null ? $"'{autor.Falecimento:yyyy-MM-ddTHH:mm:ss}'" : "null";

            string query = $"update autor set nome = '{autor.Nome}', " +
                $"nascimento = '{autor.Nascimento:yyyy-MM-ddTHH:mm:ss}', " +
                $"falecimento = {falecimento} where id = '{autor.Id}'";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return autor;
        }

        public bool Remove(Guid autorId) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"update autor set ativo = 0 where id = '{autorId}'";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return affectedLines > 0;
        }

        public bool Exists(Guid id) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select count(*) from autor where id = '{id}' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            int count = (int)command.ExecuteScalar(); //só serve para usar com Select e retorna só a primera linha da primeira coluna
            connection.Close();

            return count > 0;
        }

    }
}