# Project Task List

## Task Table

| **ID** | **Task Description**                      | **Category**         | **Estimated Time (days)** | **Actual Time (days)** | **Delay (days)** | **Notes** |
| ------ | ----------------------------------------- | -------------------- | ------------------------- | ---------------------- | ---------------- | --------- |
| 1      | Design database schema in 3NF             | Database Design      | 5                         |           4             |         0         |           |
| 2      | Write database migrations                 | Database Design      | 3                         |           1             |         0         |           |
| 3      | Create user management microservice       | Backend Development  | 5                         |                        |                  |           |
| 4      | Create customer service microservice      | Backend Development  | 7                         |                        |                  |           |
| 5      | Create agent service microservice         | Backend Development  | 7                         |                        |                  |           |
| 6      | Create ticket management service          | Backend Development  | 7                         |                        |                  |           |
| 7      | Create communication service microservice | Backend Development  | 6                         |                        |                  |           |
| 8      | Implement JWT authentication              | Backend Development  | 4                         |                        |                  |           |
| 9      | Set up API Gateway                        | Backend Development  | 3                         |                        |                  |           |
| 10     | Create login and registration UI          | Frontend Development | 4                         |                        |                  |           |
| 11     | Implement customer ticket management UI   | Frontend Development | 6                         |                        |                  |           |
| 12     | Implement agent ticket management UI      | Frontend Development | 6                         |                        |                  |           |
| 13     | Add role-based access control in UI       | Frontend Development | 3                         |                        |                  |           |
| 14     | Write unit tests for backend services     | Testing              | 5                         |                        |                  |           |
| 15     | Write unit tests for frontend components  | Testing              | 5                         |                        |                  |           |
| 16     | Perform end-to-end testing                | Testing              | 5                         |                        |                  |           |


## Task Details

### Database Design

1. **Create Normalized Tables**:

- ERD

```mermaid
erDiagram
    Users {
        CHAR(36) id PK
        VARCHAR name
        VARCHAR email
        VARCHAR password_hash
        VARCHAR phone
        TEXT address
        BOOLEAN is_active
        TIMESTAMP last_login
        TIMESTAMP created_at
        TIMESTAMP updated_at
    }

    Roles {
        CHAR(36) role_id PK
        VARCHAR role_name
    }

    UserRole {
        CHAR(36) user_id PK
        CHAR(36) role_id PK
        TIMESTAMP asigned_at
    }

    Customers {
        CHAR(36) customer_id PK
        CHAR(36) user_id
        INT loyalty_points
        CHAR(36) type_id
    }

    CustomerTypes {
        CHAR(36) type_id PK
        VARCHAR type_name
    }

    Agents {
        CHAR(36) agent_id PK
        CHAR(36) user_id
    }

    AgentRole {
        CHAR(36) agent_id PK
        CHAR(36) role_id PK
        TIMESTAMP asigned_at
    }

    Departments {
        CHAR(36) department_id PK
        VARCHAR department_name
    }

    AgentDepartment {
        CHAR(36) agent_id PK
        CHAR(36) department_id PK
        TIMESTAMP asigned_at
    }

    Tickets {
        CHAR(36) ticket_id PK
        CHAR(36) customer_id
        VARCHAR subject
        TEXT description
        CHAR(36) status_id
        CHAR(36) priority_id
        TIMESTAMP created_at
        TIMESTAMP updated_at
    }

    Ticket_Statuses {
        CHAR(36) status_id PK
        VARCHAR status_name
    }

    Ticket_Priorities {
        CHAR(36) priority_id PK
        VARCHAR priority_name
    }

    Ticket_Assignments {
        CHAR(36) assignment_id PK
        CHAR(36) ticket_id
        CHAR(36) agent_id
        TIMESTAMP assigned_at
    }

    Communication_Logs {
        CHAR(36) log_id PK
        CHAR(36) ticket_id
        CHAR(36) agent_id
        TEXT message
        CHAR(36) type_id
        TIMESTAMP created_at
    }

    Communication_Types {
        CHAR(36) type_id PK
        VARCHAR type_name
    }

    Users ||--o| UserRole: "has"
    Roles ||--o| UserRole: "assigned to"
    Users ||--o| Customers: "owns"
    CustomerTypes ||--o| Customers: "categorized as"
    Users ||--o| Agents: "is assigned as"
    Roles ||--o| AgentRole: "assigned to"
    Agents ||--o| AgentRole: "has role"
    Departments ||--o| AgentDepartment: "has agents"
    Agents ||--o| AgentDepartment: "belongs to"
    Customers ||--o| Tickets: "creates"
    Ticket_Statuses ||--o| Tickets: "categorized as"
    Ticket_Priorities ||--o| Tickets: "categorized as"
    Agents ||--o| Ticket_Assignments: "assigned to"
    Tickets ||--o| Ticket_Assignments: "assigned to"
    Agents ||--o| Communication_Logs: "logs"
    Tickets ||--o| Communication_Logs: "logged for"
    Communication_Types ||--o| Communication_Logs: "categorized as"

```

2. **Migrate the Database**:

- User table

```sql
CREATE TABLE Users (
    id CHAR(36) PRIMARY KEY
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    phone VARCHAR(20),
    address TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    last_login TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP
);
```

- Role table

