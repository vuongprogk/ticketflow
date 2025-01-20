﻿namespace Domain.Models;

public class UserRole
{
  public int UserId { get; set; }
  public int RoleId { get; set; }
  public DateTime AssignedAt { get; set; } = DateTime.UtcNow;
}