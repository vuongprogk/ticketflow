namespace Domain.Models;

public class User
{
  public string Id { get; set; } = Guid.NewGuid().ToString();
  public string Name { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string PasswordHash { get; set; } = string.Empty;
  public string Phone { get; set; } = string.Empty;
  public string Address { get; set; } = string.Empty;
  public bool IsActive { get; set; } = true;
  public DateTime? LastLogin { get; set; } = null;
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime? UpdatedAt { get; set; } = null;
}