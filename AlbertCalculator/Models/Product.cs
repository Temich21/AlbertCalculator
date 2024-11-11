using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbertCalculator.Models
{
    [Table("Product", Schema = "albercalculatorschema")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public double Price { get; set; }

        public virtual required ICollection<PurchaseProducts> PurchaseProducts { get; set; }
    }
}
