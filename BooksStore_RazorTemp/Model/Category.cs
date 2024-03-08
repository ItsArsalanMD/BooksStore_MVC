using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BooksStore_RazorTemp.Model
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
