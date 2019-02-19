
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxinomZip.Services
{
    public class FileSendService : IFileSendService
    {
        public FileSendService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private readonly IConfiguration _configuration;

        public bool SendFile(string data, string username, string password)
        {
            try
            {


                var client = new RestClient("https://localhost:44390/api/");
                var request = new RestRequest("file/upload", Method.POST);
                client.Authenticator = new HttpBasicAuthenticator(username, password);
                request.AddHeader("Cache-Control", "no-cache");
                request.RequestFormat = DataFormat.Json;
                var jsonData = request.JsonSerializer.Serialize(new { EncryptText = data });
                request.AddParameter("application/json", jsonData, ParameterType.RequestBody);

                var response = client.Execute(request);
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }
}
