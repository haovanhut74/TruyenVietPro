using System.Net.Http.Json;
using TruyenHayPro.Application.Common;
using TruyenHayPro.Application.Contracts.Category;
using TruyenHayPro.Application.DTO.Admin;

namespace TruyenHayPro.Web.Services;

public class CategoryApiService
{
    private readonly HttpClient _http;

    public CategoryApiService(HttpClient http)
    {
        _http = http;
    }

    // GET list admin
    public async Task<PagedResult<CategoryAdminDto>> GetListAsync(CategoryListQuery query)
    {
        var url = $"api/admin/categories?page={query.Page}&pageSize={query.PageSize}";

        if (!string.IsNullOrWhiteSpace(query.Q))
            url += $"&q={Uri.EscapeDataString(query.Q)}";

        var res = await _http.GetFromJsonAsync<PagedResult<CategoryAdminDto>>(url);
        return res ?? new PagedResult<CategoryAdminDto>(
            [],
            0,
            query.Page,
            query.PageSize
        );
    }

    // GET detail
    public Task<CategoryAdminDto?> GetByIdAsync(Guid id)
        => _http.GetFromJsonAsync<CategoryAdminDto?>($"api/admin/categories/{id}");

    // Create
    public async Task<Guid?> CreateAsync(CategoryCreateRequest req)
    {
        var res = await _http.PostAsJsonAsync("api/admin/categories", req);

        if (!res.IsSuccessStatusCode) return null;

        var location = res.Headers.Location?.ToString();
        if (location != null && Guid.TryParse(location.Split('/').Last(), out var id))
            return id;

        return null;
    }

    // Update
    public async Task<bool> UpdateAsync(Guid id, CategoryUpdateRequest req)
    {
        var res = await _http.PutAsJsonAsync($"api/admin/categories/{id}", req);
        return res.IsSuccessStatusCode;
    }

    // Delete
    public async Task<bool> DeleteAsync(Guid id)
    {
        var res = await _http.DeleteAsync($"api/admin/categories/{id}");
        return res.IsSuccessStatusCode;
    }
}