using UserManagementAPI.Application.Commands;
using UserManagementAPI.Application.Queries;
using UserManagementAPI.Domain.Ports;
using UserManagementAPI.Infrastructure.Adapters;
using UserManagementAPI.Infrastructure.Ports;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar a string de conexão do banco de dados
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Host=localhost;Port=5432;Database=mydatabase;Username=myuser;Password=mypassword";

// Registrar dependências
builder.Services.AddSingleton<IDbConnectionFactory>(new DbConnectionFactory(connectionString));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<CreateUserCommandHandler>();
builder.Services.AddScoped<UpdateUserCommandHandler>();
builder.Services.AddScoped<DeleteUserCommandHandler>();
builder.Services.AddScoped<GetAllUsersQueryHandler>();
builder.Services.AddScoped<GetUserQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API V1");
        c.RoutePrefix = string.Empty; // Define o Swagger como página raiz
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
