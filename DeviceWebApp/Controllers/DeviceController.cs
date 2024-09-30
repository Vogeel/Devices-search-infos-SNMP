using DeviceWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class DeviceController : Controller
{
    private readonly DeviceService _deviceService;

    public DeviceController(DeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpGet]
    public IActionResult GetDevice()
    {
        return View(new DeviceViewModel()); // Retorna a view com um modelo vazio
    }

    [HttpPost]
    public async Task<IActionResult> GetDevice(DeviceViewModel model)
    {

            // Chama o método para obter o dispositivo
            model.Result = await _deviceService.GetDeviceByIpAsync(model.IP, model.OID);
        

        return View(model); // Retorna a mesma view com o modelo preenchido
    }
}
