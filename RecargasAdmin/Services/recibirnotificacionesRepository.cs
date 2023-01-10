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

namespace RidiviApi.Services
{
    public class recibirnotificacionesRepository
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

        public static confirmaRidivi PostRecibirNotificaciones(notificaRidivi notificaData, string hdr)
        {
            Log('I', "PostRecibirNotificaciones", notificaData.numMov.ToString());

            bool errorResponse = false;
            int codeResponse = 0;
            string statusResponse = "";
            string messageResponse = "";
            string resultResponse = "";
            try
            {
                if (true)
                {
                    // Determina la compañía que va a recibir el dinero
                    switch (notificaData.destination.phone)
                    {
                        // JPS
                        case "XXXXXXXX":

                            break;
                        default:
                            break;
                    }

                    statusResponse = "HTTP 200";
                    codeResponse = 200;
                    errorResponse = false;
                    messageResponse = "Proceso exitoso";
                    resultResponse = "Proceso exitoso";

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                statusResponse = "HTTP 409";
                codeResponse = 409;
                errorResponse = true;
                messageResponse = ex.Message;
                resultResponse = ex.Message;

                Log('E', "PostRecibirNotificaciones", ex.Message);
            }
            Log('I', "PostRecibirNotificaciones => ", messageResponse);
            return new confirmaRidivi
            {
                status = statusResponse,
                code = codeResponse,
                error = errorResponse,
                message = messageResponse,
                result = resultResponse
            };
        }
    }
}
