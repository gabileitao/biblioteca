using Biblioteca.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Biblioteca.Repositories
{
    public  class LocatarioRepository {

        public string ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public Locatario[] FindAll() {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "select * from locatario where ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);
            
            SqlDataReader reader = command.ExecuteReader();
            List<Locatario> locatarios = new List<Locatario>();

            while (reader.Read()) {
                Locatario locatario = new Locatario();
                locatario.Id = reader.GetGuid(0);
                locatario.Nome = reader.GetString(1);
                locatario.Cpf = reader.GetString(2);
                locatario.Email = reader.GetString(3);
                locatario.Nascimento = reader.GetDateTime(4);
                if (!reader.IsDBNull(reader.GetOrdinal("telefone"))) {
                    locatario.Telefone = reader.GetString(reader.GetOrdinal("telefone"));
                }
                locatarios.Add(locatario);
            }

            connection.Close();

            return locatarios.ToArray();
        }

        public Locatario[] FindPerName(string name) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from locatario where nome like '%{name}%' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Locatario> locatarios = new List<Locatario>();

            while (reader.Read()) {
                Locatario locatario = new Locatario();
                locatario.Id = reader.GetGuid(0);
                locatario.Nome = reader.GetString(1);
                locatario.Cpf = reader.GetString(2);
                locatario.Email = reader.GetString(3);
                locatario.Nascimento = reader.GetDateTime(4);
                if (!reader.IsDBNull(reader.GetOrdinal("telefone"))) {
                    locatario.Telefone = reader.GetString(reader.GetOrdinal("telefone"));
                }
                locatarios.Add(locatario);
            }

            connection.Close();

            return locatarios.ToArray();
        }

        public Locatario[] FindPerId(Guid id) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from locatario where id = '{id}' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Locatario> locatarios = new List<Locatario>();

            while (reader.Read()) {
                Locatario locatario = new Locatario();
                locatario.Id = reader.GetGuid(0);
                locatario.Nome = reader.GetString(1);
                locatario.Cpf = reader.GetString(2);
                locatario.Email = reader.GetString(3);
                locatario.Nascimento = reader.GetDateTime(4);
                if (!reader.IsDBNull(reader.GetOrdinal("telefone"))) {
                    locatario.Telefone = reader.GetString(reader.GetOrdinal("telefone"));
                }
                locatarios.Add(locatario);
            }

            connection.Close();

            return locatarios.ToArray();
        }

        public Locatario Add(Locatario locatario) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            locatario.Id = Guid.NewGuid();

            var telefone = locatario.Telefone != null ? $"'{locatario.Telefone}'" : "null";

            string query = $"insert into locatario values ('{locatario.Id}', '{locatario.Nome}', '{locatario.Cpf}', '{locatario.Email}', '{locatario.Nascimento}', {telefone},  1)";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return locatario;
        }

        public Locatario Att(Locatario locatario) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            var telefone = locatario.Telefone != null ? $"'{locatario.Telefone}'" : "null";

            string query = $"update locatario set nome = '{locatario.Nome}', " +
                $"cpf = '{locatario.Cpf}', email = '{locatario.Email}', " +
                $"nascimento = '{locatario.Nascimento}', telefone = {telefone} " +
                $"where id = '{locatario.Id}'";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return locatario;
        }

        public Locatario Remove(Locatario locatario) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"update locatario set ativo = 0 where id = '{locatario.Id}'";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return locatario;
        }

        public bool Exists(Guid id) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select count(*) from locatario where id = '{id}' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0;
        }

    }
}