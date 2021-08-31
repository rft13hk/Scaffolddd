using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Scaffolddd.Core;
using Scaffolddd.Core.Helpers;
using Scaffolddd.Core.Models;

namespace Scaffolddd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Scaffolddd process starting...");
            Console.WriteLine();

            string path = Directory.GetCurrentDirectory();

            var runInLinux = (path.IndexOf(@"/") != -1);

            #region  Configuração inicial
            var conf = new ScaffoldddModel();

            conf.ProjectName = "DtmSysAdmin";

            conf.InfraStructure.ProjectName = "DtmSysAdmin.Infrastructure.Data";
            conf.InfraStructure.NameSpace = "DtmSysAdmin.Infrastructure.Data";
            conf.InfraStructure.NameDbContext = "DtmSysAdminContext";
            
            conf.InfraStructure.PathForDbContext =  @"/home/ronaldo/GitLab/dtmsysadmin/source/3-Infrastructure/DtmSysAdmin.Infrastructure.Data/DbContexts";
            conf.InfraStructure.PathForModels = @"/home/ronaldo/GitLab/dtmsysadmin/source/3-Infrastructure/DtmSysAdmin.Infrastructure.Data/Models";
            conf.InfraStructure.PathForRepositories = @"/home/ronaldo/GitLab/dtmsysadmin/source/3-Infrastructure/DtmSysAdmin.Infrastructure.Data/Repositories";


            conf.Domain.ProjectName = "DtmSysAdmin.Domain";
            conf.Domain.NameSpace = "DtmSysAdmin.Domain";

            conf.Domain.PathForEntities = @"/home/ronaldo/GitLab/dtmsysadmin/source/2-Domain/DtmSysAdmin.Domain/Entities";
            conf.Domain.PathForInterfaces = @"/home/ronaldo/GitLab/dtmsysadmin/source/2-Domain/DtmSysAdmin.Domain/Interfaces";

            conf.Application.ProjectName = "DtmSysAdmin.WebApi";
            conf.Application.NameSpace = "DtmSysAdmin.WebApi";

            conf.Application.PathForMappingProfile = @"";
            conf.Application.PathForInjectionMapping = @"";
            conf.Application.PathForDTO = @"/home/ronaldo/GitLab/dtmsysadmin/source/1-API/DtmSysAdmin.WebApi/DTOs";

            #endregion

            
            var processo = new Process(conf);
            

            //Console.WriteLine(path);

            processo.Start();

            Console.WriteLine("Process completed successfully");

        }
    }

    
}
