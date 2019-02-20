using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AxinomZip.Models;
using AxinomZip.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace AxinomZip.Controllers
{

    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        internal IFileSendService _fileSendService { get; }

        public HomeController(IConfiguration configuration, IFileSendService fileService)
        {
            _configuration = configuration;
            _fileSendService = fileService;

        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FileUpload()
        {

            return View("Index");
        }

        [HttpPost]
        public ActionResult FileUploadZip()
        {
            try
            {
                IFormFile file = Request.Form.Files[0];
                List<TreeViewNode> nodes = new List<TreeViewNode>();
                List<TreeViewNode> resultNode = new List<TreeViewNode>();

                using (var archive = new ZipArchive(file.OpenReadStream()))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        var lastelement = entry.ToString().ToCharArray().Last().ToString();
                        var filepath = entry.ToString().Split('/');
                        int nodesize = filepath.Length;
                        if (nodesize == 1)
                        {
                            nodes.Add(new TreeViewNode { id = filepath[0], parent = "#", text = filepath[0] });
                        }
                        if (lastelement != "/" && nodesize != 1)
                        {
                            for (int i = 0; i < nodesize; i++)
                            {
                                if (i == 0)
                                {
                                    nodes.Add(new TreeViewNode { id = filepath[0], parent = "#", text = filepath[0] });
                                }
                                else
                                {
                                    nodes.Add(new TreeViewNode { id = filepath[i], parent = filepath[i - 1], text = filepath[i] });
                                }

                            }
                        }
                        else
                        {
                            for (int i = 0; i < nodesize - 1; i++)
                            {
                                if (i == 0)
                                {
                                    nodes.Add(new TreeViewNode { id = filepath[0], parent = "#", text = filepath[0] });
                                }
                                else
                                {
                                    nodes.Add(new TreeViewNode { id = filepath[i], parent = filepath[i - 1], text = filepath[i] });
                                }

                            }
                        }

                    }

                    resultNode = nodes.GroupBy(x => new { x.id, x.parent }).Select(grp => grp.ToList().First()).ToList();

                }
                var jsonResult = JsonConvert.SerializeObject(resultNode);
                return Json(jsonResult);
            }
            catch (Exception ex)
            {
                throw (ex);

            }

        }

        public IActionResult SendFile(string plainText)
        {
            try
            {
                var username = HttpContext.Session.GetString("UserName");
                var password = HttpContext.Session.GetString("Password");
                var key = _configuration["Key"];
                var IV = _configuration["IV"];
                if (plainText.Length == 0)
                    throw new ArgumentNullException("Source file size is zero.");
                if (key == null || key.Length == 0)
                    throw new ArgumentNullException("Symmetric key is null.");
                if (IV == null || IV.Length == 0)
                    throw new ArgumentNullException("Initilization Vector is null.");

                byte[] plantextfile = System.Text.ASCIIEncoding.ASCII.GetBytes(plainText);
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                aes.BlockSize = 128;
                aes.KeySize = 256;
                aes.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
                aes.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(IV);
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
                byte[] encrypted = crypto.TransformFinalBlock(plantextfile, 0, plantextfile.Length);
                var encryptedString = Convert.ToBase64String(encrypted);
                _fileSendService.SendFile(encryptedString , username ,password);
                ModelState.AddModelError("", "SuccessFully Saved");
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }
        [HttpPost]
        public ActionResult Index(string selectedItems)
        {
            try
            {
                if (selectedItems == "[]" | selectedItems == null)
                {
                    return PartialView("Index");
                }
                else
                {
                    var DeserializeData = JsonConvert.DeserializeObject(selectedItems);
                    var value = SendFile(DeserializeData.ToString());
                     return View();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }


        }

    }
}
