using DSC.Student.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DSC.Student.Infrastructure.Data.Mapping
{
    public class ObservacaoConfigurations : IEntityTypeConfiguration<NoteModel>
    {
        public void Configure(EntityTypeBuilder<NoteModel> builder)
        {

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Text)
                .IsRequired()
                .HasColumnType("varchar(8000)");

            builder.ToTable("Notes");

        }
    }
}
