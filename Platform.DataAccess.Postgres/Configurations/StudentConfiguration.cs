using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.DataAccess.Postgres.Models;

namespace Platform.DataAccess.Postgres.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<StudentEntity>
{
    public void Configure(EntityTypeBuilder<StudentEntity> builder)
    {
        builder.HasKey( c => c.Id);

        builder
            .HasMany(s => s.Courses)
            .WithMany(c => c.Students);
    }
}