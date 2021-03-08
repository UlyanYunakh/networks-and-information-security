using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostStationAPI.Models
{
    public class Platform
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}