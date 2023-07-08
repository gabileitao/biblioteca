using Biblioteca.Domain;
using Biblioteca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Repositories {
    public  class MovimentacaoRepository {

        public string ConnectionString = ConfigurationManager.AppSettings.Get("ConnectionString");

        public Movimentacao[] FindAll() {

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = "select * from movimentacao where ativo = 1";

            SqlCommand command = new SqlCommand(query, connection);
            
            SqlDataReader reader = command.ExecuteReader();
            List<Movimentacao> movimentacoes = new List<Movimentacao>();

            while (reader.Read()) {
                Movimentacao movimentacao = new Movimentacao();
                movimentacao.Datamovimentacao = reader.GetDateTime(0);
                movimentacao.Idlivro = reader.GetGuid(1);
                movimentacao.Idlocatario = reader.GetGuid(2);
                movimentacao.Idusuario = reader.GetGuid(3);
                movimentacoes.Add(movimentacao);
            }

            connection.Close();

            return movimentacoes.ToArray();
        }

        //public Livro[] FindPerBarcode(string barcode) {

        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    connection.Open();

        //    string query = $"select * from livro where barcode like '%{barcode}%' and ativo = 1";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    SqlDataReader reader = command.ExecuteReader();
        //    List<Livro> livros = new List<Livro>();

        //    while (reader.Read()) {
        //        Livro livro = new Livro();
        //        livro.Id = reader.GetGuid(0);
        //        livro.Barcode = reader.GetString(1);
        //        livro.Idobra = reader.GetGuid(2);
        //        livro.Doacao = reader.GetDateTime(3);
        //        livros.Add(livro);
        //    }

        //    connection.Close();

        //    return livros.ToArray();
        //}

        //public Livro[] FindPerId(Guid id) {
        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    connection.Open();

        //    string query = $"select * from livro where id = '{id}' and ativo = 1 ";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    SqlDataReader reader = command.ExecuteReader();
        //    List<Livro> livros = new List<Livro>();

        //    while (reader.Read()) {
        //        Livro livro = new Livro();
        //        livro.Id = reader.GetGuid(0);
        //        livro.Barcode = reader.GetString(1);
        //        livro.Idobra = reader.GetGuid(2);
        //        livro.Doacao = reader.GetDateTime(3);
        //        livros.Add(livro);
        //    }

        //    connection.Close();

        //    return livros.ToArray();
        //}

        public Movimentacao Add(Movimentacao movimentacao) {
            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();

            string query = $"insert into movimentacao values ('{movimentacao.Datamovimentacao}', '{movimentacao.Idlivro}', '{movimentacao.Idlocatario}', '{movimentacao.Idusuario}', 1)";

            SqlCommand command = new SqlCommand(query, connection);
            int affectedLines = command.ExecuteNonQuery();
            connection.Close();

            return movimentacao;
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

        //public bool Exists(string barcode) {
        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    connection.Open();

        //    string query = $"select count(*) from livro where barcode = '{barcode}' and ativo = 1";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    int count = (int)command.ExecuteScalar();
        //    connection.Close();

        //    return count > 0;
        //}

        //public bool Exists(Guid id) {
        //    SqlConnection connection = new SqlConnection(ConnectionString);
        //    connection.Open();

        //    string query = $"select count(*) from livro where id = '{id}' and ativo = 1";

        //    SqlCommand command = new SqlCommand(query, connection);

        //    int count = (int)command.ExecuteScalar();
        //    connection.Close();

        //    return count > 0;
        //}

    }
}