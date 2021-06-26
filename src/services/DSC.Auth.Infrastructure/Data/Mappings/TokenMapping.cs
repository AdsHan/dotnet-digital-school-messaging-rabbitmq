using DSC.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DSC.Auth.Infrastructure.Data.Mapping
{
    public class TokenMapping : IEntityTypeConfiguration<TokenModel>
    {
        public void Configure(EntityTypeBuilder<TokenModel> builder)
        {

            builder.HasKey(a => a.Id);

            builder.ToTable("Tokens");
        }
    }
}
