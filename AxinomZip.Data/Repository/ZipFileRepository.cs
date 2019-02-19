using AxinomZip.Data.Domain;
using AxinomZip.Data.Infastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxinomZip.Data.Repository
{
    public class ZipFileRepository 
    {
        private readonly AxinomZipContext _context;
        public ZipFileRepository(AxinomZipContext context)
        {
            _context = context;
        }
        public async Task Add(ZipFile file)
        {
            _context.Add(file);
            await Save();
        }

        private async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
