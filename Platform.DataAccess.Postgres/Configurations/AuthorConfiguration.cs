using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.DataAccess.Postgres.Models;

namespace Platform.DataAccess.Postgres.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
{
    public void Configure(EntityTypeBuilder<AuthorEntity> builder)
    {
        builder.HasKey(a => a.Id);

        builder
            .HasOne(a => a.Course)
            .WithOne(c => c.Author)
            .HasForeignKey<CourseEntity>(a => a.AuthorId);
    }
}