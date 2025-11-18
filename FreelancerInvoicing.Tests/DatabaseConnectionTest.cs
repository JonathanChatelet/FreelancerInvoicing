using Microsoft.EntityFrameworkCore;
using FreelancerInvoicing.Tools.Settings;
using FreelancerInvoicing.Repositories.Context;

namespace FreelancerInvoicing.Tests
{
    public class DatabaseConnectionTest
    {
        [Fact]
        public void DBConnectionTest()
        {            
            DataBaseConnectionSettings dataBaseConnectionSettings = new DataBaseConnectionSettings();
            dataBaseConnectionSettings = SettingsTools.GetDataBaseConnectionSettings();
            DbContextOptionsBuilder<FreelancerInvoicingDbContext> optionsBuilder = new DbContextOptionsBuilder<FreelancerInvoicingDbContext>();
            optionsBuilder.UseSqlServer(dataBaseConnectionSettings.FreelanceInvoicingDataBase);

            using var context = new FreelancerInvoicingDbContext(optionsBuilder.Options);
            Assert.True(context.Database.CanConnect());
        }
    }
}