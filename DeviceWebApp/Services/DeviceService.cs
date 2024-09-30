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
        // Optional: Validate input parameters
        if (string.IsNullOrWhiteSpace(ip) || string.IsNullOrWhiteSpace(oidWrite))
        {
            throw new ArgumentException("IP and OID must not be empty.");
        }

        try
        {
            // Build the URL with IP and OID
            var response = await _httpClient.GetAsync($"devices?ip={ip}&oidWrite={oidWrite}");

            // Check if the response was successful
            response.EnsureSuccessStatusCode();

            // Return the response content as string
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex)
        {
            // Log or handle the error as needed
            throw new HttpRequestException($"Error retrieving device: {ex.Message}");
        }
    }

}
