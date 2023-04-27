using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Biblioteca.Domain.Models;
using Biblioteca.Domain;
using System.Xml.Linq;

namespace Biblioteca.Repositories {
    public class EditoraRepository {

        public string ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        //GetAll
        public Editora[] FindAll() {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "select * from editora where ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Editora> editoras = new List<Editora>();

            while (reader.Read()) {
                Editora editora = new Editora();
                editora.Id = reader.GetGuid(reader.GetOrdinal("id"));
                editora.Nome = reader.GetString(reader.GetOrdinal("nome"));
                editora.CNPJ = reader.GetString(reader.GetOrdinal("cnpj"));
                editoras.Add(editora);
            }

            connection.Close();

            return editoras.ToArray();
        }

        //GetPerName
        public Editora[] FindPerName(string name) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from editora where nome like '%{name}%' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Editora> editoras = new List<Editora>();

            while (reader.Read()) {
                Editora editora = new Editora();
                editora.Id = reader.GetGuid(0);
                editora.Nome = reader.GetString(1);
                editora.CNPJ = reader.GetString(2);
                editoras.Add(editora);
            }

            connection.Close();

            return editoras.ToArray();
        }

        //GetPerId
        public Editora[] FindPerId(Guid id) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from editora where id = '{id}' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Editora> editoras = new List<Editora>();

            while (reader.Read()) {
                Editora editora = new Editora();
                editora.Id = reader.GetGuid(0);
                editora.Nome = reader.GetString(1);
                editora.CNPJ = reader.GetString(2);
                editoras.Add(editora);
            }

            connection.Close();

            return editoras.ToArray();
        }

        //Post
        public Editora Add(Editora editora) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            editora.Id = Guid.NewGuid();

            string query = $"insert into editora values ('{editora.Id}', '{editora.Nome}', '{editora.CNPJ}', 1)";

            SqlCommand command = new SqlCommand(query, connection);

            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return editora;
        }

        //Put
        public Editora Att(Editora editora) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"update editora set nome = '{editora.Nome}', " +
                $"cnpj = '{editora.CNPJ}' where id = '{editora.Id}'";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return editora;
        }

        public Editora Remove(Editora editora) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"update editora set ativo = 0 where id = '{editora.Id}'";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return editora;
        }

        public bool Exists(Guid id) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select count(*) from editora where id = '{id}' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0;
        }

    }
}