using System.Text;
using Scaffolddd.Core.Models;

namespace Scaffolddd.Core.Resource
{
    internal static class IUnitOfWorkTemplate
    {
        internal static string MakeTemplate(ScaffoldddModel conf , string tab)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Concat("namespace ", conf.Domain.NameSpace, ".Interfaces.Infrastructure"));
            sb.AppendLine(@"{");

            sb.AppendLine(string.Concat(tab, "public interface IUnitOfWork"));
            sb.AppendLine(string.Concat(tab, "{"));

            sb.AppendLine(string.Concat(tab,tab, "void Commit();"));
            sb.AppendLine(string.Concat(tab,tab, "void RollBack();"));
            sb.AppendLine(string.Concat(tab,tab, "void Dispose();"));

            sb.AppendLine(string.Concat(tab, "}"));

            sb.AppendLine(@"}");

            return sb.ToString();            
        }
    }
}