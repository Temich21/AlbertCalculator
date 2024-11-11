using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace AlbertCalculator.Models
{
    [Table("ProductsCategories", Schema = "trelloschema")]
    public class ProductsCategories
    {
        [Required]
        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual required Product Product { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual required Category Category { get; set; }
    }
}
