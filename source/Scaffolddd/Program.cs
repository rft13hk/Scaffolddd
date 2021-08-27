using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Scaffolddd.Core.Helpers;
using Scaffolddd.Core.Models;

namespace Scaffolddd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Scaffolddd process starting...");

            var conf = new ScaffoldddModel();

            conf.ProjectName = "DtmSysAdmin";

            conf.InfraStructure.ProjectName = "DtmSysAdmin.Infrastructure.Data";
            conf.InfraStructure.NameSpace = "DtmSysAdmin.Infrastructure.Data";
            
            conf.InfraStructure.PathForDbContext = @"/home/ronaldo/GitLab/dtmsysadmin/source/3-Infrastructure/DtmSysAdmin.Infrastructure.Data/DbContexts";
            conf.InfraStructure.PathForModels = @"/home/ronaldo/GitLab/dtmsysadmin/source/3-Infrastructure/DtmSysAdmin.Infrastructure.Data/Models";
            conf.InfraStructure.PathForRepositories = @"/home/ronaldo/GitLab/dtmsysadmin/source/3-Infrastructure/DtmSysAdmin.Infrastructure.Data/Repositories";

            conf.Domain.ProjectName = "DtmSysAdmin.Domain";
            conf.Domain.NameSpace = "DtmSysAdmin.Domain";

            conf.Domain.PathForEntities = @"/home/ronaldo/GitLab/dtmsysadmin/source/2-Domain/DtmSysAdmin.Domain/Entities";
            conf.Domain.PathForInterfaces = @"/home/ronaldo/GitLab/dtmsysadmin/source/2-Domain/DtmSysAdmin.Domain/Entities";

            conf.Application.ProjectName = "";
            conf.Application.NameSpace = "";

            conf.Application.PathForMappingProfile = @"";
            conf.Application.PathForInjectionMapping = @"";
            conf.Application.PathForDTO = @"";

            //Roteiro:

            #region Passo 1:
            /*
                - Ler todos os nomes de arquivos que estao na pasta dos Models na infrastructure;
            */

            var lstFilesModels = FileUtils.ProcessDirectory(conf.InfraStructure.PathForModels);
            var lstNameModels = new List<string>();

            lstFilesModels.ForEach( f => lstNameModels.Add(FileUtils.ExtractNameFromPath(f).Replace(".cs","")));

            #endregion


            

            // Open firt file
            var file = lstFilesModels[0];

            StringBuilder sb = new StringBuilder();

            var filePath = file;//String.Concat(conf.InfraStructure.PathForModels,@"/", file);

            Console.WriteLine("");
            Console.WriteLine(filePath);
            Console.WriteLine(FileUtils.ExtractNameFromPath(filePath));


            if (File.Exists(filePath))
            {
                string readText = File.ReadAllText(filePath);
                Console.WriteLine(readText);

                var dicSwap = new Dictionary<string,string>();

                lstNameModels.ForEach(f => dicSwap.Add(f,string.Concat(f,"Entity")));

                foreach (var item in dicSwap)
                {
                    Console.WriteLine("De[{0}] -> [{1}]", item.Key, item.Value);    
                }


                

                Console.WriteLine(new string('-',50));

                var newtext = StringUtils.Replace(readText,dicSwap);

                Console.WriteLine(newtext);

            }




        }
    }

    
}
