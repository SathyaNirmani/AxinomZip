using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AxinomZip.Data.Domain
{
    public class ZipFile
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string FolderStructure { get; set; }
    }
}
