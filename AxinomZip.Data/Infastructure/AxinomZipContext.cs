using AxinomZip.Data.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxinomZip.Data.Infastructure
{
    public class AxinomZipContext :DbContext
    {
        public AxinomZipContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<ZipFile> ZipFiles { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
    }
}
