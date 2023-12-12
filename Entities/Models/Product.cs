using System;
using Entities.Abstractions;

namespace Entities.Models;

public sealed class Product : Entity
{
    public Guid CategoryId { get; set; }//FK

    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}