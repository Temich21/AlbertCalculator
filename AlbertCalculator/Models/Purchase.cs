using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbertCalculator.Models
{
    [Table("Purchase", Schema = "albercalculatorschema")]
    public class Purchase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual required User User { get; set; }

        public virtual required ICollection<PurchaseProducts> PurchaseProducts { get; set; }
    }
}
