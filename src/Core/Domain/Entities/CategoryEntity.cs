namespace Domain.Entities;

public class CategoryEntity<T> : ManualEntity<T>,INaming
{
    public string? Name_az { get; set; }
    public string? Name_en { get; set; }
    public string? Name_ru { get; set; }
}