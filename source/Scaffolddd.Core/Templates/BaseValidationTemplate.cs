using System.Text;
using Scaffolddd.Core.Models;

namespace Scaffolddd.Core.Templates
{
    internal static class BaseValidationTemplate
    {
        internal static string MakeTemplate(ScaffoldddModel conf , string tab)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine(string.Concat("using ", conf.Domain.Paths.Interface.Validations, ";"));
            sb.AppendLine();
            sb.AppendLine(string.Concat("namespace ", conf.Domain.Paths.Implementation.Validations));
            sb.AppendLine(@"{");
            sb.AppendLine(string.Concat(tab, "public abstract class BaseValidation<Entity>: IBaseValidation<Entity>"));
            sb.AppendLine(string.Concat(tab, "{"));

                sb.AppendLine(string.Concat(tab,tab, "public IEnumerable<string> GetErros()"));
                sb.AppendLine(string.Concat(tab,tab, "{"));
                sb.AppendLine(string.Concat(tab,tab,tab, "return lstErros;"));
                sb.AppendLine(string.Concat(tab,tab, "}"));
                sb.AppendLine();
                sb.AppendLine(string.Concat(tab,tab, "public bool HaveErros()"));
                sb.AppendLine(string.Concat(tab,tab, "{"));
                sb.AppendLine(string.Concat(tab,tab,tab, "return lstErros.Count()>0;"));
                sb.AppendLine(string.Concat(tab,tab, "}"));
                sb.AppendLine();
                sb.AppendLine(string.Concat(tab,tab, "public virtual bool Validate(Entity entity)"));
                sb.AppendLine(string.Concat(tab,tab, "{"));
                sb.AppendLine(string.Concat(tab,tab,tab, "throw new System.NotImplementedException();"));
                sb.AppendLine(string.Concat(tab,tab, "}"));
                sb.AppendLine();
                sb.AppendLine(string.Concat(tab,tab, "public virtual bool ValidateDelete(Entity entity)"));
                sb.AppendLine(string.Concat(tab,tab, "{"));
                sb.AppendLine(string.Concat(tab,tab,tab, "throw new System.NotImplementedException();"));
                sb.AppendLine(string.Concat(tab,tab, "}"));
                sb.AppendLine();
                sb.AppendLine(string.Concat(tab,tab, "public virtual bool ValidateInsert(Entity entity)"));
                sb.AppendLine(string.Concat(tab,tab, "{"));
                sb.AppendLine(string.Concat(tab,tab,tab, "throw new System.NotImplementedException();"));
                sb.AppendLine(string.Concat(tab,tab, "}"));
                sb.AppendLine();
                sb.AppendLine(string.Concat(tab,tab, "public virtual bool ValidateUpdate(Entity entity)"));
                sb.AppendLine(string.Concat(tab,tab, "{"));
                sb.AppendLine(string.Concat(tab,tab,tab, "throw new System.NotImplementedException();"));
                sb.AppendLine(string.Concat(tab,tab, "}"));
                sb.AppendLine();
                sb.AppendLine(string.Concat(tab,tab, "protected void AddError(string mensagem)"));
                sb.AppendLine(string.Concat(tab,tab, "{"));
                sb.AppendLine(string.Concat(tab,tab,tab, "lstErros.Add(mensagem);"));
                sb.AppendLine(string.Concat(tab,tab, "}"));
                sb.AppendLine();

            sb.AppendLine(string.Concat(tab, "}"));

            sb.AppendLine(@"}");

            return sb.ToString();
        }
    }
}