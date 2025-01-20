
using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data;

public class ApplicationDbContext : IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly string _connection;
    private DbConnection? _db;
    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connection = _configuration.GetConnectionString("DefaultConnection")!;
    }

    //TODO : Add a method to get the connection
    public DbConnection GetConnection()
    {
        if (_db == null || _db.State != System.Data.ConnectionState.Open)
        {
            _db = new SqlConnection(_connection);
            _db.Open();
        }
        return _db;
    }

    // TODO: Add a method to create the database and tables
    public void GenerateDatabase()
    {
        using var command = GetConnection().CreateCommand();
        command.CommandText =
        """
        IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'UserService')
        BEGIN
            CREATE DATABASE UserService;
        END
        USE UserService;
        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
        BEGIN
            CREATE TABLE Users (
                id CHAR(36) PRIMARY KEY,
                name VARCHAR(255) NOT NULL,
                email VARCHAR(255) UNIQUE NOT NULL,
                password_hash VARCHAR(255) NOT NULL,
                phone VARCHAR(20),
                address TEXT,
                is_active BIT DEFAULT 1,
                last_login DATETIME,
                created_at DATETIME DEFAULT GETDATE(),
                updated_at DATETIME
            );
        END;
        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Roles')
        BEGIN
            CREATE TABLE Roles (
                role_id CHAR(36) PRIMARY KEY,
                role_name VARCHAR(50) UNIQUE NOT NULL
            );
        END;
        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'UserRoles')
        BEGIN
            CREATE TABLE UserRole (
                user_id INT NOT NULL,
                role_id INT NOT NULL,
                asigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                PRIMARY KEY (user_id, role_id),
                FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE CASCADE,
                FOREIGN KEY (role_id) REFERENCES Roles(role_id) ON DELETE CASCADE
            );
        END;
        """;
        command.ExecuteNonQuery();
    }

    //TODO : Add a method to close the connection
    public void Dispose()
    {
        if (_db != null && _db.State == System.Data.ConnectionState.Open)
        {
            _db.Close();
            _db.Dispose();
        }
    }
}