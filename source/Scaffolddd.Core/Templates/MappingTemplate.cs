using System.Collections.Generic;
using System.Text;
using Scaffolddd.Core.Models;

namespace Scaffolddd.Core.Templates
{
    internal static class MappingTemplate
    {
        internal static string MakeTemplate(ScaffoldddModel conf, string tab, Dictionary<string,string> _dicSwapDto, Dictionary<string,string> _dicSwapEntity)
        {
            StringBuilder sb = new StringBuilder();
             
            sb.AppendLine(@"using AutoMapper;");
            sb.AppendLine(@"using System.Text;");
            sb.AppendLine(string.Concat("using ",conf.Application.NameSpace,".DTOs;"));
            sb.AppendLine(string.Concat("using ",conf.Domain.NameSpace,".Entities;"));
            sb.AppendLine(string.Concat("using ",conf.InfraStructure.NameSpace,".Models;"));
            sb.AppendLine();

            sb.AppendLine(string.Concat("namespace ", conf.Application.NameSpace,".Implementation"));
            sb.AppendLine("{");

            sb.AppendLine(string.Concat(tab,"public class MappingProfile : Profile"));
            sb.AppendLine(string.Concat(tab,@"{"));

            sb.AppendLine(string.Concat(tab,tab, @"public MappingProfile()"));
            sb.AppendLine(string.Concat(tab,tab, @"{"));
            
            sb.AppendLine(string.Concat(tab,tab,tab, @"#region API"));

            sb.AppendLine();
            foreach (var item in _dicSwapDto)
            {
                sb.AppendLine(string.Concat(tab,tab,tab, string.Concat("CreateMap<", item.Key,"Entity, ",item.Value,">().ReverseMap();" )));
            }
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab,tab, @"#endregion"));

            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab,tab, @"#region Domain"));
            sb.AppendLine();
            foreach (var item in _dicSwapEntity)
            {
                sb.AppendLine(string.Concat(tab,tab,tab, string.Concat("CreateMap<", item.Key,", ",item.Value,">().ReverseMap();" )));
            }
            sb.AppendLine();

            sb.AppendLine(string.Concat(tab,tab,tab, @"#endregion"));
            sb.AppendLine();


            sb.AppendLine(string.Concat(tab,tab, @"}"));

            sb.AppendLine(string.Concat(tab,@"}"));
            
            sb.AppendLine("}");

            return sb.ToString();
        }

    }
}
