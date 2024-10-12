using System.ComponentModel.DataAnnotations;

namespace learndotnet.Models.Dto
{
    public class GamesDto
    {
        public int Id { get; set; }

        // Data Validation for Model
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

    }
}