```sql
CREATE TABLE Roles (
  role_id CHAR(36) PRIMARY KEY
  role_name VARCHAR(50) UNIQUE NOT NULL
);

```
- UserRole table
```sql
CREATE TABLE UserRole (
    user_id INT NOT NULL,
    role_id INT NOT NULL,
    asigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id, role_id),
    FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE CASCADE,
    FOREIGN KEY (role_id) REFERENCES Roles(role_id) ON DELETE CASCADE
);

```
- Customer table

```sql
CREATE TABLE Customers (
    customer_id CHAR(36) PRIMARY KEY
    user_id INT NOT NULL,
    loyalty_points INT DEFAULT 0,
    type_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (type_id) REFERENCES Customer_Types(type_id)
);

```

- Customer Type table

```sql
CREATE TABLE CustomerTypes (
    type_id CHAR(36) PRIMARY KEY
    type_name VARCHAR(50) UNIQUE NOT NULL
);
```

- Agents table

```sql
CREATE TABLE Agents (
    agent_id CHAR(36) PRIMARY KEY
    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (role_id) REFERENCES Roles(role_id),
    FOREIGN KEY (department_id) REFERENCES Departments(department_id)
);

```
- Agent Roles table
```sql
CREATE TABLE Roles (
  role_id CHAR(36) PRIMARY KEY
  role_name VARCHAR(50) UNIQUE NOT NULL
);
```
- AgentRole table
```sql
CREATE TABLE AgentRole (
    agent_id INT NOT NULL,
    role_id INT NOT NULL,
    asigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (agent_id, role_id),
    FOREIGN KEY (agent_id) REFERENCES Agentsx(id) ON DELETE CASCADE,
    FOREIGN KEY (role_id) REFERENCES Roles(role_id) ON DELETE CASCADE
);

```
- Department table

```sql
CREATE TABLE Departments (
    department_id CHAR(36) PRIMARY KEY
    department_name VARCHAR(255) NOT NULL UNIQUE
);

```
- AgentDepartments Table

```sql
CREATE TABLE AgentDepartment (
    agent_id INT NOT NULL,
    department_id INT NOT NULL,
    asigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (agent_id, department_id),
    FOREIGN KEY (agent_id) REFERENCES Agentsx(id) ON DELETE CASCADE,
    FOREIGN KEY (department_id) REFERENCES Departments(department_id) ON DELETE CASCADE
);

```
- Ticket Table

```sql
CREATE TABLE Tickets (
    ticket_id CHAR(36) PRIMARY KEY
    customer_id INT NOT NULL,
    subject VARCHAR(255) NOT NULL,
    description TEXT,
    status_id INT NOT NULL,
    priority_id INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id) ON DELETE CASCADE,
    FOREIGN KEY (status_id) REFERENCES Ticket_Statuses(status_id),
    FOREIGN KEY (priority_id) REFERENCES Ticket_Priorities(priority_id)
);

```

- Ticket Status table

```sql
CREATE TABLE Ticket_Statuses (
    status_id CHAR(36) PRIMARY KEY
    status_name VARCHAR(50) UNIQUE NOT NULL
);
```

- Ticket Priority

```sql
CREATE TABLE Ticket_Priorities (
    priority_id CHAR(36) PRIMARY KEY
    priority_name VARCHAR(50) UNIQUE NOT NULL
);
```

- Ticket Assignment table

```sql
CREATE TABLE Ticket_Assignments (
    assignment_id CHAR(36) PRIMARY KEY
    ticket_id INT NOT NULL,
    agent_id INT NOT NULL,
    assigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ticket_id) REFERENCES Tickets(ticket_id) ON DELETE CASCADE,
    FOREIGN KEY (agent_id) REFERENCES Agents(agent_id) ON DELETE CASCADE
);
```

- Communication Log table

```sql
CREATE TABLE Communication_Logs (
    log_id CHAR(36) PRIMARY KEY
    ticket_id INT NOT NULL,
    agent_id INT,
    message TEXT NOT NULL,
    type_id INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (ticket_id) REFERENCES Tickets(ticket_id) ON DELETE CASCADE,
    FOREIGN KEY (agent_id) REFERENCES Agents(agent_id) ON DELETE CASCADE,
    FOREIGN KEY (type_id) REFERENCES Communication_Types(type_id)
);
```

- Communication Type table

```sql
CREATE TABLE Communication_Types (
    type_id CHAR(36) PRIMARY KEY
    type_name VARCHAR(50) UNIQUE NOT NULL
);
```
### Backend Development

#### User Management Service

- Create APIs for:
  - User registration and login.
  - JWT-based authentication.
  - Role management (assigning roles like agent or customer).
- Sequence Diagram
```mermaid
sequenceDiagram
    actor User
    participant System
    participant UsersDB as Users
    participant RolesDB as Roles
    participant UserRoleDB as UserRole
    participant JWTService as "JWT Service"

    User->>System: Provide LoginDto
    System->>UsersDB: Query user by email
    UsersDB-->>System: Return user details
    
    alt If user found
        System->>System: Validate password (compare with password_hash)
        
        alt If password valid
            System->>UserRoleDB: Query user roles based on user_id
            UserRoleDB-->>System: Return user roles (role_id)
            System->>RolesDB: Query role details based on role_id
            RolesDB-->>System: Return role details (role_name)
            System->>JWTService: Generate JWT token for user
            JWTService-->>System: Return JWT token
            System->>User: Return JWT token for access
        else If password invalid
            System->>User: Display login failure message
        end
    else If user not found
        System->>User: Display login failure message
    end

```