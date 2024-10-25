using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PensionHackathonBackend.Core.Models;

namespace PensionHackathonBackend.DataAccess.Configurations
{
    /* Конфигурация файла CSV */
    public class FileCSVConfiguration : IEntityTypeConfiguration<FileCSV>
    {
        public void Configure(EntityTypeBuilder<FileCSV> builder)
        {
            builder.HasKey(file => file.Id);

            builder.Property(file => file.FileName)
                .IsRequired();

            builder.Property(file => file.FilePath)
                .IsRequired();

            builder.Property(file => file.DateAdded)
                .IsRequired();
        }
    }
}
