using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlbertCalculator.Models
{
    public enum CategoryType
    {
        Custom,
        Default
    }

    [Table("Category", Schema = "albercalculatorschema")]
    public abstract class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public CategoryType CategoryType { get; set; }

        public Guid? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual required User User { get; set; }
    }

    public class CustomCategory : Category
    {
        public CustomCategory()
        {
            CategoryType = CategoryType.Custom;
        }
    }

    public class DefaultCategory : Category
    {
        public DefaultCategory()
        {
            CategoryType = CategoryType.Default;
        }
    }
}
