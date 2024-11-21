using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AlbertCalculator.Models
{
    public class File
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string Data { get; set; }

        [Required]
        public Guid PurchaseId { get; set; }

        [ForeignKey("PurchaseId")]
        public virtual Purchase Purchase { get; set; }
    }
}
