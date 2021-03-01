using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostStation.Models
{
    public class Developer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }

        public List<Game> Games { get; set; } = new List<Game>();
    }
}