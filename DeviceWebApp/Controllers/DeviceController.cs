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
        return View(new DeviceViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> GetDevice(DeviceViewModel model)
    {
            model.Result = await _deviceService.GetDeviceByIpAsync(model.IP, model.OID);

        return View(model); 
    }
    public async Task<IActionResult> GetStaticInfoDevice(DeviceViewModel model)
    {
        model.Result = await _deviceService.GetStaticsInfos(model.IP);

        return View(model);
    }
}
