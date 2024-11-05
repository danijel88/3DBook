using Microsoft.AspNetCore.Identity;

namespace _3DBook.Core;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Company { get; set; }
    public bool IsActive { get; set; }
}