/*using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using WebAPI.Models;

namespace WebAPI {
    public class DBook {
        //métodos CRUD - inserir livro
        public void Insert(int id, string title, string author, int release, string publishingHouse, string category, string description) {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //inserindo o dado novo 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO BOOK VALUES('" + id + "', '" + title + "', '" + author + "', '" + release + "', '" + publishingHouse + "', '" + category + "', '" + description + "'); ";
            cmd.CommandType = CommandType.Text;
            conn.Open();
            try {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro realizado com sucesso!");
            }
            catch (Exception ex) {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally {
                conn.Close();
            }
        }

        //métodos CRUD - ler um dado no banco
        public void Search(int id) {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM BOOK WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();

            try {
                NpgsqlDataReader data = cmd.ExecuteReader();
                while (data.Read()) {
                    for (int i = 0; i < data.FieldCount; i++) {
                        Console.Write("{0} \t", data[i]);
                    }
                    Console.WriteLine();
                }
            }
            finally {
                conn.Close();
            }
        }

        //métodos CRUD - retornar a tabela inteira
        public void GetAllData(){

            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");
            conn.Open();

            NpgsqlCommand exibeAll = new NpgsqlCommand("select * from book", conn);

            //buscando dados
            try
            {
                NpgsqlDataReader dt = exibeAll.ExecuteReader();
                while (dt.Read())
                {
                    for (int i = 0; i < dt.FieldCount; i++)
                    {
                        Console.Write("{0} \t", dt[i]);
                    }
                    Console.WriteLine();
                }

            }

            finally
            {
                conn.Close();
            }

            
        }

        //métodos CRUD - atualizar todos os campos de uma só vez
        public void UpdateAll(int id, string title, string author, int release, string publishingHouse, string category, string description) {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET TITLE='" + title + "',AUTHOR='" + author + "',RELEASE='" + release + "',PUBLISHING='" + publishingHouse + "',CATEGORY='" + category + "',DESCRIPTION='" + description + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex) {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally {
                conn.Close();
            }
        }

        //métodos CRUD - atualizar campo: título da obra
        public void UpdateTitle(int id, string title) {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET TITLE='" + title + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex) {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally {
                conn.Close();
            }
        }

        //métodos CRUD - atualizar campo: autor
        public void UpdateAuthor(int id, string author) {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET AUTHOR='" + author + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex) {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally {
                conn.Close();
            }
        }

        //métodos CRUD - atualizar campo: ano de lançamento
        public void UpdateRelease(int id, int release) {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET RELEASE='" + release + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex) {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally {
                conn.Close();
            }
        }

        //métodos CRUD - atualizar campo: editora
        public void UpdatePublishing(int id, string publishingHouse) {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET PUBLISHING='" + publishingHouse + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex) {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally {
                conn.Close();
            }
        }

        //métodos CRUD - atualizar campo: categoria
        public void UpdateCategory(int id, string category) {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET CATEGORY='" + category + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex) {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally {
                conn.Close();
            }
        }

        //métodos CRUD - atualizar campo: descrição
        public void UpdateDescription(int id, string description) {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET DESCRIPTION='" + description + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex) {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally {
                conn.Close();
            }
        }

        //deletar um registro
        public void Delete(int id) {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM BOOK WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Registro deletado com sucesso!");
            }
            catch (Exception ex) {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally {
                conn.Close();
            }
        }
    }
}
*/