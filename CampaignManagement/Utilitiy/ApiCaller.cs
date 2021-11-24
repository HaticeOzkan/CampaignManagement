using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace CampaignManagement.Utilitiy
{
    public class ApiCaller
    {
        public static readonly string ConnectionErrorMessage = "An error occurred while service connecting";

        public T Call<T>(string methodType, string uri, object postData)
        {
            var response = string.Empty;
            var serviceUri = new Uri(uri);
            byte[] requestByteData = null;
            byte[] responseByteData = null;
            byte[] buffer = new byte[1024];

            try
            {
                var webRequest = (HttpWebRequest)System.Net.WebRequest.Create(serviceUri);
                webRequest.Method = methodType;
                webRequest.ContentType = "application/json";

                if (postData != null)
                {
                    requestByteData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(postData));
                    webRequest.ContentLength = requestByteData.Length;

                    using (var writeStream = webRequest.GetRequestStream())
                    {
                        writeStream.Write(requestByteData, 0, requestByteData.Length);
                    }
                }

                WebResponse myResp = webRequest.GetResponse();
                using (Stream stream = myResp.GetResponseStream())
                using (MemoryStream ms = new MemoryStream())
                {
                    int count = 0;

                    do
                    {
                        count = stream.Read(buffer, 0, buffer.Length);
                        ms.Write(buffer, 0, count);

                    } while (stream.CanRead && count > 0);

                    responseByteData = ms.ToArray();
                }

                string jsonStr = Encoding.UTF8.GetString(responseByteData);
                return JsonConvert.DeserializeObject<T>(jsonStr);
            }
            catch
            {
                throw new Exception(ConnectionErrorMessage);
            }
        }
    }
}
