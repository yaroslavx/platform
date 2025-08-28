using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Platform.DataAccess.Postgres.Models;

namespace Platform.DataAccess.Postgres.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<LessonEntity>
{
    public void Configure(EntityTypeBuilder<LessonEntity> builder)
    {
        builder.HasKey( c => c.Id);

        builder
            .HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId);
    }
}