﻿using System;
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
            Console.WriteLine("Scaffolddd process starting...");
            Console.WriteLine();

            string path = Directory.GetCurrentDirectory();

            var runInLinux = (path.IndexOf(@"/") != -1);

            #region  Tamplate for start conf

            var confTemplate = new ScaffoldddModel();

            confTemplate.ProjectName = "<Name of your Project>";
            confTemplate.BackupOld = true;
            confTemplate.OverWrite = false;

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


            confTemplate.InfraStructure.NameSpace = "<Namespace of project infrastructure>";
            confTemplate.InfraStructure.NameDbContext = "<Name of DbContext File>";
            confTemplate.InfraStructure.Paths.Project = @"<path of project>";
            confTemplate.InfraStructure.Paths.DbContext =  @"<Diretory of DbContexts>";
            confTemplate.InfraStructure.Paths.Models = @"<Diretory of Models>";
            confTemplate.InfraStructure.Paths.Repositories = @"<Diretory of Repositories>";

            confTemplate.Domain.NameSpace = "<Namespace of project Domain>";
            confTemplate.Domain.Paths.Project = @"<Path of Project>";

            confTemplate.Domain.Paths.Entities = @"<Path of Entities>";
            confTemplate.Domain.Paths.Infrastructure = @"<Path of interfaces the Interfaces/Infrastructure>";
            confTemplate.Domain.Paths.Repositories = @"<Path of interfaces the Interfaces/Repositories>";
            confTemplate.Domain.Paths.Services = @"<Path of interfaces the Interfaces/Services>";

            confTemplate.Application.NameSpace = "<Namespace of project Application>";
            confTemplate.Application.Paths.Project = @"<path of project>";
            confTemplate.Application.Paths.MappingProfile = @"<Directory of MappingProfile";
            confTemplate.Application.Paths.InjectionMapping = @"<Directory of InjectionMapping>";
            confTemplate.Application.Paths.DTO = @"<Directory of DTOs>";

            #endregion

            var configurationFile = string.Concat(path,"/Scaffolddd.json");

            if (!File.Exists(configurationFile))
            {
                string json = JsonSerializer.Serialize(confTemplate);
                File.WriteAllText(configurationFile, json);
            }

            if (File.Exists(configurationFile))
            {
                var text = File.ReadAllText(configurationFile);

                var conf = JsonSerializer.Deserialize<ScaffoldddModel>(text);

                var processo = new Process(conf);
            
                processo.Start();

            }

            //Console.WriteLine(path); 

            Console.WriteLine("Process completed successfully");

        }
    }

    
}
