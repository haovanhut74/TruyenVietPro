using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TruyenHayPro.Infrastructure.Entities;

namespace TruyenHayPro.Infrastructure.Data.Config;

public class FailedJobConfiguration : IEntityTypeConfiguration<FailedJob>
{
    public void Configure(EntityTypeBuilder<FailedJob> b)
    {
        b.ToTable("FailedJobs");

        b.HasIndex(x => x.JobType);
    }
}
