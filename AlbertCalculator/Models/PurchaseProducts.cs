using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AlbertCalculator.Models
{
    [Table("PurchaseProducts", Schema = "albercalculatorschema")]
    public class PurchaseProducts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public Guid PurchaseId { get; set; }

        [ForeignKey("PurchaseId")]
        public virtual required Purchase Purchase { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual required Product Product { get; set; }
    }
}
