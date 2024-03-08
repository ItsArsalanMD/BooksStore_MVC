using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Books.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Category Name")]
        [MaxLength(25, ErrorMessage = "Category Name must be maximum 25 characters")]
        public string Name { get; set; }


        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int DisplayOrders { get; set; }

    }
}
