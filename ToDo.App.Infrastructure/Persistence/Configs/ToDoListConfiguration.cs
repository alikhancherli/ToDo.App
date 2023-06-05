using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.App.Domain.Entities;

namespace ToDo.App.Infrastructure.Persistence.Configs
{
    public sealed class ToDoListConfiguration : IEntityTypeConfiguration<ToDoList>
    {
        public void Configure(EntityTypeBuilder<ToDoList> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(a => a.Title).IsRequired();
            builder.Property(a => a.Tag).IsRequired();
            builder.Property(a => a.UserId).IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a=>a.ToDoItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
