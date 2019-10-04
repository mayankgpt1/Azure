using System;
using System.IO;
using System.Net;
using System.Text;
namespace Azure_Accesss_Token
{
    class Program
    {
        private static string token = string.Empty;
        static string strClient_id, strGrant_type, strClient_secret, strResource, strTenantId;
        static void Main(string[] args)
        {
            strClient_id = "6626da35-8042-43b2-b1cd-f68d3f5b2b75";// Application (client) ID
            strGrant_type = "client_credentials";// Permission Grant Type
            strTenantId = "fba9f653-affd-4e73-8d2f-c5e970f21e0e";//Directory (tenant) ID
            strClient_secret = "8f?9MyN7q@:dyB5@q.4Z=Wk3NJdr@zm5";//A secret string that the application uses to prove its identity when requesting a token.
            strResource = "https://management.azure.com/";// Resource URL
            //Get an authentication access token
            string token = GetToken(strClient_id, strGrant_type, strTenantId, strResource);
            Console.WriteLine(token);
            Console.ReadKey();
        }
        #region Get an authentication access token
        private static string GetToken(string strClient_id, string strGrant_type, string strTenantId, string strResource)
        {
            HttpWebResponse response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://login.microsoftonline.com/"+ strTenantId + "/oauth2/token");
                request.KeepAlive = true;
                request.Headers.Add("Content-Type", @"application/x-www-form-urlencoded");
                request.Method = "POST";
                string body = @"client_id=" + strClient_id;
                body += "&grant_type=" + strGrant_type;
                body += "&client_secret=" + strClient_secret;
                body += "&resource=" + strResource;
                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(body);
                request.ContentLength = postBytes.Length;
                System.IO.Stream stream = request.GetRequestStream();
                stream.Write(postBytes, 0, postBytes.Length);
                stream.Close();
                response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader Content = new StreamReader(receiveStream, Encoding.UTF8);
                string AccessToken = Content.ReadToEnd();
                return AccessToken;
            }           
            catch (Exception ex)
            {
                return "Following error Came: " + ex.Message.ToString();
            }
        }
        #endregion
    }
}