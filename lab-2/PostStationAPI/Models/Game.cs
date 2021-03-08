using System.ComponentModel.DataAnnotations;

namespace PostStationAPI.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? DeveloperId { get; set; }
        public Developer Developer { get; set; }

        public int? PlatformId { get; set; }
        public Platform Platform { get; set; }
    }
}