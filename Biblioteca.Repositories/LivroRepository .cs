using Biblioteca.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Biblioteca.Repositories
{
    public  class LivroRepository {

        public string ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public Livro[] FindAll() {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "select * from livro where ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);
            
            SqlDataReader reader = command.ExecuteReader();
            List<Livro> livros = new List<Livro>();

            while (reader.Read()) {
                Livro livro = new Livro();
                livro.Id = reader.GetGuid(0);
                livro.Barcode = reader.GetString(1);
                livro.Idobra = reader.GetGuid(2);
                livro.Doacao = reader.GetDateTime(3);
                livros.Add(livro);
            }

            connection.Close();

            return livros.ToArray();
        }

        public Livro[] FindPerBarcode(string barcode) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from livro where barcode like '%{barcode}%' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Livro> livros = new List<Livro>();

            while (reader.Read()) {
                Livro livro = new Livro();
                livro.Id = reader.GetGuid(0);
                livro.Barcode = reader.GetString(1);
                livro.Idobra = reader.GetGuid(2);
                livro.Doacao = reader.GetDateTime(3);
                livros.Add(livro);
            }

            connection.Close();

            return livros.ToArray();
        }

        public Livro[] FindPerId(Guid id) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from livro where id = '{id}' and ativo = 1 ";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Livro> livros = new List<Livro>();

            while (reader.Read()) {
                Livro livro = new Livro();
                livro.Id = reader.GetGuid(0);
                livro.Barcode = reader.GetString(1);
                livro.Idobra = reader.GetGuid(2);
                livro.Doacao = reader.GetDateTime(3);
                livros.Add(livro);
            }

            connection.Close();

            return livros.ToArray();
        }

        public Livro Add(Livro livro) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            livro.Id = Guid.NewGuid();

            string query = $"insert into livro values ('{livro.Id}', '{livro.Barcode}', '{livro.Idobra}', '{livro.Doacao}', 1)";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return livro;
        }

        public Livro Att(Livro livro) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"update livro set barcode = '{livro.Barcode}', idobra = '{livro.Idobra}', doacao = '{livro.Doacao}' where id = '{livro.Id}'";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return livro;
        }

        public Livro Remove(Livro livro) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"update livro set ativo = 0 where id = '{livro.Id}'";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return livro;
        }

        public bool Exists(string barcode) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select count(*) from livro where barcode = '{barcode}' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0;
        }

        public bool Exists(Guid id) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select count(*) from livro where id = '{id}' and ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);

            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0;
        }

    }
}