using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RidiviApi.Models;
using System.Web.Http.Cors;
using System.Web.Http;
using System.Net.Http;
using RidiviApi.Services;
using System.Text;

namespace RidiviApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RecibirNotificacionesController : ApiController
    {

        private recibirnotificacionesRepository RecibirNotificacionesRepository;

        public RecibirNotificacionesController()
        {
            this.RecibirNotificacionesRepository = new recibirnotificacionesRepository();
        }



        public HttpResponseMessage Post(notificaRidivi notificaData)
        {

            string hdr64 = System.Web.HttpContext.Current.Request.Headers.Get("Authorization").ToString();
            string[] auth64 = hdr64.Split(' ');
            byte[] dataAuth = Convert.FromBase64String(auth64[1]);
            string hdr = Encoding.UTF8.GetString(dataAuth);

            confirmaRidivi resp = recibirnotificacionesRepository.PostRecibirNotificaciones(notificaData, hdr);

            var respHttp = new HttpResponseMessage
            {
                Content =
                    new StringContent(
                    "{\"status\":\"" + resp.status +
                    "\",\"code\":" + resp.code.ToString() +
                    ",\"error\":\"" + resp.error +
                    "\",\"message\":\"" + resp.message +
                    "\",\"result\":\"" + resp.result +
                    "\"}",
                    System.Text.Encoding.UTF8, "application/json")
            };
            switch (resp.code)
            {
                case 200: 
                    respHttp.StatusCode = System.Net.HttpStatusCode.OK;
                    break;
                case 409:
                    respHttp.StatusCode = System.Net.HttpStatusCode.Conflict;
                    break;
                case 503:
                    respHttp.StatusCode = System.Net.HttpStatusCode.ServiceUnavailable;
                    break;
                default:
                    respHttp.StatusCode = System.Net.HttpStatusCode.ServiceUnavailable;
                    break;
            }
            return respHttp;
        }
    }
}
