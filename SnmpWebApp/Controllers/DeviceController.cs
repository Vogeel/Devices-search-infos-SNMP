using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SnmpWebApp.Models;
using SnmpWebApp.Service;

namespace SnmpWebApp.Controllers
{
    public class DeviceController : Controller
    {
        private readonly DeviceService _deviceService;
        private readonly DataBaseService _dataBaseService = new();

        public DeviceController(DeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public IActionResult ListItens()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetDevice(string oid = "")
        {
            var model = new DeviceViewModel
            {
                OID = oid
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult WalkDevice()
        {
            return View(new DeviceViewModel());
        }
        [HttpGet]
        public IActionResult SnmpInfo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetDevice(DeviceViewModel model, string action)
        {
            try
            {

                if (action == "consult")
                {
                    model.Result = await _deviceService.GetDeviceByIpAsync(model.IP, model.OID);
                }
                else if (action == "saveOnDataBase")
                {
                    var result = await _deviceService.GetDeviceByIpAsync(model.IP, model.OID);
                    model.Result = _dataBaseService.SaveResulInLiteDb(model, result);
                }
            }
            catch
            {
                model.Result = $"Ao tentar fazer um GET na oid {model.OID}, retornou Erro.";
            }

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
