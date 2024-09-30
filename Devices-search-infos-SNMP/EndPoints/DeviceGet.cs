using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Devices_search_infos_SNMP.EndPoints
{
    public class DeviceGet : ControllerBase
    {
        private const string Community = "public";

        [HttpGet("/devices/{ip}")]
        public IActionResult GetValue(string ip, [FromQuery] string oidWrite)
        {
            if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(oidWrite))
            {
                return BadRequest("IP e OID são obrigatórios.");
            }

            try
            {
                var oid = new List<Variable> { new Variable(new ObjectIdentifier(oidWrite)) };
                var result = Messenger.Get(VersionCode.V2,
                                           new IPEndPoint(IPAddress.Parse(ip), 161),
                                           new OctetString(Community),
                                           oid,
                                           6000);

                if (result != null && result.Count > 0)
                {
                    var value = result[0].Data.ToString();
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
