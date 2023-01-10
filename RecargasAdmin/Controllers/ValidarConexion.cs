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
    public class ValidarConexionController : ApiController
    {

        private validarconexionRepository ValidarConexionRepository;

        public ValidarConexionController()
        {
            this.ValidarConexionRepository = new validarconexionRepository();
        }

        public HttpResponseMessage Post(string mensaje)
        {
            Confirmaciones resp = validarconexionRepository.PostValidarConexion(mensaje);

            var respHttp = new HttpResponseMessage
            {
                Content =
                    new StringContent(
                    "{\"resultado\":\"" + resp.Resultado +
                    "\",\"mensaje\":\"" + resp.Mensaje +
                    "\"}",
                    System.Text.Encoding.UTF8, "application/json")
            };
            return respHttp;
        }
    }
}
