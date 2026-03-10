namespace Web.Service.Categories.Update;
public record UpdateCategoryRequest(
    int CategoryId,
    string Name,
    string? Description,
    int? ParentCategoryId,
    List<int>? SubCategoryId
);