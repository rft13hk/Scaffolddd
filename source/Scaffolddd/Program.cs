#define TestPathx
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Scaffolddd.Core;
using Scaffolddd.Core.Helpers;
using Scaffolddd.Core.Models;

namespace Scaffolddd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("SCAFFOLDdd - Maker DDD Projects for .NET CORE 5 or >");
            Console.WriteLine("========== = ===== === ======== === ==== ==== = == =");
            Console.WriteLine("");
            Console.WriteLine("By Ronaldo Francisco Tolentino");
            Console.WriteLine("Contact: rft13hk@outlook.com");
            Console.WriteLine("");
            Console.WriteLine("Scaffolddd process starting...");
            Console.WriteLine();
            

            string path = Directory.GetCurrentDirectory();

            var runInLinux = (path.IndexOf(@"/") != -1);

            #region  Tamplate for start conf

            var confTemplate = new ScaffoldddModel();

            confTemplate.ProjectName = "[ProjecName]";
            confTemplate.BackupOld = true;
            confTemplate.OverWrite = false;

            //-----------------------------------------------------------------------------------------------
            #region Project Structure Flags
            confTemplate.Flags.GenerateIUnitOfWork = true;
            confTemplate.Flags.GenerateUnitOfWork = true;
            
            confTemplate.Flags.GenerateIBaseRepository = true;
            confTemplate.Flags.GenerateBaseRepository = true;
            
            confTemplate.Flags.GenerateIBaseService = true;
            confTemplate.Flags.GenerateBaseService = true;

            confTemplate.Flags.GenerateEntities = true;
            confTemplate.Flags.GenerateDTOs = true;

            confTemplate.Flags.GenerateRepositoryInterfaces = true;
            confTemplate.Flags.GenerateRepositories = true;

            confTemplate.Flags.GenerateMappings = true;
            confTemplate.Flags.GenerateDependencyInjection = true;

            confTemplate.Flags.GenerateIBaseValidation = true;
            confTemplate.Flags.GenerateBaseValidation = true;
            confTemplate.Flags.GenerateValidaions = true;
            #endregion
            //-----------------------------------------------------------------------------------------------
            confTemplate.InfraStructure.NameSpace =  String.Concat(confTemplate.ProjectName,".Infrastructure.Data");
            
            confTemplate.InfraStructure.NameDbContext = String.Concat( confTemplate.ProjectName,"DtmSysAdminContext");
            confTemplate.InfraStructure.DbContextPath =  @"Diretory of DbContexts";
            
            confTemplate.InfraStructure.PathRoot = @"path of project";

            confTemplate.InfraStructure.ModelsPath = @"Diretory of Models";
            confTemplate.InfraStructure.RepositoriesPath = @"Diretory of Repositories";

            //-----------------------------------------------------------------------------------------------
            confTemplate.Domain.NameSpace = "Namespace of project Domain";
            confTemplate.Domain.PathRoot = @"Path of Project";

            confTemplate.Domain.Interface_InfrastructurePath = @"Path Relative of interfaces the Infrastructure";
            confTemplate.Domain.Interface_RepositoriesPath = @"Path Relative of interfaces the IRepositories";
            confTemplate.Domain.Interface_ServicesPath = @"Path Relative of interfaces the IServices";
            confTemplate.Domain.Interface_ValidationsPath = @"Path Relative of interfaces the IValidations";


            confTemplate.Domain.EntitiesPath = @"Path Relative of Entities";
            confTemplate.Domain.ValidationsPath = @"Path Relative of Validations";
            confTemplate.Domain.ServicesPath = @"Path Relative of Services";

            //-----------------------------------------------------------------------------------------------
            confTemplate.Application.NameSpace = "Namespace of project Application";
            confTemplate.Application.PathRoot = @"path of project";
            confTemplate.Application.MappingProfilePath = @"Directory of MappingProfile";
            confTemplate.Application.InjectionMappingPath = @"Directory of InjectionMapping";
            confTemplate.Application.DTOPath = @"Directory of DTOs";

            #endregion

            var configurationFile = string.Concat(path,"/Scaffolddd.json");

            if (!File.Exists(configurationFile))
            {



                Console.WriteLine("WARNING!!! - Configuration file not found... ");
                Console.WriteLine("A new one was created in the execution directory of this program");
                Console.WriteLine("");
                Console.WriteLine("[Scaffolddd.json]");
                Console.WriteLine("");
                Console.WriteLine("Make changes to the file and run this program again.");
                Console.WriteLine("");


                string json = JsonSerializer.Serialize(confTemplate);
                File.WriteAllText(configurationFile, json);
            }
            else
            {
                var text = File.ReadAllText(configurationFile);

                var conf = JsonSerializer.Deserialize<ScaffoldddModel>(text);


#if TestPath
                Console.WriteLine(new string('-',80));
                Console.WriteLine("Path Application:");
                Console.WriteLine(conf.Application.PathRoot);
                Console.WriteLine(conf.Application.InjectionMappingFullPath());
                Console.WriteLine(conf.Application.MappingProfileFullPath());
                Console.WriteLine(conf.Application.DTOFullPath());
                Console.WriteLine(conf.Application.NameSpace);


                
                Console.WriteLine(new string('-',80));
                Console.WriteLine("Path Domain:");
                Console.WriteLine(conf.Domain.PathRoot);
                Console.WriteLine(conf.Domain.EntitiesFullPath());
                Console.WriteLine(conf.Domain.ValidationsPathFullPath());
                Console.WriteLine(conf.Domain.ServicesPathFullPath());
                Console.WriteLine(conf.Domain.HelpersPathFullPath());

                Console.WriteLine("-->>");
                Console.WriteLine(conf.Domain.InterfaceFullPath());
                Console.WriteLine(conf.Domain.Interface_InfrastructureFullPath());
                Console.WriteLine(conf.Domain.Interface_RepositoriesFullPath());
                Console.WriteLine(conf.Domain.Interface_ServicesFullPath());
                Console.WriteLine(conf.Domain.Interface_ValidationsFullPath());

                Console.WriteLine("<<--");

                Console.WriteLine(new string('-',80));
                Console.WriteLine("Path Infrastructure:");
                Console.WriteLine(conf.InfraStructure.PathRoot);
                Console.WriteLine(conf.InfraStructure.DbContextFullPath());
                Console.WriteLine(conf.InfraStructure.ModelsFullPath());
                Console.WriteLine(conf.InfraStructure.RepositoriesFullPath());


                Console.WriteLine(new string('-',80));


#else
                var processo = new Process(conf);
            
                processo.Start();

#endif

            }

            //Console.WriteLine(path); 

            Console.WriteLine("Process completed successfully");

        }
    }

    
}
