using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDo.App.Domain.Entities;
using ToDo.App.Domain.ValueObjects;

namespace ToDo.App.Infrastructure.Persistence.Configs
{
    internal class ToDoItemConfiguration : IEntityTypeConfiguration<ToDoItem>
    {
        public void Configure(EntityTypeBuilder<ToDoItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(a => a.Title).IsRequired();
            builder.Property(a => a.Note).IsRequired();

            builder.Property(t => t.PriorityLevel).HasConversion(src => src.Value, value => new PriorityLevel(value));
        }
    }
}
