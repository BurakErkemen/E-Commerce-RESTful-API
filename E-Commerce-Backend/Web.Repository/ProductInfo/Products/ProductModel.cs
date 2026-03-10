using Web.Repository.OrderSection.BasketItem;
using Web.Repository.ProductInfo.Categories;
using Web.Repository.ProductInfo.Reviews;

namespace Web.Repository.ProductInfo.Products;

public class ProductModel
{
    public int ProductID { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public int ProductStock { get; set; }
    public string ProductStockCode { get; set; } = default!;
    public List<string>? ProductImg { get; set; } = default!;
    public int CategoryId { get; set; }  // ForeignKey olarak

    // Navigation property
    public virtual CategoryModel? Category { get; set; } 
    public List<ReviewModel>? Reviews { get; set; } = [];
    public ICollection<BasketItemModel> BasketItems { get; set; } = [];

}