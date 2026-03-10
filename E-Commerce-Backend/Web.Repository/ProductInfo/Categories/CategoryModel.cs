using Web.Repository.ProductInfo.Products;

namespace Web.Repository.ProductInfo.Categories
{
    public class CategoryModel
    {
        public int CategoryId { get; set; } // Benzersiz kategori kimliği
        public string Name { get; set; } = default!; // Kategori adı
        public string? Description { get; set; } // Opsiyonel: Kategori açıklaması
        public int? ParentCategoryId { get; set; } // Üst kategori (null ise ana kategori)
        public List<int>? SubCategoryId { get; set; }

        // İlişkiler
        public virtual CategoryModel? ParentCategory { get; set; } // Üst kategori referansı
        public virtual ICollection<CategoryModel>? SubCategories { get; set; } // Alt kategoriler
        public virtual ICollection<ProductModel>? Products { get; set; } // Kategorideki ürünler

        public DateTime CreatedDate { get; set; } // Kategori oluşturulma tarihi
        public DateTime? UpdatedDate { get; set; } // Opsiyonel: Kategori güncelleme tarihi
    }
}