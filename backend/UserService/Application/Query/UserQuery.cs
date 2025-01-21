using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query
{
    public static class UserQuery
    {
        public static string GetAllUser = "SELect * from Users";
        public static string AddUser = @"
            INSERT INTO Users (Id, Name, Email, PasswordHash, Phone, Address, IsActive, LastLogin, CreatedAt, UpdatedAt)
            OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.Email, INSERTED.Phone, INSERTED.Address, INSERTED.IsActive, INSERTED.LastLogin, INSERTED.CreatedAt, INSERTED.UpdatedAt
            VALUES (@Id, @Name, @Email, @PasswordHash, @Phone, @Address, @IsActive, @LastLogin, @CreatedAt, @UpdatedAt);
        ";
        public static string GetById = @"
            SELECT Id, Name, Email, PasswordHash, Phone, Address, IsActive, LastLogin, CreatedAt, UpdatedAt
            FROM Users
            WHERE Id = @Id;
        ";
        public static string Update = @"
            UPDATE Users
            SET 
                Name = @Name, 
                Email = @Email, 
                PasswordHash = @PasswordHash, 
                Phone = @Phone, 
                Address = @Address, 
                IsActive = @IsActive, 
                LastLogin = @LastLogin, 
                UpdatedAt = @UpdatedAt
            OUTPUT INSERTED.Id, INSERTED.Name, INSERTED.Email, INSERTED.Phone, INSERTED.Address, INSERTED.IsActive, INSERTED.LastLogin, INSERTED.CreatedAt, INSERTED.UpdatedAt
            WHERE Id = @Id;
        ";
        public static string Delete = @"
            DELETE FROM Users
            OUTPUT DELETED.Id, DELETED.Name, DELETED.Email, DELETED.Phone, DELETED.Address, DELETED.IsActive, DELETED.LastLogin, DELETED.CreatedAt, DELETED.UpdatedAt
            WHERE Id = @Id;
        ";

        public static string GetByEmail = @"
            SELECT Id, Name, Email, PasswordHash, Phone, Address, IsActive, LastLogin, CreatedAt, UpdatedAt
            FROM Users
            WHERE Email = @Email;
        ";
    }
}
