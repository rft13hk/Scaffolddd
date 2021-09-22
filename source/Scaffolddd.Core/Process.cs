using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Scaffolddd.Core.Helpers;
using Scaffolddd.Core.Models;
using Scaffolddd.Core.Resource;

namespace Scaffolddd.Core
{
    public class Process
    {
        const string tab= "    ";

        private ScaffoldddModel _conf;

        private List<string> _lstFilesModels;
        private List<string> _lstNameModels;

        private Dictionary<string,string> _dicSwapEntity;
        private Dictionary<string,string> _dicSwapDto;
        private Dictionary<string,string> _dicSwapRepository;

        public Process(ScaffoldddModel conf)
        {
            _conf = conf;

        }

        private void LoadFiles()
        {
            _lstFilesModels = FileUtils.ProcessDirectory(_conf.InfraStructure.Paths.GetPath(_conf.InfraStructure.Paths.Models));
            _lstNameModels = new List<string>();
            _lstFilesModels.ForEach( f => _lstNameModels.Add(FileUtils.ExtractNameFromPath(f).Replace(".cs","")));

        }


        private void GenerateSwapNames()
        {
            _dicSwapEntity = new Dictionary<string,string>();
            _dicSwapDto = new Dictionary<string, string>();
            _dicSwapRepository = new Dictionary<string, string>();

            _lstNameModels.ForEach(f => _dicSwapEntity.Add(f,string.Concat(f,"Entity")));
            _lstNameModels.ForEach(f => _dicSwapDto.Add(f,string.Concat(f,"Dto")));
            _lstNameModels.ForEach(f => _dicSwapRepository .Add(f,string.Concat(f,"Repository")));
        }


        private static bool CompareString(string text1, string text2)
        {
            return (text1.RemoveBreakLine().RemoveWhitespace().ToUpper() == text2.RemoveBreakLine().RemoveWhitespace().ToUpper());
        }

        private void WriteFile(string text, string pathFileDest, string pathFileSource = null)
        {
            #region Compare Old and New

            var fileExist = File.Exists(pathFileDest);

            if (fileExist)
            {
                var oldText = File.ReadAllText(pathFileDest);

                if (CompareString(text, oldText))
                {
                    //Is Equal then exit;
                    return;
                }
            }
            #endregion

            var nowName = DateTime.Now.ToString("yyyyMMdd-HHmmss");

            if (_conf.BackupOld && fileExist && _conf.OverWrite)
            {
                // Create Backup 
                File.Copy(pathFileDest, string.Concat(pathFileDest,"_Old_", nowName ),true);
            }

            if (fileExist && _conf.OverWrite)
            {
                File.Delete(pathFileDest);
            }

            if (!File.Exists(pathFileDest))
            {
                File.WriteAllText(pathFileDest,text);
            }
            else
            {
                File.WriteAllText(string.Concat(pathFileDest,"_New_", nowName ),text);
            }
        }

        private void ProcessEntities()
        {
            foreach (var item in _lstFilesModels)
            {
                if (File.Exists(item))
                {
                    string readText = File.ReadAllText(item);
                    
                    var newtext = StringUtils.Replace(readText,_dicSwapEntity);

                    newtext = newtext.Replace(string.Concat(_conf.InfraStructure.NameSpace,".Models") 
                        , string.Concat(_conf.Domain.NameSpace,".Entities") );

                    var entity = Helpers.FileUtils.ExtractNameFromPath(item).Replace(".cs","");

                    var pathFile = string.Concat(_conf.Domain.Paths.GetPath(_conf.Domain.Paths.Entities), "/", entity,"Entity.cs"); 

                    WriteFile(newtext,pathFile);
                }
                
            }
        }

        private void ProcessDtos(bool onlyNotFound)
        {
            foreach (var item in _lstFilesModels)
            {
                if (File.Exists(item))
                {
                    string readText = File.ReadAllText(item);
                    
                    var newtext = StringUtils.Replace(readText,_dicSwapDto);

                    newtext = newtext.Replace(string.Concat(_conf.InfraStructure.NameSpace,".Models") 
                        , string.Concat(_conf.Application.NameSpace,".DTOs") );

                    var dto = Helpers.FileUtils.ExtractNameFromPath(item).Replace(".cs","");

                    var pathFile = string.Concat(_conf.Application.Paths.GetPath(_conf.Application.Paths.DTO), "/", dto,"Dto.cs"); 

                    WriteFile(newtext,pathFile);
                    // if (!File.Exists(pathFile))
                    // {
                    //     File.WriteAllText(pathFile,newtext);
                    // }
                }
                
            }
        }

        private void ProcessInterfaces(bool onlyNotFound)
        {
            var readText = InterfacesTemplate.MakeTemplate(_conf,tab);

            foreach (var item in _lstNameModels)
            {
                var newtext = readText.Replace("[[CLASS]]",item);

                var pathFile = string.Concat(_conf.Domain.Paths.GetPath(_conf.Domain.Paths.Repositories), "/I", item,"Repository.cs"); 

                WriteFile(newtext,pathFile);
                // if (!File.Exists(pathFile))
                // {
                //     File.WriteAllText(pathFile,newtext);
                // }
            }
        }

