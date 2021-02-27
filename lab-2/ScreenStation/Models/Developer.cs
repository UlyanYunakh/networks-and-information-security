using System.ComponentModel.DataAnnotations;

namespace ScreenStation.Models
{
    public class Developer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public Country Country { get; set; }
    }
}