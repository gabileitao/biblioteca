using Biblioteca.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Biblioteca.Repositories
{
    public  class MovimentacaoRepository {

        public string ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public Movimentacao[] FindAll() {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "select * from movimentacao";

            SqlCommand command = new SqlCommand(query, connection);
            
            SqlDataReader reader = command.ExecuteReader();
            List<Movimentacao> movimentacoes = new List<Movimentacao>();

            while (reader.Read()) {
                Movimentacao movimentacao = new Movimentacao();
                movimentacao.Id = reader.GetGuid(0);
                movimentacao.Datamovimentacao = reader.GetDateTime(1);
                movimentacao.Idlivro = reader.GetGuid(2);
                movimentacao.Idlocatario = reader.GetGuid(3);
                movimentacao.Idusuario = reader.GetGuid(4);
                movimentacao.Tipo = reader.GetString(5);
                movimentacoes.Add(movimentacao);
            }

            connection.Close();

            return movimentacoes.ToArray();
        }

        public Movimentacao[] FindPerDate(DateTime date) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string yyyyMMdd = $"{date.Year}-{date.Month}-{date.Day}";

            string query = $"select * from movimentacao where datamovimentacao >= '{yyyyMMdd} 00:00:00' and datamovimentacao <= '{yyyyMMdd} 23:59:59'";


            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Movimentacao> movimentacoes = new List<Movimentacao>();

            while (reader.Read()) {
                Movimentacao movimentacao = new Movimentacao();
                movimentacao.Id = reader.GetGuid(0);
                movimentacao.Datamovimentacao = reader.GetDateTime(1);
                movimentacao.Idlivro = reader.GetGuid(2);
                movimentacao.Idlocatario = reader.GetGuid(3);
                movimentacao.Idusuario = reader.GetGuid(4);
                movimentacao.Tipo = reader.GetString(5);
                movimentacoes.Add(movimentacao);
            }

            connection.Close();

            return movimentacoes.ToArray();
        }


        public Movimentacao[] FindPerId(Guid id) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from movimentacao where id = '{id}'";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Movimentacao> movimentacoes = new List<Movimentacao>();

            while (reader.Read()) {
                Movimentacao movimentacao = new Movimentacao();
                movimentacao.Id = reader.GetGuid(0);
                movimentacao.Datamovimentacao = reader.GetDateTime(1);
                movimentacao.Idlivro = reader.GetGuid(2);
                movimentacao.Idlocatario = reader.GetGuid(3);
                movimentacao.Idusuario = reader.GetGuid(4);
                movimentacao.Tipo = reader.GetString(5);
                movimentacoes.Add(movimentacao);
            }

            connection.Close();

            return movimentacoes.ToArray();
        }


        public Movimentacao[] FindBookStatus(Guid idlivro) {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select * from movimentacao where idlivro = '{idlivro}' order by datamovimentacao desc";

            SqlCommand command = new SqlCommand(query, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<Movimentacao> movimentacoes = new List<Movimentacao>();

            while (reader.Read()) {
                Movimentacao movimentacao = new Movimentacao();
                movimentacao.Id = reader.GetGuid(0);
                movimentacao.Datamovimentacao = reader.GetDateTime(1);
                movimentacao.Idlivro = reader.GetGuid(2);
                movimentacao.Idlocatario = reader.GetGuid(3);
                movimentacao.Idusuario = reader.GetGuid(4);
                movimentacao.Tipo = reader.GetString(5);
                movimentacoes.Add(movimentacao);
            }

            connection.Close();

            return movimentacoes.ToArray();

        }

        public Movimentacao Add(Movimentacao movimentacao) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            movimentacao.Id = Guid.NewGuid();

            string query = $"insert into movimentacao values ('{movimentacao.Id}','{movimentacao.Datamovimentacao}', " +
                $"'{movimentacao.Idlivro}', '{movimentacao.Idlocatario}', '{movimentacao.Idusuario}', '{movimentacao.Tipo}')";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return movimentacao;
        }

        public bool Exists(Guid id) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select count(*) from movimentacao where id = '{id}'";

            SqlCommand command = new SqlCommand(query, connection);

            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0;
        }

        public bool ExistsMov(Guid idlivro) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"select count(*) from movimentacao where idlivro = '{idlivro}'";

            SqlCommand command = new SqlCommand(query, connection);

            int count = (int)command.ExecuteScalar();
            connection.Close();

            return count > 0;
        }

    }
}