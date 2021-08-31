using System.Text;
using Scaffolddd.Core.Models;

namespace Scaffolddd.Core.Resource
{
    public static class Templates
    {
        internal static string GetTextForIUnitOfWork(ScaffoldddModel conf , string tab)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Concat("namespace ", conf.Domain.NameSpace, "Interfaces.Infrastructure"));
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

        internal static string GetTextForUnitOfWork(ScaffoldddModel conf , string tab)
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
            sb.AppendLine(string.Concat(tab,tab,"}"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab, "}"));

            sb.AppendLine(@"}");
            return sb.ToString();            
        }

        internal static string GetTextForIBaseRepository(ScaffoldddModel conf , string tab)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System.Threading.Tasks;");
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

        internal static string GetTextForBaseRepository(ScaffoldddModel conf , string tab)
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

            sb.Append(@"
        public int SaveChanges<TEntity>() where TEntity : class
        {
            var original = _context.ChangeTracker.Entries()
                        .Where(x => !typeof(TEntity).IsAssignableFrom(x.Entity.GetType()) && x.State != EntityState.Unchanged)
                        .GroupBy(x => x.State)
                        .ToList();

            foreach(var entry in _context.ChangeTracker.Entries().Where(x => !typeof(TEntity).IsAssignableFrom(x.Entity.GetType())))
            {
                entry.State = EntityState.Unchanged;
            }

            var rows = _context.SaveChanges();

            foreach(var state in original)
            {
                foreach(var entry in state)
                {
                    entry.State = state.Key;
                }
            }

            return rows;
        }

            ");

            sb.AppendLine();

            
            sb.AppendLine(string.Concat(tab, "}"));

            sb.AppendLine(@"}");

            return sb.ToString();

       }

        internal static string GetTextForInterfaces(ScaffoldddModel conf , string tab)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.Threading.Tasks;");
            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace+".Entities;"));
            sb.AppendLine();

            sb.AppendLine(string.Concat("namespace ",conf.Domain.NameSpace,".Interfaces.Repositories"));
            sb.AppendLine(@"{");

            sb.AppendLine(string.Concat(tab,"public interface I","[[CLASS]]",": IBaseRepository<[[CLASS]]Entity>"));
            sb.AppendLine(string.Concat(tab,"{"));
            sb.AppendLine();
            sb.AppendLine(string.Concat(tab,"}"));

            sb.AppendLine(@"}");

            return sb.ToString();

        }

        internal static string GetTextForRepository(ScaffoldddModel conf, string tab, string entity)
        {
            StringBuilder sb = new StringBuilder();
             
            sb.AppendLine(@"using System.Threading.Tasks;");
            sb.AppendLine(@"using AutoMapper;");
            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace,".Entities"));
            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace,".Interfaces.Repositories"));
            sb.AppendLine(string.Concat("using ",conf.InfraStructure.NameSpace,".DbContexts"));
            sb.AppendLine();

            sb.AppendLine(string.Concat("namespace ",conf.InfraStructure.NameSpace,".Repositories"));
            sb.AppendLine(@"{");

            sb.AppendLine(string.Concat(tab, "public class ",entity,"Repository : BaseRepository, I",entity,"Repository"));
            sb.AppendLine(string.Concat(tab, "{"));
            
            sb.AppendLine(string.Concat(tab,tab, ""));

            sb.AppendLine(string.Concat(tab,tab, "public ",entity,"Repository(",conf.InfraStructure.NameDbContext," context, IMapper mapper) : base(context, mapper)"));
            sb.AppendLine(string.Concat(tab,tab, "{"));
            sb.AppendLine(string.Concat(tab,tab, "}"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab, "public Task<bool> Delete(",entity,"Entity entity)"));
            sb.AppendLine(string.Concat(tab,tab, "{"));
            sb.AppendLine(string.Concat(tab,tab,tab, "throw new System.NotImplementedException();"));
            sb.AppendLine(string.Concat(tab,tab, "}"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab, "public Task<",entity,"Entity> GetByKey(",entity,"Entity entity)"));
            sb.AppendLine(string.Concat(tab,tab, "{"));
            sb.AppendLine(string.Concat(tab,tab,tab, "var retorno = await _context.",entity,".FindAsync(entity);"));
            sb.AppendLine(string.Concat(tab,tab,tab, "return _mapper.Map<",entity,"Entity>(retorno);"));
            sb.AppendLine(string.Concat(tab,tab, "}"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab, "public Task<",entity,"Entity> Insert(",entity,"Entity entity)"));
            sb.AppendLine(string.Concat(tab,tab, "{"));
            sb.AppendLine(string.Concat(tab,tab,tab, "var retorno= await _context.",entity,".AddAsync(_mapper.Map<Models.",entity,">(entity));"));
            sb.AppendLine(string.Concat(tab,tab,tab, "return _mapper.Map<",entity,"Entity>(retorno);"));
            sb.AppendLine(string.Concat(tab,tab, "}"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab, "public Task<",entity,"Entity> Update(",entity,"Entity entity)"));
            sb.AppendLine(string.Concat(tab,tab, "{"));
            sb.AppendLine(string.Concat(tab,tab,tab, "var entityFound = await _context.",entity,".FindAsync(entity.Uid);"));

            sb.AppendLine(string.Concat(tab,tab,tab, "_mapper.Map(entity,entityFound);"));
            sb.AppendLine(string.Concat(tab,tab,tab, "var retorno = _context.",entity,".Update(entityFound);"));

            sb.AppendLine(string.Concat(tab,tab,tab, "return _mapper.Map<",entity,"Entity>(retorno);"));
            sb.AppendLine(string.Concat(tab,tab, "}"));
            sb.AppendLine();


            sb.AppendLine(string.Concat(tab, "}"));
            sb.AppendLine(@"}");

            return sb.ToString();

        }

    }

}