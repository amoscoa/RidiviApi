using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace RidiviApi
{
    public static class Seguridad
    {

        // HMAC
        public static string secretKey = "9d2ce5fd-3c24-4a2f-9a2f-1d2adb065b0a";  // pruebas
        // producción :  "8e1df6ge-4d35-5b3g-0b3g-2e3bec176c1b"

        public const string originsURL = "*";

        public const bool PasaHMAC = true;
        public static string HMAC(string signatureString, string secretKey)
        {
            var enc = Encoding.ASCII;
            HMACSHA1 hmac = new HMACSHA1(enc.GetBytes(secretKey));
            hmac.Initialize();

            byte[] buffer = enc.GetBytes(signatureString);
            return BitConverter.ToString(hmac.ComputeHash(buffer)).Replace("-", "").ToLower();
        }

        public static string ComputeHash(string Key)
        {
            SHA1CryptoServiceProvider objSHA1 = new SHA1CryptoServiceProvider();
            objSHA1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Key.ToCharArray()));
            byte[] buffer = objSHA1.Hash;
            string HashValue = System.Convert.ToBase64String(buffer);
            return HashValue;
        }

        public static string Numeros( string id )
        {
            string newId = "";
            for (int i = 0; i < id.Length; i++)
                if ((id[i] >= '0') && (id[i] <= '9'))
                    newId += id[i];
            return newId;
        }

        public static string GetClientAddress()
        {
            try
            {
                return System.Web.HttpContext.Current.Request.UserHostAddress;
            }
            catch (Exception ex)
            {
                return ex.Message + ' ' + ex.StackTrace;
            }
        }

        public static string GetClientIp()
        {
            try
            {
                string headerValues = System.Web.HttpContext.Current.Request.Headers.Get("X-Forwarded-For");
                return headerValues.Split(':')[0];
            }
            catch (Exception ex)
            {
                return ex.Message + ' ' + ex.StackTrace;
            }
        }

        //public static string GetClientAddress()
        //{
        //    try
        //    {
        //        // creating object of service when request comes   
        //        OperationContext context = OperationContext.Current;
        //        //Getting Incoming Message details   
        //        MessageProperties prop = context.IncomingMessageProperties;
        //        //Getting client endpoint details from message header   
        //        RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
        //        return endpoint.Address;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;

        //    }
        //}

    }
}