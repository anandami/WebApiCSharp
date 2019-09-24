using System;


namespace WebAPI.Models {
    public class BookModel {
        //atributos
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Release { get; set; }
        public string PublishingHouse { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public BookModel() {

        }
        
        public BookModel(int id, string title, string author, int release, string publishingHouse, string category, string description) {
            Id = id;
            Title = title;
            Author = author;
            Release = release;
            PublishingHouse = publishingHouse;
            Category = category;
            Description = description;
        }
        public class ApiModel
        {
            public string Content { get; set; }
        }
    }
}