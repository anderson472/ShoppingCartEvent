using System.ComponentModel.DataAnnotations;

namespace Sales.Shared.Entities
{
    public class ProductCategory
    {

        public int Id { get; set; }

        [Display(Name = "Producto/Categoría")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Name { get; set; } = null!;

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<Product>? Products { get; set; }

        public int ProductsNumber => Products == null ? 0 : Products.Count;
    }
}
