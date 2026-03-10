namespace Web.Service.Categories.Create;
public record CreateCategoryRequest(
    string Name,
    string? Description,
    int? ParentCategoryId,
    List<int>? SubCategoryId
);