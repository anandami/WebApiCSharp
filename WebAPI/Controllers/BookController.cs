using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Models;





namespace WebAPI.Controllers {

    [RoutePrefix("book")]
    [EnableCors("*", "*", "*", "*")]
    public class BookController : ApiController
    {
        //métodos CRUD - inserir livro
        [HttpPost]
        [Route("Insert")]
        public void Insert()
        {
            BookModel book = new BookModel();
            HttpContent requestContent = Request.Content;
            string jsonContent = (requestContent.ReadAsStringAsync().Result);
            book = Newtonsoft.Json.JsonConvert.DeserializeObject<BookModel>(jsonContent);

            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");
            conn.Open();
            try
            {    
                
                //gerando o comando com o banco
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO BOOK (TITLE,AUTHOR,RELEASE,PUBLISHING,CATEGORY,DESCRIPTION) VALUES(@title,@author,@release,@publishing,@category,@description); ";
                //cmd.Parameters.AddWithValue("@id", book.Id);
                cmd.Parameters.AddWithValue("@title", book.Title);
                cmd.Parameters.AddWithValue("@author", book.Author);
                cmd.Parameters.AddWithValue("@release", book.Release);
                cmd.Parameters.AddWithValue("@publishing", book.PublishingHouse);
                cmd.Parameters.AddWithValue("@category", book.Category);
                cmd.Parameters.AddWithValue("@description", book.Description);
                cmd.CommandType = CommandType.Text;
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Registro realizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }

        }
      
        //métodos CRUD - ler um dado no banco
        [HttpGet]
        [Route("Search/{id}")]
        public List <BookModel> Search(int id)
        {
            //abrindo o canal com o banco e fazendo a busca
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM BOOK WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();

            List<BookModel> book = new List<BookModel>(id);

            try
            {
                NpgsqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    BookModel book1 = new BookModel();
                    book1.Id = (int)data["id"];
                    book1.Title = (string)data["title"];
                    book1.Author = (string)data["author"];
                    book1.Release = (int)data["release"];
                    book1.PublishingHouse = (string)data["publishing"];
                    book1.Category = (string)data["category"];
                    book1.Description = (string)data["description"];
                    book.Add(book1);
                }
            }
            finally
            {
                conn.Close();
            }
            return book;
        }

        //métodos CRUD - retornar a tabela inteira

        [HttpGet]
        [Route("GetAllData")]
        public List <BookModel> GetAllData()
        {

            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");
            conn.Open();

            NpgsqlCommand exibeAll = new NpgsqlCommand("select * from book order by id asc", conn);

            List<BookModel> booklist = new List<BookModel>();
            //buscando dados
            try
            {
                NpgsqlDataReader dt = exibeAll.ExecuteReader();
                while (dt.Read())
                {
                    BookModel book = new BookModel();
                    book.Id = (int)dt["id"];
                    book.Title = (string)dt["title"];
                    book.Author = (string)dt["author"];
                    book.Release = (int)dt["release"];
                    book.PublishingHouse = (string)dt["publishing"];
                    book.Category = (string)dt["category"];
                    book.Description = (string)dt["description"];

                    booklist.Add(book);

                }

            }
            catch (Exception ex)
            {
                string teste = ex.Message;
            }

            finally
            {
                conn.Close();
            }
            return booklist;

        }

        //métodos CRUD - atualizar todos os campos de uma só vez
        [HttpPut]
        [Route("Update/{id}")]
        public void Update(int id)
        {
            BookModel book = new BookModel();
            HttpContent requestContent = Request.Content;
            string jsonContent = (requestContent.ReadAsStringAsync().Result);
            book = Newtonsoft.Json.JsonConvert.DeserializeObject<BookModel>(jsonContent);
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET TITLE=@title,AUTHOR=@author,RELEASE=@release,PUBLISHING=@publishing,CATEGORY=@category,DESCRIPTION=@description WHERE ID='" + id + "';";
            cmd.Parameters.AddWithValue("@title", book.Title);
            cmd.Parameters.AddWithValue("@author", book.Author);
            cmd.Parameters.AddWithValue("@release", book.Release);
            cmd.Parameters.AddWithValue("@publishing", book.PublishingHouse);
            cmd.Parameters.AddWithValue("@category", book.Category);
            cmd.Parameters.AddWithValue("@description", book.Description);
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        //métodos CRUD - atualizar campo: título da obra
        [HttpPut]
        [Route("UpdateTitle/{id}/{title}")]
        public void UpdateTitle(int id, string title)
        {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET TITLE='" + title + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        //métodos CRUD - atualizar campo: autor
        [HttpPut]
        [Route("UpdateAuthor/{id}/{author}")]
        public void UpdateAuthor(int id, string author)
        {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET AUTHOR='" + author + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        //métodos CRUD - atualizar campo: ano de lançamento
        [HttpPut]
        [Route("UpdateRelease/{id}/{release}")]
        public void UpdateRelease(int id, int release)
        {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET RELEASE='" + release + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        //métodos CRUD - atualizar campo: editora
        [HttpPut]
        [Route("UpdatePublishing/{id}/{publishingHouse}")]
        public void UpdatePublishing(int id, string publishingHouse)
        {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET PUBLISHING='" + publishingHouse + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        //métodos CRUD - atualizar campo: categoria
        [HttpPut]
        [Route("UpdateCategory/{id}/{publishingHouse}")]
        public void UpdateCategory(int id, string category)
        {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET CATEGORY='" + category + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        //métodos CRUD - atualizar campo: descrição
        [HttpPut]
        [Route("UpdateDescription/{id}/{description}")]
        public void UpdateDescription(int id, string description)
        {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE BOOK SET DESCRIPTION='" + description + "' WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        //deletar um registro
        [HttpDelete]
        [Route("Delete/{id}")]
        public void Delete(int id)
        {
            //abrindo o canal com o banco
            NpgsqlConnection conn = new NpgsqlConnection("Server=127.0.0.1;Port=5434;User Id=postgres;Password=admin;Database=challange;");

            //buscando o dado a partir da ID 
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM BOOK WHERE ID='" + id + "';";
            cmd.CommandType = CommandType.Text;

            conn.Open();
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    Console.WriteLine("Registro deletado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}