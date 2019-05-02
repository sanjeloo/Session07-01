using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Session7
{
    public class stringGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false ;

        public override string Next(EntityEntry entry)
        {
            return "not implemented";
        }
    }
    public class DateTimeValueGenerator : ValueGenerator<DateTime>
    {
        public override bool GeneratesTemporaryValues => false;

        public override DateTime Next( EntityEntry entry)
        {
            return DateTime.Now.AddYears(-10);
        }
    }
    public class Person
    {
        public  int Id { get; set; }
        [ConcurrencyCheck]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Address Home { get; set; }
        public BacnkAccount acc { get; set; }
    }
    public class PersonContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<BacnkAccount> accounts { get; set; }
        
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=.;initial catalog=PrsonContext; integrated security= true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(() => DatabaseFunction.MyFunction());
            modelBuilder.Entity<Person>().OwnsOne(c => c.Home);
            modelBuilder.Entity<Person>().ToTable("People");
            modelBuilder.Entity<BacnkAccount>().ToTable("People");
            modelBuilder.Entity<Person>().HasOne(c => c.acc).WithOne(c => c.Person).HasForeignKey<Person>(c => c.Id);
            modelBuilder.Entity<Person>().Property(c => c.BirthDate).HasValueGenerator(typeof(DateTimeValueGenerator));
            modelBuilder.Entity<Person>().Property(c => c.FirstName).HasValueGenerator(typeof(stringGenerator));
            modelBuilder.Entity<Person>().Property(c => c.FirstName).IsConcurrencyToken();
            //modelBuilder.Entity<Person>().Property(c => c.BirthDate).HasDefaultValueSql("your query is here");
           // modelBuilder.Entity<Person>().HasDiscriminator<int>("Discriminator")
              //  .HasValue<Person>(1).HasValue<Teacher>(2);
        }

    }
}
