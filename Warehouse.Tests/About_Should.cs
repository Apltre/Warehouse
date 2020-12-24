using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Warehouse.Tests
{
    [TestClass]
    public class About_Should : Test
    {
        [TestMethod]
        public async Task ReturnValidMetadata()
        {
            var client = this.Factory.Create();

            var about = await client.GetAboutAsync();

            Assert.AreEqual("Warehouse - Sorting center api", about.Description);
            Assert.AreEqual("1.0.0.0", about.Version);
            Assert.AreEqual("Development", about.Environment);
        }
    }
}