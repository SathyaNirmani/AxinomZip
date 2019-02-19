using AxinomZip.Data.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxinomZip.Services.Interface
{
    public interface IFileService
    {
        Task SaveFile(string paths);
    }
}