        private void ProcessRepositories(bool onlyNotFound)
        {
            foreach (var item in _lstNameModels)
            {
                var readText = RepositoryTemplate.MakeTemplate(_conf,tab,item);

                var newtext = readText.Replace("[[CLASS]]",item);

                var pathFile = string.Concat(_conf.InfraStructure.Paths.GetPath(_conf.InfraStructure.Paths.Repositories), "/", item,"Repository.cs"); 

                WriteFile(newtext,pathFile);
                // if (!File.Exists(pathFile))
                // {
                //     File.WriteAllText(pathFile,newtext);
                // }
            }
        }

        private void ProcessMapping(bool onlyNotFound)
        {
            var newtext = MappingTemplate.MakeTemplate(_conf,tab,_dicSwapDto, _dicSwapEntity);

            var pathFile = string.Concat(_conf.Application.Paths.GetPath(_conf.Application.Paths.MappingProfile),"/MappingProfile.cs");

            WriteFile(newtext,pathFile);
            // if (!File.Exists(pathFile))
            // {
            //     File.WriteAllText(pathFile,newtext);
            // }


        }

        private void ProcessDependencyInjectionMapping(bool onlyNotFound)
        {

            var newtext = DependencyInjectionMappingTemplate.MakeTemplate(_conf,tab,_dicSwapDto, _dicSwapEntity);

            var pathFile = string.Concat(_conf.Application.Paths.GetPath(_conf.Application.Paths.MappingProfile),"/MappingProfile.cs");

            WriteFile(newtext,pathFile);

            // if (!File.Exists(pathFile))
            // {
            //     File.WriteAllText(pathFile,newtext);
            // }

        }

        public void Start()
        {
            LoadFiles();

            GenerateSwapNames();

            #region Passo 1 - Criar as Classes e Interfaces Base caso as mesmas nao existao: IUnitOfWork, UnitOfWork, IBaseRepository, BaseRepositori

            #region IUnitOfWork, UnitOfWork

            string template, pathFile;

            if (_conf.Flags.GenerateIUnitOfWork)
            {
                pathFile = string.Concat(_conf.Domain.Paths.GetPath(_conf.Domain.Paths.Infrastructure), "/IUnitOfWork.cs"); 

                template = IUnitOfWorkTemplate.MakeTemplate(_conf,tab);

                WriteFile(template,pathFile);
            }

            if (_conf.Flags.GenerateUnitOfWork)
            {
                pathFile = string.Concat(_conf.InfraStructure.Paths.GetPath( _conf.InfraStructure.Paths.DbContext), "/UnitOfWork.cs"); 

                template = UnitOfWorkTemplate.MakeTemplate(_conf,tab);

                WriteFile(template,pathFile);
            }

            #endregion

            #region IBaseRepository, BaseRepository

            if (_conf.Flags.GenerateIBaseRepository)
            {
                pathFile = string.Concat(_conf.Domain.Paths.GetPath( _conf.Domain.Paths.Repositories), "/IBaseRepository.cs"); 

                //if (!File.Exists(pathFile))
                //{
                    template = IBaseRepositoryTemplate.MakeTemplate(_conf,tab);

                    // Gravar arquivo...
                    //File.WriteAllText(pathFile,template);
                    WriteFile(template,pathFile);
                //}
            }

            if (_conf.Flags.GenerateBaseRepository)
            {
                pathFile = string.Concat(_conf.InfraStructure.Paths.GetPath(_conf.InfraStructure.Paths.Repositories) , "/BaseRepository.cs"); 

                //if (!File.Exists(pathFile))
                //{
                    template = BaseRepositoryTemplate.MakeTemplate(_conf,tab);

                    // Gravar arquivo...
                    //File.WriteAllText(pathFile,template);
                    WriteFile(template,pathFile);
                //}
            }

            #endregion

            #endregion

            //Passo 2 - Criar as Entidades
            if (_conf.Flags.GenerateEntities)
            {
                ProcessEntities();
            }

            //Passo 3 - Criar os DTOs
            if (_conf.Flags.GenerateDTOs)
            {
                ProcessDtos(!_conf.OverWrite);
            }

            //Passo 4 - Criar as Interfaces dos Repositorios
            if (_conf.Flags.GenerateRepositoryInterfaces)
            {
                ProcessInterfaces(!_conf.OverWrite);
            }

            //Passo 5 - Criar os Repositorios
            if (_conf.Flags.GenerateRepositories)
            {
                ProcessRepositories(!_conf.OverWrite);
            }

            //Passo 6 - Criar os mapeamentos
            if (_conf.Flags.GenerateMappings)
            {
                ProcessMapping(!_conf.OverWrite);
            }

            //Passo 7 - Criar os mapeamentos de injeçao de dependencia
            if (_conf.Flags.GenerateDependencyInjection)
            {
                //ProcessDependencyInjectionMapping(!_conf.OverWrite);
            }
        }


    }
}
