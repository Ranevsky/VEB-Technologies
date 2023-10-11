namespace Identity.Domain.Entities;

public class RoleEntity
{
    public const string DefaultRole = "User";
    public const string SuperAdmin = "SuperAdmin";
    
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public ICollection<UserEntity> Users { get; set; } = null!;
}