using System.Net.Http;
using System.Threading.Tasks;

public class DeviceService
{
    private readonly HttpClient _httpClient;

    public DeviceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDeviceByIpAsync(string ip, string oidWrite)
    {

        try
        {
            var response = await _httpClient.GetAsync($"https://localhost:7055/devices?ip={ip}&oidWrite={oidWrite}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            throw new HttpRequestException($"Error retrieving device: {ex.Message}");
        }
    }

    public async Task<string> GetStaticsInfos(string ip)
    {

        try
        {
            var response = await _httpClient.GetAsync($"https://localhost:7055/devices/info?ip={ip}");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            throw new HttpRequestException($"Error retrieving device: {ex.Message}");
        }
    }
}
