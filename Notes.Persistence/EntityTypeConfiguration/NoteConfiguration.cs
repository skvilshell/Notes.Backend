using Notes.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Notes.Persistence.EntityTypeConfiguration
{
    internal class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(note => note.Id);
            builder.HasIndex(note => note.Id).IsUnique();
            builder.Property(note => note.Title).IsRequired().HasMaxLength(250);
        }
    }
}
