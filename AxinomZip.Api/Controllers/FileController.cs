using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AxinomZip.Api.Models;
using AxinomZip.Data.Domain;
using AxinomZip.Services.Interface;
using AxinomZip.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AxinomZip.Api.Controllers
{
    [Route("api/File")]
    public class FileController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IFileService _fileService;

        public FileController(IConfiguration configuration , IFileService iFileService)
        {
            _configuration = configuration;
            _fileService = iFileService;
        }       
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromBody]ZipFileModel model)
        {
            try
            {
                var key = _configuration["Key"];
                var IV = _configuration["IV"];
                if (model.EncryptText.Length == 0)
                    throw new ArgumentNullException("Encrypted Source file size is zero.");
                if (key == null || key.Length == 0)
                    throw new ArgumentNullException("Symmetric key is null.");
                if (IV == null || IV.Length == 0)
                    throw new ArgumentNullException("Initilization Vector is null.");

                byte[] encryptedByte = Convert.FromBase64String(model.EncryptText);
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
                aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
                byte[] decrypted = crypto.TransformFinalBlock(encryptedByte, 0, encryptedByte.Length);
                var filepaths = System.Text.ASCIIEncoding.ASCII.GetString(decrypted);
                await _fileService.SaveFile(filepaths);
                return Ok();
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

    }
}
