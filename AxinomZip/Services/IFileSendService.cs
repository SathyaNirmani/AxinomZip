using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxinomZip.Services
{
    public interface IFileSendService
    {
        bool SendFile(string data, string username, string password);
    }
}
