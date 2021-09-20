using System.Collections.Generic;
using System.Text;
using Scaffolddd.Core.Models;

namespace Scaffolddd.Core.Resource
{
    public static class RepositoryTemplate
    {
        internal static string MakeTemplate(ScaffoldddModel conf, string tab, string entity)
        {
            StringBuilder sb = new StringBuilder();
             
            sb.AppendLine(@"using System.Threading.Tasks;");
            sb.AppendLine(@"using AutoMapper;");
            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace,".Entities;"));
            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace,".Interfaces.Repositories;"));
            sb.AppendLine(string.Concat("using ",conf.InfraStructure.NameSpace,".DbContexts;"));
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

            sb.AppendLine(string.Concat(tab,tab, "public async Task<",entity,"Entity> GetByKey(",entity,"Entity entity)"));
            sb.AppendLine(string.Concat(tab,tab, "{"));
            sb.AppendLine(string.Concat(tab,tab,tab, "var retorno = await _context.",entity,".FindAsync(entity);"));
            sb.AppendLine(string.Concat(tab,tab,tab, "return _mapper.Map<",entity,"Entity>(retorno);"));
            sb.AppendLine(string.Concat(tab,tab, "}"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab, "public async Task<",entity,"Entity> Insert(",entity,"Entity entity)"));
            sb.AppendLine(string.Concat(tab,tab, "{"));
            sb.AppendLine(string.Concat(tab,tab,tab, "var retorno= await _context.",entity,".AddAsync(_mapper.Map<Models.",entity,">(entity));"));
            sb.AppendLine(string.Concat(tab,tab,tab, "return _mapper.Map<",entity,"Entity>(retorno.Entity);"));
            sb.AppendLine(string.Concat(tab,tab, "}"));
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab, "public async Task<",entity,"Entity> Update(",entity,"Entity entity)"));
            sb.AppendLine(string.Concat(tab,tab, "{"));
            sb.AppendLine(string.Concat(tab,tab,tab, "var entityFound = await _context.",entity,".FindAsync(entity);"));

            sb.AppendLine(string.Concat(tab,tab,tab, "_mapper.Map(entity,entityFound);"));
            sb.AppendLine(string.Concat(tab,tab,tab, "var retorno = _context.",entity,".Update(entityFound);"));

            sb.AppendLine(string.Concat(tab,tab,tab, "return _mapper.Map<",entity,"Entity>(retorno.Entity);"));
            sb.AppendLine(string.Concat(tab,tab, "}"));
            sb.AppendLine();


            sb.AppendLine(string.Concat(tab, "}"));
            sb.AppendLine(@"}");

            return sb.ToString();

        }

    }

}