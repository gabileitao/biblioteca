using Biblioteca.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Biblioteca.Repositories
{
    public  class ObraRepository {

        public string ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public Obra[] FindAll() {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "select * from obra where ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);
            
            SqlDataReader reader = command.ExecuteReader();
            List<Obra> obras = new List<Obra>();

            while (reader.Read()) {
                Obra obra = new Obra();
                obra.Id = reader.GetGuid(0);
                obra.Titulo = reader.GetString(1);
                obra.Isbn = reader.GetString(2);
                obra.Genero = reader.GetString(3);
                obra.Idautor = reader.GetGuid(4);
                obra.Ideditora = reader.GetGuid(5);
                obras.Add(obra);
            }

            connection.Close();

            return obras.ToArray();
        }

        public Obra[] FindPerTitle(string title) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from obra where titulo like '%{title}%' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Obra> obras = new List<Obra>();

            while (reader.Read()) {
                Obra obra = new Obra();
                obra.Id = reader.GetGuid(0);
                obra.Titulo = reader.GetString(1);
                obra.Isbn = reader.GetString(2);
                obra.Genero = reader.GetString(3);
                obra.Idautor = reader.GetGuid(4);
                obra.Ideditora = reader.GetGuid(5);
                obras.Add(obra);
            }

            connection.Close();

            return obras.ToArray();
        }

        public Obra[] FindPerId(Guid id) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from obra where id = '{id}' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Obra> obras = new List<Obra>();

            while (reader.Read()) {
                Obra obra = new Obra();
                obra.Id = reader.GetGuid(0);
                obra.Titulo = reader.GetString(1);
                obra.Isbn = reader.GetString(2);
                obra.Genero = reader.GetString(3);
                obra.Idautor = reader.GetGuid(4);
                obra.Ideditora = reader.GetGuid(5);
                obras.Add(obra);
            }

            connection.Close();

            return obras.ToArray();
        }

        public Obra Add(Obra obra) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            obra.Id = Guid.NewGuid();

            string query = $"insert into obra values ('{obra.Id}', '{obra.Titulo}', '{obra.Isbn}', '{obra.Genero}', '{obra.Idautor}', '{obra.Ideditora}', 1)";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return obra;
        }

        public Obra Att(Obra obra) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"update obra set titulo = '{obra.Titulo}', " +
                $"isbn = '{obra.Isbn}', genero = '{obra.Genero}', " +
                $"idautor = '{obra.Idautor}', ideditora = '{obra.Ideditora}' " +
                $"where id = '{obra.Id}'";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return obra;
        }

        public Obra Remove(Obra obra) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"update obra set ativo = 0 where id = '{obra.Id}'";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return obra;
        }

        public bool Exists(Guid id) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select count(*) from obra where id = '{id}' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0;
        }

    }
}