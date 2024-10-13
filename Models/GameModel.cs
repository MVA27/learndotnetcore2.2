using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace learndotnet.Models
{
    public class GameModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // to autogenerate id
        public int Id { get; set; }

        public string Name { get; set; }

        public string Device { get; set; }

        public string HashKey { get; set; }
    }
}
