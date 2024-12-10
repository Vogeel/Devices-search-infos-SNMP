using LiteDB;
using SnmpWebApp.Models;

namespace SnmpWebApp.Service
{
    public class DataBaseService
    {
    private readonly string _dataBasePath = @"DataBase\SNMPInfo.nsi";

        public string SaveResulInLiteDb(DeviceViewModel model, string result)
        {
            if (model == null)
                return ("O modelo não pode ser nulo.");

            model.Result = result;

            using (var db = new LiteDatabase(_dataBasePath))
            {
                var collection = db.GetCollection<DeviceViewModel>("results");

                collection.EnsureIndex(x => x.Id, true);

                collection.Insert(model);

            }

            return $"Sucesso ao salvar informações no Banco de dados, result: {model.Result} ";
        }

        public List<DeviceViewModel> ListItensInDataBase()
        {
            using (var db = new LiteDatabase(_dataBasePath))
            {
                var collection = db.GetCollection<DeviceViewModel>("results");
                return collection.FindAll().ToList();
            }
        }

    }
}
