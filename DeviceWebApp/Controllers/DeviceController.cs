using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class DeviceController : Controller
{
    private readonly DeviceService _deviceService;

    public DeviceController(DeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    // Ação para exibir a página de busca
    [HttpGet]
    public IActionResult ShowGetDeviceForm()
    {
        return View("GetDevice"); // Exibe a View GetDevice.cshtml
    }

    // Ação para buscar o dispositivo
    [HttpGet]
    public async Task<IActionResult> GetDevice(string ip, string oidWrite)
    {
        if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(oidWrite))
        {
            // Retorna a view sem dados, se não houver IP ou OID
            ViewData["ErrorMessage"] = "IP e OID são obrigatórios.";
            return View("GetDevice", null);
        }

        try
        {
            // Chama o método do service somente quando os dados são fornecidos
            var device = await _deviceService.GetDeviceByIp(ip, oidWrite);
            return View("GetDevice", device); // Exibe os resultados na View
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Erro na chamada à API: {ex.Message}");
        }
    }
}
