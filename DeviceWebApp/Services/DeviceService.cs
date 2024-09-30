using System.Net.Http;
using System.Threading.Tasks;

public class DeviceService
{
    private readonly HttpClient _httpClient;

    public DeviceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetDeviceByIp(string ip, string oidWrite)
    {
        // Remova a verificação de parâmetros aqui
        var response = await _httpClient.GetAsync($"https://localhost:7055/devices/{ip}?oidWrite={oidWrite}");

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Erro na requisição: {response.StatusCode}, Detalhes: {errorContent}");
        }

        return await response.Content.ReadAsStringAsync();
    }
}
