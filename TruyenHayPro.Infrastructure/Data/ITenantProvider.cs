namespace TruyenHayPro.Infrastructure.Data;

public interface ITenantProvider
{
    Guid? GetCurrentTenantId();
}