using System.Collections.Generic;
using System.Text;
using Scaffolddd.Core.Models;

namespace Scaffolddd.Core.Resource
{
    internal static class DependencyInjectionMappingTemplate
    {
        internal static string MakeTemplate(ScaffoldddModel conf, string tab, Dictionary<string,string> _dicSwapService, Dictionary<string,string> _dicSwapRepository)
        {
            StringBuilder sb = new StringBuilder();
             
            sb.AppendLine(@"using Microsoft.Extensions.DependencyInjection;");

            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace,".Services;"));
            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace,".Interfaces","Services",";"));
            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace,".Interfaces","Repositories",";"));
            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace,".Interfaces","Infrastructure",";"));

            sb.AppendLine(string.Concat("using ",conf.InfraStructure.NameSpace, ".DbContexts",";"));
            sb.AppendLine(string.Concat("using ",conf.InfraStructure.NameSpace, ".Repositories",";"));
            
            //--------------
            sb.AppendLine();
            //--------------
            sb.AppendLine(string.Concat("namespace ", conf.Application.NameSpace,".Implementation"));
            sb.AppendLine("{");

            sb.AppendLine(string.Concat(tab,"public static class DependencyInjectionMapping"));
            sb.AppendLine(string.Concat(tab,@"{"));

            sb.AppendLine(string.Concat(tab,tab, @"public static void ConfigureServices(IServiceCollection services)"));
            sb.AppendLine(string.Concat(tab,tab, @"{"));
            
            sb.AppendLine(string.Concat(tab,tab,tab, @"#region Services")); //----------------------------------------------------------------------

            sb.AppendLine();
            foreach (var item in _dicSwapService)
            {
                sb.AppendLine(string.Concat(tab,tab,tab, string.Concat("services.AddTransient<", item.Key,", ",item.Value,">();" )));
            }
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab,tab, @"#endregion")); //----------------------------------------------------------------------------

            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab,tab, @"#region Repositories"));
            sb.AppendLine();
            foreach (var item in _dicSwapRepository)
            {
                sb.AppendLine(string.Concat(tab,tab,tab, string.Concat("services.AddTransient<", item.Key,", ",item.Value,">();" )));
            }
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab,tab, @"#endregion"));
            sb.AppendLine();


            sb.AppendLine(string.Concat(tab,tab, @"}"));

            sb.AppendLine(string.Concat(tab,@"}"));
            
            sb.AppendLine("}");

            return sb.ToString();
        }

    }

}
