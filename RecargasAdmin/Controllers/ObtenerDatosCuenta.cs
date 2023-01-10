using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RidiviApi.Models;
using System.Web.Http.Cors;
using System.Web.Http;
using System.Net.Http;
using RidiviApi.Services;

namespace RidiviApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ObtenerDatosCuentaController : ApiController
    {

        private obtenerdatoscuentaRepository ObtenerDatosCuentaRepository;

        public ObtenerDatosCuentaController()
        {
            this.ObtenerDatosCuentaRepository = new obtenerdatoscuentaRepository();
        }

        public HttpResponseMessage Post(string idCliente, string iban)
        {
            clienteData resp = obtenerdatoscuentaRepository.PostObtenerDatosCuenta(idCliente, iban);

            var respHttp = new HttpResponseMessage
            {
                Content =
                    new StringContent(
                    "{\"idnumber\":\"" + resp.account.idNumber +
                    "\",\"moneda\":\"" + resp.account.ToString() +
                    "\",\"nombre\":\"" + resp.account.NomPropietario +
                    "\",\"error\":\"" + resp.error +
                    "\",\"mensaje\":\"" + resp.message +
                    "\"}",
                    System.Text.Encoding.UTF8, "application/json")
            };
            return respHttp;
        }
    }
}
