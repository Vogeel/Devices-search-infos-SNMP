using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SnmpWebApp.Models;

namespace SnmpWebApp.Controllers
{
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
        [HttpGet]
        public IActionResult WalkDevice()
        {
            return View(new DeviceViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> GetDevice(DeviceViewModel model)
        {
            model.Result = await _deviceService.GetDeviceByIpAsync(model.IP, model.OID);

            return View(model);
        }
        public async Task<IActionResult> WalkDevice(DeviceViewModel model)
        {
            var jsonResponse = await _deviceService.GetStaticsInfos(model.IP);

            var deviceInfo = JsonConvert.DeserializeObject<DeviceViewModel>(jsonResponse);

            return View(new DeviceViewModel
            {
                DeviceName = deviceInfo.DeviceName,
                Username = deviceInfo.Username,
                UpTime = deviceInfo.UpTime,
                SysDescription = deviceInfo.SysDescription
            });
        }
    }
}
