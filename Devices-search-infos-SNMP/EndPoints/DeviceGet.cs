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

        [HttpGet("/devices/info")]
        public IActionResult GetDeviceInfo(string ip)
        {
            _device.IP = ip;

            if (string.IsNullOrEmpty(_device.IP))
            {
                return BadRequest("IP é obrigatório.");
            }

            try
            {
                var oids = new List<Variable>
        {
            new Variable(new ObjectIdentifier(".1.3.6.1.2.1.1.5.0")), // Nome do dispositivo
            new Variable(new ObjectIdentifier(".1.3.6.1.2.1.1.4.0")), // Usuário logado
            new Variable(new ObjectIdentifier(".1.3.6.1.2.1.25.4.2.1.2.18996")), // Aplicativo aberto
            new Variable(new ObjectIdentifier(".1.3.6.1.2.1.25.6.3.1.2.10")), // Aplicativo aleatorio
            new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.3.0")), // upTime
            new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.1.0"))  // Descrição sys
        };

                var result = Messenger.Get(VersionCode.V2,
                                           new IPEndPoint(IPAddress.Parse(_device.IP), 161),
                                           new OctetString(Community),
                                           oids,
                                           6000);

                if (result != null && result.Count > 0)
                {
                    var response = new Dictionary<string, string>
            {
                { "DeviceName", result[0].Data.ToString() },
                { "Username", result[1].Data.ToString() },
                { "RandomOpenedApp", result[2].Data.ToString() },
                { "RandomInstalledApp", result[3].Data.ToString() },
                { "upTime", result[4].Data.ToString() },
                { "SysDesription", result[5].Data.ToString() }
            };

                    return Ok(response);
                }
                else
                {
                    return NotFound("Não foi possível obter informações do dispositivo.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao consultar SNMP: {ex.Message}");
            }
        }



    }
}
