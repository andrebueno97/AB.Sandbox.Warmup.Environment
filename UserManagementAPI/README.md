# User Management API

Uma API Web em C# com .NET 10 para gerenciamento de usuários, implementada usando **Arquitetura Hexagonal** (Ports and Adapters).

## Arquitetura

A solução está organizada em 4 camadas:

- **Domain** (Core): Entidades, interfaces (ports) e lógica de negócio
- **Application**: Casos de uso (Commands e Queries)
- **Infrastructure**: Implementações dos ports (Repository, DbConnectionFactory, PasswordHasher)
- **API**: Controllers e configuração do servidor web

## Estrutura de Projetos

```
UserManagementAPI/
├── UserManagementAPI.Domain/
│   ├── Entities/
│   │   └── User.cs
│   └── Ports/
│       ├── IUserRepository.cs
│       └── IPasswordHasher.cs
├── UserManagementAPI.Application/
│   ├── Commands/
│   │   ├── CreateUserCommand.cs
│   │   └── CreateUserCommandHandler.cs
│   └── Queries/
│       ├── GetAllUsersQuery.cs
│       └── GetAllUsersQueryHandler.cs
├── UserManagementAPI.Infrastructure/
│   ├── Ports/
│   │   └── IDbConnectionFactory.cs
│   └── Adapters/
│       ├── DbConnectionFactory.cs
│       ├── UserRepository.cs
│       └── PasswordHasher.cs
├── UserManagementAPI.API/
│   ├── Controllers/
│   │   └── UsersController.cs
│   ├── Program.cs
│   └── appsettings.json
└── database-schema.sql
```

## Pré-requisitos

- .NET 10 SDK instalado
- PostgreSQL rodando (utilize o docker-compose da pasta pai)
- Visual Studio Code ou Visual Studio

## Como Executar

### 1. Criar o Banco de Dados

Execute o SQL em `database-schema.sql` no PostgreSQL:

```sql
-- Conecte-se ao PostgreSQL com as credenciais do seu .env
-- Copie e cole o conteúdo de database-schema.sql
```

### 2. Restaurar Dependências e Compilar

```bash
cd UserManagementAPI
dotnet restore
dotnet build
```

### 3. Executar a API

```bash
cd UserManagementAPI.API
dotnet run
```

A API estará disponível em: `https://localhost:5001`

O Swagger UI estará em: `https://localhost:5001/` (raiz)

## Endpoints Disponíveis

### POST /api/users
Cria um novo usuário

**Request Body:**
```json
{
  "name": "João Silva",
  "username": "joao.silva",
  "password": "minha_senha_segura"
}
```

**Response:**
```json
"550e8400-e29b-41d4-a716-446655440000"
```

### GET /api/users
Obtém todos os usuários cadastrados

**Response:**
```json
[
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "name": "João Silva",
    "username": "joao.silva",
    "createdAt": "2026-06-12T22:30:00Z",
    "updatedAt": "2026-06-12T22:30:00Z"
  }
]
```

## Funcionalidades Implementadas

✅ Criar usuário com hash de senha (PBKDF2 + SHA256)  
✅ Listar todos os usuários  
✅ Arquitetura Hexagonal com separação clara de responsabilidades  
✅ Injeção de Dependência configurada  
✅ Documentação OpenAPI (Swagger)  
✅ Integração com PostgreSQL usando Dapper  
✅ Primary Constructors (C# 12)  
✅ File-scoped namespaces  

## Funcionalidades Planejadas

🔜 Endpoint de Login com JWT  
🔜 Validações de entrada  
🔜 Testes unitários  
🔜 Filtros customizados  

## Configuração de Conexão

A string de conexão padrão é:

```
Host=localhost;Port=5432;Database=mydatabase;Username=myuser;Password=mypassword
```

Edite `appsettings.json` para alterar as credenciais.

## Tecnologias Utilizadas

- **.NET 10** - Framework
- **ASP.NET Core** - Web API
- **PostgreSQL** - Banco de dados
- **Dapper** - ORM leve
- **Npgsql** - Driver PostgreSQL
- **Swashbuckle.AspNetCore** - Swagger/OpenAPI
