using System.Text;
using Scaffolddd.Core.Models;

namespace Scaffolddd.Core.Resource
{
    internal static class IBaseRepositoryTemplate
    {
        internal static string MakeTemplate(ScaffoldddModel conf , string tab)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine();
            sb.AppendLine(string.Concat("namespace ", conf.Domain.NameSpace, ".Interfaces.Repositories"));
            sb.AppendLine(@"{");
            sb.AppendLine(string.Concat(tab, "public interface IBaseRepository<Entidade>"));
            sb.AppendLine(string.Concat(tab, "{"));
            sb.AppendLine(string.Concat(tab, tab, "Task<Entidade> Insert(Entidade entity);"));
            sb.AppendLine(string.Concat(tab, tab, "Task<bool> Delete(Entidade entity);"));
            sb.AppendLine(string.Concat(tab, tab, "Task<Entidade> Update(Entidade entity);"));
            sb.AppendLine(string.Concat(tab, tab, "Task<Entidade> GetByKey(Entidade entity);"));
            sb.AppendLine(string.Concat(tab, "}"));
            sb.AppendLine(@"}");

            return sb.ToString();
        }        
    }
}