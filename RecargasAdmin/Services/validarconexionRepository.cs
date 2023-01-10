using System;
using System.Collections.Generic;
using System.Linq;
using NLog;
using System.Threading.Tasks;
using RidiviApi.Models;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using RestSharp;
using System.Net;
using System.Web.Script.Serialization;

namespace RidiviApi.Services
{
    public class validarconexionRepository
    {
        private static Logger logger;

        #region "Logging"
        public static void Log(char type, string proc, string msg, [CallerMemberName] string memberName = "")
        {
            if (logger == null)
                logger = LogManager.GetCurrentClassLogger();
            logger.Debug("Thread: " + Thread.CurrentThread.Name + ". Caller: " + memberName + ". Message: " + proc + "-" + msg + "\n");
        }
        #endregion


        public static Confirmaciones PostValidarConexion(string mensaje)
        {
            Log('I', "PostValidarConexion", mensaje);

            Confirmaciones resp = new Confirmaciones();
            resp.Resultado = "XX";
            resp.Mensaje = "Error en el proceso";
            try
            {
                resp.Resultado = "00";
                resp.Mensaje = mensaje;
            }
            catch (Exception ex)
            {
                resp.Resultado = "XX";
                resp.Mensaje = ex.Message;

                Log('E', "PostValidarConexion", ex.Message);
            }
            Log('I', "PostValidarConexion => ", resp.Mensaje);
            return resp;
        }
    }
}
