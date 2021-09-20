using System.Text;
using Scaffolddd.Core.Models;

namespace Scaffolddd.Core.Resource
{
    internal static class BaseRepositoryTemplate
    {
        internal static string MakeTemplate(ScaffoldddModel conf , string tab)
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Linq;");
            sb.AppendLine("using AutoMapper;");
            sb.AppendLine("using Microsoft.EntityFrameworkCore;");
            sb.AppendLine(string.Concat("using ",conf.InfraStructure.NameSpace,".DbContexts;"));
            sb.AppendLine();
            
            sb.AppendLine(string.Concat("namespace ", conf.InfraStructure.NameSpace, ".Repositories"));
            sb.AppendLine(@"{");

            sb.AppendLine(string.Concat(tab, "public abstract class BaseRepository"));
            sb.AppendLine(string.Concat(tab, "{"));

            sb.AppendLine(string.Concat(tab,tab, "protected readonly ",conf.InfraStructure.NameDbContext," _context;"));
            sb.AppendLine(string.Concat(tab,tab, "protected readonly IMapper _mapper;"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab, "public BaseRepository(",conf.InfraStructure.NameDbContext," context, IMapper mapper)"));
            sb.AppendLine(string.Concat(tab,tab, "{"));
            sb.AppendLine(string.Concat(tab,tab,tab, "_mapper = mapper;"));
            sb.AppendLine(string.Concat(tab,tab,tab, "_context = context ?? throw new ArgumentNullException(nameof(context));"));
            sb.AppendLine(string.Concat(tab,tab, "}"));
            sb.AppendLine();
            sb.AppendLine(string.Concat(tab,tab, "public int SaveChanges<TEntity>() where TEntity : class "));
            sb.AppendLine(string.Concat(tab,tab, "{"));
            //--
            //sb.AppendLine(string.Concat(tab,tab,tab, ""));
            //sb.AppendLine(string.Concat(tab,tab,tab,tab, ""));

            sb.AppendLine(string.Concat(tab,tab,tab, @"var original = _context.ChangeTracker.Entries()"));
            sb.AppendLine(string.Concat(tab,tab,tab,tab, @".Where(x => !typeof(TEntity).IsAssignableFrom(x.Entity.GetType()) && x.State != EntityState.Unchanged)"));
            sb.AppendLine(string.Concat(tab,tab,tab,tab, @".GroupBy(x => x.State)"));
            sb.AppendLine(string.Concat(tab,tab,tab,tab, @".ToList();"));
            //--
            sb.AppendLine();
            sb.AppendLine(string.Concat(tab,tab,tab, "foreach(var entry in _context.ChangeTracker.Entries().Where(x => !typeof(TEntity).IsAssignableFrom(x.Entity.GetType())))"));
            sb.AppendLine(string.Concat(tab,tab,tab, "{"));
            sb.AppendLine(string.Concat(tab,tab,tab,tab, "entry.State = EntityState.Unchanged;"));
            sb.AppendLine(string.Concat(tab,tab,tab, "}"));
            sb.AppendLine();
            sb.AppendLine(string.Concat(tab,tab,tab, "var rows = _context.SaveChanges();"));
            sb.AppendLine();
            sb.AppendLine(string.Concat(tab,tab,tab, "foreach(var state in original)"));
            sb.AppendLine(string.Concat(tab,tab,tab, "{"));
            //--
            sb.AppendLine(string.Concat(tab,tab,tab,tab, "foreach(var entry in state)"));
            sb.AppendLine(string.Concat(tab,tab,tab,tab, "{"));
            sb.AppendLine(string.Concat(tab,tab,tab,tab,tab, "entry.State = state.Key;"));
            sb.AppendLine(string.Concat(tab,tab,tab,tab, "}"));
            //--
            sb.AppendLine(string.Concat(tab,tab,tab, "}"));
            sb.AppendLine();
            sb.AppendLine(string.Concat(tab,tab,tab, "return rows;"));

            sb.AppendLine(string.Concat(tab,tab, "}"));

            sb.AppendLine();
            
            sb.AppendLine(string.Concat(tab, "}"));

            sb.AppendLine(@"}");

            return sb.ToString();

        }
    }
}