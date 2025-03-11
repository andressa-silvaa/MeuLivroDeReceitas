namespace MyRecipeBook.Domain.Entities;
public class EntityBase
{
    public int Id { get; set; }
    public bool Active { get; set; } = true;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
