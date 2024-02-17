using Biblioteca.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Biblioteca.Repositories
{
    public  class UsuarioRepository {

        public string ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public Usuario[] FindAll() {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "select * from usuario where ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);
            
            SqlDataReader reader = command.ExecuteReader();
            List<Usuario> usuarios = new List<Usuario>();

            while (reader.Read()) {
                Usuario usuario = new Usuario();
                usuario.Id = reader.GetGuid(0);
                usuario.Nome = reader.GetString(1);
                usuario.Cpf = reader.GetString(2);
                usuario.Login = reader.GetString(3);
                usuario.Senha = reader.GetString(4);
                usuario.Nascimento = reader.GetDateTime(5);
                usuarios.Add(usuario);
            }

            connection.Close();

            return usuarios.ToArray();
        }

        public Usuario[] FindPerName(string name) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from usuario where nome like '%{name}%' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Usuario> usuarios = new List<Usuario>();

            while (reader.Read()) {
                Usuario usuario = new Usuario();
                usuario.Id = reader.GetGuid(0);
                usuario.Nome = reader.GetString(1);
                usuario.Cpf = reader.GetString(2);
                usuario.Login = reader.GetString(3);
                usuario.Senha = reader.GetString(4);
                usuario.Nascimento = reader.GetDateTime(5);
                usuarios.Add(usuario);
            }

            connection.Close();

            return usuarios.ToArray();
        }

        public Usuario[] FindPerId(Guid id) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from usuario where id = '{id}' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Usuario> usuarios = new List<Usuario>();

            while (reader.Read()) {
                Usuario usuario = new Usuario();
                usuario.Id = reader.GetGuid(0);
                usuario.Nome = reader.GetString(1);
                usuario.Cpf = reader.GetString(2);
                usuario.Login = reader.GetString(3);
                usuario.Senha = reader.GetString(4);
                usuario.Nascimento = reader.GetDateTime(5);
                usuarios.Add(usuario);
            }

            connection.Close();

            return usuarios.ToArray();
        }

        public Usuario Add(Usuario usuario) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            usuario.Id = Guid.NewGuid();

            string query = $"insert into usuario values ('{usuario.Id}', '{usuario.Nome}', '{usuario.Cpf}', '{usuario.Login}', '{usuario.Senha}', '{usuario.Nascimento}', 1)";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return usuario;
        }

        public Usuario Att(Usuario usuario) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"update usuario set nome = '{usuario.Nome}', " +
                $"cpf = '{usuario.Cpf}', login = '{usuario.Login}', " +
                $"senha = '{usuario.Senha}', nascimento = '{usuario.Nascimento}' " +
                $"where id = '{usuario.Id}'";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return usuario;
        }

        public Usuario Remove(Usuario usuario) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"update usuario set ativo = 0 where id = '{usuario.Id}'";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return usuario;
        }

        public bool Exists(Guid id) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select count(*) from usuario where id = '{id}' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0;
        }

    }
}