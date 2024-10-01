using Devices_search_infos_SNMP.Entities;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Devices_search_infos_SNMP.EndPoints
{
    public class DeviceGet : ControllerBase
    {
        private const string Community = "public";
        Device _device = new Device();

        [HttpGet("/devices/")]
        public IActionResult GetValue(string ip, [FromQuery] string oidWrite)
        {
            _device.IP = ip;
            _device.OID = oidWrite;

            if (string.IsNullOrEmpty(_device.IP) || string.IsNullOrEmpty(_device.OID))
            {
                return BadRequest("IP e OID são obrigatórios.");
            }

            try
            {
                var oid = new List<Variable> { new Variable(new ObjectIdentifier(_device.OID)) };
                var result = Messenger.Get(VersionCode.V2,
                                           new IPEndPoint(IPAddress.Parse(_device.IP), 161),
                                           new OctetString(Community),
                                           oid,
                                           6000);

                if (result != null && result.Count > 0)
                {
                    var value = result[0].Data.ToString();
                    _device.Result = value;
                    return Ok(value);
                }
                else
                {
                    return NotFound("OID não encontrada ou sem resposta.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao consultar SNMP: {ex.Message}");
            }
        }


    }
}
