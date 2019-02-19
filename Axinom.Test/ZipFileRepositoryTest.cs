using AxinomZip.Data.Domain;
using AxinomZip.Data.Infastructure;
using AxinomZip.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Axinom.Test
{
    public class ZipFileRepositoryTest
    {
        private DbContextOptionsBuilder<AxinomZipContext> optionsBuilder = new DbContextOptionsBuilder<AxinomZipContext>();

        [Fact]
        public async Task Should_Add_New_FileStructure()
        {
            optionsBuilder.UseInMemoryDatabase("MySuperTestDb");

            // Run the test against one instance of the context
            using (var context = new AxinomZipContext(optionsBuilder.Options))
            {
                var zipFileRepository = new ZipFileRepository(context);
                var zipFile = new ZipFile { Name = "python vedios", FolderStructure = "python vedios" };
                await zipFileRepository.Add(zipFile);

                Assert.True(zipFile.Id>0);
            }
        }

        [Fact]
        public async Task Should_Return_Two_As_FileStructure_List_Count()
        {
            optionsBuilder.UseInMemoryDatabase("MySuperTestDb");

            // Run the test against one instance of the context
            using (var context = new AxinomZipContext(optionsBuilder.Options))
            {
                var zipFileRepository = new ZipFileRepository(context);
                var zipFile1 = new ZipFile { Name = "xamarin.forms", FolderStructure = "xamarin.forms" };
                var zipFile2 = new ZipFile { Name = "python vedios", FolderStructure = "python vedios" };
                await zipFileRepository.Add(zipFile1);
                await zipFileRepository.Add(zipFile2);

                Assert.Equal(2, context.ZipFiles.Count());
            }
        }
    }
}
