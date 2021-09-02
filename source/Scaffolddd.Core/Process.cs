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

        private void WriteFile(string text, string pathFile)
        {
            var fileExist = File.Exists(pathFile);

            if (_conf.BackupOld && fileExist && _conf.OverWrite)
            {
                File.Copy(pathFile, string.Concat(pathFile,"_bk_",DateTime.Now.ToString("yyyyMMdd-HHmmss") ),true);
            }

            if (fileExist && _conf.OverWrite)
            {
                File.Delete(pathFile);
            }

            if (!File.Exists(pathFile))
            {
                File.WriteAllText(pathFile,text);
            }
        }

        private void ProcessEntities(bool onlyNotFound)
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
            var readText = Templates.GetTextForInterfaces(_conf,tab);

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
                var readText = Templates.GetTextForRepository(_conf,tab,item);

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
            var newtext = Templates.GetTextForMapping(_conf,tab,_dicSwapDto, _dicSwapEntity);

            var pathFile = string.Concat(_conf.Application.Paths.GetPath(_conf.Application.Paths.MappingProfile),"/MappingProfile.cs");

            WriteFile(newtext,pathFile);
            // if (!File.Exists(pathFile))
            // {
            //     File.WriteAllText(pathFile,newtext);
            // }


        }

        private void ProcessDependencyInjectionMapping(bool onlyNotFound)
        {



            var newtext = Templates.GetTextForDependencyInjectionMapping(_conf,tab,_dicSwapDto, _dicSwapEntity);

            var pathFile = string.Concat(_conf.Application.Paths.GetPath(_conf.Application.Paths.MappingProfile),"/MappingProfile.cs");

            WriteFile(newtext,pathFile);
            // if (!File.Exists(pathFile))
            // {
            //     File.WriteAllText(pathFile,newtext);
            // }

        }


        public string GetTemplate()
        {
            return Templates.GetTextForRepository(_conf,tab,"Abacaxi");
        }


        public void Start()
        {
            LoadFiles();

            GenerateSwapNames();

            #region Passo 1 - Criar as Classes e Interfaces Base caso as mesmas nao existao: IUnitOfWork, UnitOfWork, IBaseRepository, BaseRepositori

            #region IUnitOfWork, UnitOfWork

            string pathFile = string.Concat(_conf.Domain.Paths.GetPath(_conf.Domain.Paths.Infrastructure), "/IUnitOfWork.cs"); 

            //if (!File.Exists(pathFile))
            //{
                var template = Templates.GetTextForIUnitOfWork(_conf,tab);

                // Gravar arquivo...
                //File.WriteAllText(pathFile,template);
                WriteFile(template,pathFile);
            //}


            pathFile = string.Concat(_conf.InfraStructure.Paths.GetPath( _conf.InfraStructure.Paths.DbContext), "/UnitOfWork.cs"); 

            //if (!File.Exists(pathFile))
            //{
                template = Templates.GetTextForUnitOfWork(_conf,tab);

                // Gravar arquivo...
                //File.WriteAllText(pathFile,template);
                WriteFile(template,pathFile);
            //}


            #endregion

            #region IBaseRepository, BaseRepository

            pathFile = string.Concat(_conf.Domain.Paths.GetPath( _conf.Domain.Paths.Repositories), "/IBaseRepository.cs"); 

            //if (!File.Exists(pathFile))
            //{
                template = Templates.GetTextForIBaseRepository(_conf,tab);

                // Gravar arquivo...
                //File.WriteAllText(pathFile,template);
                WriteFile(template,pathFile);
            //}

            pathFile = string.Concat(_conf.InfraStructure.Paths.GetPath(_conf.InfraStructure.Paths.Repositories) , "/BaseRepository.cs"); 

            //if (!File.Exists(pathFile))
            //{
                template = Templates.GetTextForBaseRepository(_conf,tab);

                // Gravar arquivo...
                //File.WriteAllText(pathFile,template);
                WriteFile(template,pathFile);
            //}

            #endregion

            #endregion

            //Passo 2 - Criar as Entidades
            ProcessEntities(!_conf.OverWrite);

            //Passo 3 - Criar os DTOs
            ProcessDtos(!_conf.OverWrite);

            //Passo 4 - Criar as Interfaces dos Repositorios
            ProcessInterfaces(!_conf.OverWrite);

            //Passo 5 - Criar os Repositorios
            ProcessRepositories(!_conf.OverWrite);

            //Passo 6 - Criar os mapeamentos
            ProcessMapping(!_conf.OverWrite);

            //Passo 7 - Criar os mapeamentos de injeçao de dependencia
            //ProcessDependencyInjectionMapping(!_conf.OverWrite);
        }
    }
}
