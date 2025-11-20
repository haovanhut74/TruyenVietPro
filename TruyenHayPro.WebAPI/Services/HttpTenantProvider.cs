using TruyenHayPro.Infrastructure.Data;


namespace TruyenHayPro.WebAPI.Services;

public class HttpTenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpTenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? GetCurrentTenantId()
    {
        var ctx = _httpContextAccessor.HttpContext;
        if (ctx == null) return null;

        // Ví dụ: đọc từ yêu cầu "người thuê nhà"
        var claim = ctx.User?.FindFirst("tenant")?.Value;
        if (Guid.TryParse(claim, out var tid)) return tid;

        // dự phòng: thử tiêu đề "X-Tenant-Id"
        if (ctx.Request.Headers.TryGetValue("X-Tenant-Id", out var headerVal))
        {
            if (Guid.TryParse(headerVal.FirstOrDefault(), out var htid)) return htid;
        }

        // nếu bạn chưa sử dụng multitenant, bạn có thể trả về id thuê mặc định hoặc null
        return null;
    }
}