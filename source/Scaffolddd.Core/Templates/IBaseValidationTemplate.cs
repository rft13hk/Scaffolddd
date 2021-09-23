using System.Text;
using Scaffolddd.Core.Models;

namespace Scaffolddd.Core.Templates
{
    internal static class IBaseValidationTemplate
    {
        internal static string MakeTemplate(ScaffoldddModel conf , string tab)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine();
            
            sb.AppendLine(string.Concat("namespace ", conf.Domain.NameSpace,".Interfaces.Validations"));
            sb.AppendLine(@"{");

            sb.AppendLine(string.Concat(tab, "public interface IBaseValidation<TEntity>"));
            sb.AppendLine(string.Concat(tab, "{"));
            sb.AppendLine(string.Concat(tab,tab, "IEnumerable<string> GetErros();"));
            sb.AppendLine(string.Concat(tab,tab, "bool HaveErros();"));
            sb.AppendLine(string.Concat(tab,tab, "bool Validate(TEntity entity);"));
            sb.AppendLine(string.Concat(tab,tab, "bool ValidateInsert(TEntity entity);"));
            sb.AppendLine(string.Concat(tab,tab, "bool ValidateUpdate(TEntity entity);"));
            sb.AppendLine(string.Concat(tab,tab, "bool ValidateDelete(TEntity entity);"));
            sb.AppendLine(string.Concat(tab, "}"));

            sb.AppendLine(@"}");

            return sb.ToString();
        }
    }
}