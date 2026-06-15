# 🚀 Instruções de Setup - User Management API

## 1️⃣ Criar a Tabela no PostgreSQL

Abra uma ferramenta de acesso ao PostgreSQL (pgAdmin, DBeaver, psql) e execute o SQL:

```sql
-- Conecte-se ao banco "mydatabase"
-- Use as credenciais: myuser / mypassword

CREATE TABLE users (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name VARCHAR(255) NOT NULL,
    username VARCHAR(100) NOT NULL UNIQUE,
    password_hash TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    deleted_at TIMESTAMP NULL
);

CREATE INDEX idx_users_username ON users(username);
CREATE INDEX idx_users_created_at ON users(created_at DESC);
```

## 2️⃣ Executar a API

No terminal (dentro da pasta `UserManagementAPI`):

```bash
cd UserManagementAPI.API
dotnet run
```

A API iniciará em: **https://localhost:5001**

O Swagger UI estará disponível em: **https://localhost:5001/**

## 3️⃣ Testar os Endpoints

### Criar Usuário (POST)

```bash
curl -X POST "https://localhost:5001/api/users" \
  -H "Content-Type: application/json" \
  -d '{"name":"João Silva","username":"joao.silva","password":"senha123"}'
```

### Listar Usuários (GET)

```bash
curl "https://localhost:5001/api/users"
```

## 📋 Arquitetura da Solução

- **Domain**: Entidades e Interfaces (Ports)
- **Application**: Casos de Uso (Commands e Queries)
- **Infrastructure**: Implementações (Adapters - Repository, DbConnectionFactory, PasswordHasher)
- **API**: Controllers e Configuração

## 🔧 Modificar Credenciais do Banco

Edite o arquivo `UserManagementAPI.API/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=seu-host;Port=5432;Database=seu-db;Username=seu-usuario;Password=sua-senha"
  }
}
```

## ⚡ Próximos Passos

1. Implementar endpoint de Login com JWT
2. Adicionar validações de entrada
3. Criar testes unitários
4. Implementar filtros e buscas avançadas
