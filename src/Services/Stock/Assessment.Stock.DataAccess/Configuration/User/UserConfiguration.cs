using Assessment.Stock.Entities.Concrete.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment.Stock.DataAccess.Configuration.UserConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user", "public");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();
            builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
            builder.Property(d => d.Surname).HasColumnName("surname").HasMaxLength(150).IsRequired();
            builder.Property(d => d.Email).HasColumnName("email").HasMaxLength(150).IsRequired();
            builder.Property(d => d.Username).HasColumnName("username").HasMaxLength(50).IsRequired();
            builder.Property(d => d.Password).HasColumnName("password").HasMaxLength(50).IsRequired();
            builder.Property(d => d.Avatar).HasColumnName("avatar").HasMaxLength(250);
            builder.Property(d => d.Deleted).HasColumnName("deleted").IsRequired();

            builder.Property(d => d.PackageEndDate).HasColumnName("package_end_date");
            builder.Property(d => d.ApiCode).HasColumnName("api_code").HasMaxLength(400);

            builder.HasIndex(d => new { d.Id, d.Username }).IsUnique();
        }
    }
}
