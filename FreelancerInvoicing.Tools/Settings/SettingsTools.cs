using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FreelancerInvoicing.Tools.Settings
{
    public static class SettingsTools
    {
        private static readonly IConfigurationRoot _configuration;

        static SettingsTools()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\FreelancerInvoicing.API"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static DataBaseConnectionSettings GetDataBaseConnectionSettings()
        {
            return _configuration.GetSection("DataBaseConnectionSettings").Get<DataBaseConnectionSettings>();
        }

        public static AppSettings GetAppSettings()
        {
            return _configuration.GetSection("AppSettings").Get<AppSettings>();
        }

    }
}
