using Microsoft.AspNetCore.Mvc;
using SnmpWebApp.Service;

namespace SnmpWebApp.Controllers
{
    public class DataBaseController : Controller
    {
        private readonly DataBaseService _dataBaseService;

        public DataBaseController()
        {
            _dataBaseService = new DataBaseService();
        }
        [HttpGet]
        public IActionResult listItens()
        {
            var itens = _dataBaseService.ListItensInDataBase();
           return View(itens);
        }

    }
}
