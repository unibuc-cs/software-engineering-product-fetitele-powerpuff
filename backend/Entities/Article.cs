using System.ComponentModel.DataAnnotations;

namespace healthy_lifestyle_web_app.Entities
{
    public class Article
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } 

        public string Author { get; set; } 

        public string Content { get; set; } 
    }
}
