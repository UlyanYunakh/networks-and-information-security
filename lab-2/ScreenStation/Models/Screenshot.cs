using System.ComponentModel.DataAnnotations;

namespace ScreenStation.Models
{
    public class Screenshot
    {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        public byte[] Image { get; set; }

        public Game Game { get; set; }
    }
}