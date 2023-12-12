using Microsoft.AspNetCore.Identity;

namespace Entities.Models;

public sealed class AppUser : IdentityUser<Guid>
//sealed ile işaretlenen class başka bir class a inherit edilemez
{
    //guid byte bir anahtar türetir. bu anahtarı türetirken zaman faktörünü de kullanır.böylelikle uniq bir değer üretilmesini sağlar.
    public string Name { get; set; }
    public string Lastname { get; set; }
}