namespace Web.Service.Categories;
public record CategoryResponse(
    int CategoryId,
    string Name,
    string? Description,
    int? ParentCategoryId,
    List<int?> SubCategoryId
);
