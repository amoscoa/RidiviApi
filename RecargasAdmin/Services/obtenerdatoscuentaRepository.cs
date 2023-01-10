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
    public class obtenerdatoscuentaRepository
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

        static string RidiviURL = "https://api.dev-ridivi.com/v4/";
        static string RidiviUsr = "alianzaCapitales_test";
        static string RidiviPass = "6AAa79Tm.UhXH";

        public class GetKey_Class
        {
            public string error { get; set; }
            public string key { get; set; }
        }


        public static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public static GetKey_Class GetKey()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var client = new RestClient(RidiviURL);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);

            string RidiviPassHash = Hash(RidiviPass);

            request.AddHeader("Content-Type", "application/json");
            string json = "{\"option\":\"getKey\"," +
                          "\"userName\":\"" + RidiviUsr + "\"," +
                          "\"password\":\"" + RidiviPassHash + "\"}";
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            GetKey_Class obj_Query = new GetKey_Class();
            var content = response.Content; // raw content as string
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            obj_Query = serializer.Deserialize<GetKey_Class>(content);
            return obj_Query;
        }

        public static clienteData GetAccountData(string id, string iban, string key)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            var client = new RestClient(RidiviURL);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);

            string RidiviPassHash = Hash(RidiviPass);

            request.AddHeader("Content-Type", "application/json");
            string json = "{\"option\":\"getAccountData\"," +
                          "\"key\":\"" + key + "\"," +
                          "\"idNumber\":\"" + id + "\"," +
                          "\"iban\":\"" + iban + "\"}";
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            clienteData obj_Query = new clienteData();
            var content = response.Content; // raw content as string

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            obj_Query = serializer.Deserialize<clienteData>(content);

            return obj_Query;
        }

        public static clienteData PostObtenerDatosCuenta(string idCliente, string iban)
        {
            Log('I', "PostObtenerDatosCuenta", idCliente + " " + iban);

            clienteData resp = new clienteData();
            try
            {
                if (true)
                {

                    GetKey_Class respKey = GetKey();
                    if (respKey.error != "ERROR")
                    {
                        resp = GetAccountData(idCliente, iban, respKey.key);
                    }

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                resp.error = "True";
                resp.message = ex.Message;

                Log('E', "PostObtenerDatosCuenta", ex.Message);
            }
            Log('I', "PostObtenerDatosCuenta => ", resp.message);
            return resp;
        }
    }
}
