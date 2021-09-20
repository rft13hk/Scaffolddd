using System.Text;
using Scaffolddd.Core.Models;

namespace Scaffolddd.Core.Resource
{
    internal static class InterfacesTemplate
    {
        internal static string MakeTemplate(ScaffoldddModel conf , string tab)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace+".Entities;"));
            sb.AppendLine();

            sb.AppendLine(string.Concat("namespace ",conf.Domain.NameSpace,".Interfaces.Repositories"));
            sb.AppendLine(@"{");

            sb.AppendLine(string.Concat(tab,"public interface I","[[CLASS]]Repository",": IBaseRepository<[[CLASS]]Entity>"));
            sb.AppendLine(string.Concat(tab,"{"));
            sb.AppendLine();
            sb.AppendLine(string.Concat(tab,"}"));

            sb.AppendLine(@"}");

            return sb.ToString();

        }
        
    }
}
