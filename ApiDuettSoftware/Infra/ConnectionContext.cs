using ApiDuettSoftware.Model;
using ApiDuettSoftware.Model.Enum;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ApiDuettSoftware.Infra
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        private readonly IConfiguration _configuration;

        public ConnectionContext(DbContextOptions<ConnectionContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("User");

            modelBuilder.Entity<Person>()
                .Property(p => p.Type)
                .HasConversion<int>();

            modelBuilder.Entity<Person>()
                .HasIndex(p => p.Cpf)
                .IsUnique();

            modelBuilder.Entity<Person>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Person>().HasData(
      new Person
      {
          Id = 1,
          Name = "Pedro",
          Email = "pedro@email.com",
          Password = BCrypt.Net.BCrypt.HashPassword("12345678"),
          Cpf = "882.145.200-02",
          Type = PersonType.Admin
      },
      new Person
      {
          Id = 2,
          Name = "Maria",
          Email = "maria@email.com",
          Password = BCrypt.Net.BCrypt.HashPassword("4566743237"),
          Cpf = "360.266.275-61",
          Type = PersonType.User
      },
      new Person
      {
          Id = 3,
          Name = "Carlos",
          Email = "carlos@email.com",
          Password = BCrypt.Net.BCrypt.HashPassword("senha123"),
          Cpf = "123.456.789-01",
          Type = PersonType.User
      },
      new Person
      {
          Id = 4,
          Name = "Ana",
          Email = "ana@email.com",
          Password = BCrypt.Net.BCrypt.HashPassword("senha456"),
          Cpf = "234.567.890-12",
          Type = PersonType.User
      },
      new Person
      {
          Id = 5,
          Name = "João",
          Email = "joao@email.com",
          Password = BCrypt.Net.BCrypt.HashPassword("1234abcd"),
          Cpf = "345.678.901-23",
          Type = PersonType.User
      },
      new Person
      {
          Id = 6,
          Name = "Lucas",
          Email = "lucas@email.com",
          Password = BCrypt.Net.BCrypt.HashPassword("senha789"),
          Cpf = "456.789.012-34",
          Type = PersonType.User
      },
      new Person
      {
          Id = 7,
          Name = "Fernanda",
          Email = "fernanda@email.com",
          Password = BCrypt.Net.BCrypt.HashPassword("senha000"),
          Cpf = "567.890.123-45",
          Type = PersonType.User
      },
      new Person
      {
          Id = 8,
          Name = "Ricardo",
          Email = "ricardo@email.com",
          Password = BCrypt.Net.BCrypt.HashPassword("ricardo123"),
          Cpf = "678.901.234-56",
          Type = PersonType.User
      },
      new Person
      {
          Id = 9,
          Name = "Patrícia",
          Email = "patricia@email.com",
          Password = BCrypt.Net.BCrypt.HashPassword("senha987"),
          Cpf = "789.012.345-67",
          Type = PersonType.User
      },
      new Person
      {
          Id = 10,
          Name = "Marcos",
          Email = "marcos@email.com",
          Password = BCrypt.Net.BCrypt.HashPassword("marcos321"),
          Cpf = "890.123.456-78",
          Type = PersonType.User
      }
  );

        }

        public void EnsureDatabaseCreated()
        {
            Database.EnsureCreated();
        }
    }
}


