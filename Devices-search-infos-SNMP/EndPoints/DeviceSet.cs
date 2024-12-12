using Devices_search_infos_SNMP.Entities;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Devices_search_infos_SNMP.EndPoints
{
    public class DeviceSet : ControllerBase
    {
        Device _device = new Device();
        private const string Community = "public";

        [HttpGet("/devices/Set")]
        public IActionResult SetValue(string ip, [FromQuery] string oidWrite, [FromQuery] string newValueWrite)
        {
            try
            {
                var oid = new ObjectIdentifier(oidWrite);
                var newValue = new OctetString(newValueWrite);
                var variables = new List<Variable>
                {
                    new Variable(oid, newValue)
                };

                var result = Messenger.Set(VersionCode.V1,
                                           new IPEndPoint(IPAddress.Parse(ip), 161),
                                           new OctetString(Community),
                                           variables,
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
                return StatusCode(500, $"Erro ao setar SNMP: {ex.Message}");
            }
        }
    }
}

