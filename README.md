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
        INT id PK
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
        INT role_id PK
        VARCHAR role_name
    }

    UserRole {
        INT user_id PK
        INT role_id PK
        TIMESTAMP asigned_at
    }

    Customers {
        INT customer_id PK
        INT user_id
        INT loyalty_points
        INT type_id
    }

    CustomerTypes {
        INT type_id PK
        VARCHAR type_name
    }

    Agents {
        INT agent_id PK
        INT user_id
    }

    AgentRole {
        INT agent_id PK
        INT role_id PK
        TIMESTAMP asigned_at
    }

    Departments {
        INT department_id PK
        VARCHAR department_name
    }

    AgentDepartment {
        INT agent_id PK
        INT department_id PK
        TIMESTAMP asigned_at
    }

    Tickets {
        INT ticket_id PK
        INT customer_id
        VARCHAR subject
        TEXT description
        INT status_id
        INT priority_id
        TIMESTAMP created_at
        TIMESTAMP updated_at
    }

    Ticket_Statuses {
        INT status_id PK
        VARCHAR status_name
    }

    Ticket_Priorities {
        INT priority_id PK
        VARCHAR priority_name
    }

    Ticket_Assignments {
        INT assignment_id PK
        INT ticket_id
        INT agent_id
        TIMESTAMP assigned_at
    }

    Communication_Logs {
        INT log_id PK
        INT ticket_id
        INT agent_id
        TEXT message
        INT type_id
        TIMESTAMP created_at
    }

    Communication_Types {
        INT type_id PK
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
    id INT AUTO_INCREMENT PRIMARY KEY,
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
  role_id INT AUTO_INCREMENT PRIMARY KEY,
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
    customer_id INT AUTO_INCREMENT PRIMARY KEY,
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
    type_id INT AUTO_INCREMENT PRIMARY KEY,
    type_name VARCHAR(50) UNIQUE NOT NULL
);
```

- Agents table

```sql
CREATE TABLE Agents (
    agent_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    FOREIGN KEY (user_id) REFERENCES Users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (role_id) REFERENCES Roles(role_id),
    FOREIGN KEY (department_id) REFERENCES Departments(department_id)
);

```
- Agent Roles table
```sql
CREATE TABLE Roles (
  role_id INT AUTO_INCREMENT PRIMARY KEY,
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
    department_id INT AUTO_INCREMENT PRIMARY KEY,
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
    ticket_id INT AUTO_INCREMENT PRIMARY KEY,
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
    status_id INT AUTO_INCREMENT PRIMARY KEY,
    status_name VARCHAR(50) UNIQUE NOT NULL
);
```

- Ticket Priority

```sql
CREATE TABLE Ticket_Priorities (
    priority_id INT AUTO_INCREMENT PRIMARY KEY,
    priority_name VARCHAR(50) UNIQUE NOT NULL
);
```

- Ticket Assignment table

```sql
CREATE TABLE Ticket_Assignments (
    assignment_id INT AUTO_INCREMENT PRIMARY KEY,
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
    log_id INT AUTO_INCREMENT PRIMARY KEY,
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
    type_id INT AUTO_INCREMENT PRIMARY KEY,
    type_name VARCHAR(50) UNIQUE NOT NULL
);
```
