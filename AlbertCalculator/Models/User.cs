using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbertCalculator.Models
{
    [Table("Users", Schema = "albercalculatorschema")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(40)]
        public required string Name { get; set; }

        [Required]
        [MaxLength(40)]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }

        public virtual ICollection<CustomCategory> CustomCategories { get; set; }
    }
}
