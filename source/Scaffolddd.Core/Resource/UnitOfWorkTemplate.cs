using System.Text;
using Scaffolddd.Core.Models;

namespace Scaffolddd.Core.Resource
{
    internal static class UnitOfWorkTemplate
    {
        internal static string MakeTemplate(ScaffoldddModel conf , string tab)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"using System;");
            sb.AppendLine(@"using System.Linq;");
            sb.AppendLine(@"using Microsoft.EntityFrameworkCore;");
            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace,".Interfaces.Infrastructure;"));
            sb.AppendLine();
            sb.AppendLine(string.Concat("namespace ", conf.InfraStructure.NameSpace, ".DbContexts"));
            sb.AppendLine(@"{");

            sb.AppendLine(string.Concat(tab, "public class UnitOfWork : IUnitOfWork, IDisposable"));
            sb.AppendLine(string.Concat(tab, "{"));

            sb.AppendLine(string.Concat(tab,tab,"private readonly ",conf.InfraStructure.NameDbContext," _context;"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab,"public UnitOfWork(",conf.InfraStructure.NameDbContext," context)"));
            sb.AppendLine(string.Concat(tab,tab,"{"));
            sb.AppendLine(string.Concat(tab,tab,tab,"_context = context;"));
            sb.AppendLine(string.Concat(tab,tab,"}"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab,"public void Commit()"));
            sb.AppendLine(string.Concat(tab,tab,"{"));
            sb.AppendLine(string.Concat(tab,tab,tab,"_context.SaveChanges();"));
            sb.AppendLine(string.Concat(tab,tab,"}"));
            sb.AppendLine();


            sb.AppendLine(string.Concat(tab,tab,"public void RollBack()"));
            sb.AppendLine(string.Concat(tab,tab,"{"));
            sb.AppendLine(string.Concat(tab,tab,tab,"ResetContextState();"));
            sb.AppendLine(string.Concat(tab,tab,"}"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab,"private void ResetContextState() => _context.ChangeTracker.Entries()"));
            sb.AppendLine(string.Concat(tab,tab,tab,".Where(e => e.Entity != null).ToList()"));
            sb.AppendLine(string.Concat(tab,tab,tab,".ForEach(e => e.State = EntityState.Detached);"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab,"public void Dispose()"));
            sb.AppendLine(string.Concat(tab,tab,"{"));
            sb.AppendLine();
            sb.AppendLine(string.Concat(tab,tab,"}"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab, "}"));

            sb.AppendLine(@"}");
            return sb.ToString();            
        }        
    }
}