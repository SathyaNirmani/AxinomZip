using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AxinomZip.Api.BasicAuthProvider
{
    public class AuthConfig
    {
        public string Username { get;}
        public string Password { get;}

        public AuthConfig(IConfiguration configuration)
        {
            Username = configuration.GetSection("BasicAuth:Username").Value;
            Password = configuration.GetSection("BasicAuth:Password").Value;
        }
      
    }
}
