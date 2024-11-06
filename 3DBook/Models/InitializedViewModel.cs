using _3DBook.Models.UserAggregate;

namespace _3DBook.Models;

public class InitializedViewModel
{
    public bool UserExist { get; set; }
    public bool RolesExist { get; set; }
    public CreateUserViewModel UserModel { get; set; }
    public CreateRoleViewModel RoleModel { get; set; }
}