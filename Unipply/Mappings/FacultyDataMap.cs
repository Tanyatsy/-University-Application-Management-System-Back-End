using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unipply.Models.Faculty;

namespace Xor.Core.Repositories.Mappings
{

    public class FacultyDataMap : IEntityTypeConfiguration<FacultyData>
    {
        public void Configure(EntityTypeBuilder<FacultyData> builder)
        {
            builder.ToTable("FacultyData");
            builder.HasMany(p => p.Specialties).WithOne(p => p.Faculty).HasForeignKey(p => p.FacultyId);
        }
    }
}