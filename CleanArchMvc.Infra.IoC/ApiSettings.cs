using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.IoC
{
    public static class ApiSettings
    {
        public static string ApiBaseUrl { get; set; } = "https://localhost:44363/";
    }
}
