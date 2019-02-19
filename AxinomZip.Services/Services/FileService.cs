using AxinomZip.Data.Domain;
using AxinomZip.Data.Repository;
using AxinomZip.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxinomZip.Services.Services
{
    public class FileService : IFileService
    {
        private readonly ZipFileRepository _zipFileRepository;

        public FileService(ZipFileRepository zipFileRepository)
        {
            _zipFileRepository = zipFileRepository;
        }

        public async Task SaveFile(string folderStructure)
        {
           var filename = folderStructure.Split(',').GetValue(0).ToString().Split(':').GetValue(1).ToString();
            await _zipFileRepository.Add(new ZipFile { FolderStructure = folderStructure ,Name = filename });
        }
       
    }
}
