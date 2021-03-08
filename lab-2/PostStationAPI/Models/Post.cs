using System.ComponentModel.DataAnnotations;

namespace PostStationAPI.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(280)]
        [Required]
        public string Text { get; set; }

        public int? GameId { get; set; }
        public Game Game { get; set; }
    }
}