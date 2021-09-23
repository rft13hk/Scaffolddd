using System.Text;
using Scaffolddd.Core.Models;

namespace Scaffolddd.Core.Templates
{
    internal static class ValidationsTemplate
    {
        internal static string MakeTemplate(ScaffoldddModel conf , string tab, string entity)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using System.Collections.Generic;");

            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace,".Entities;"));
            sb.AppendLine();
            
            sb.AppendLine(string.Concat("namespace ", conf.Domain.NameSpace,".Validations"));
            sb.AppendLine(@"{");

            sb.AppendLine(string.Concat(tab, "public class ",entity,"Validation : BaseValidation<",entity,"Entity>"));
            sb.AppendLine(string.Concat(tab, "{"));
            sb.AppendLine(string.Concat(tab, "}"));

            sb.AppendLine(@"}");

            return sb.ToString();
        }
    }
}