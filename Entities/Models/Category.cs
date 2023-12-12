using Entities.Abstractions;

namespace Entities.Models;

public sealed class Category : Entity
{
    public string Name { get; set; }

    public ICollection<Product>? Products { get; set; }//Nav prop
}