using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Repository.UserInfo.Token
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenModel>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenModel> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Token).IsRequired();
            builder.Property(r => r.ExpiryDate).IsRequired();
            builder.HasIndex(r => r.Token).IsUnique();
        }
    }
}
