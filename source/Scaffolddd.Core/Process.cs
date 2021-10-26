using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Scaffolddd.Core.Helpers;
using Scaffolddd.Core.Models;
using Scaffolddd.Core.Templates;

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
            _lstFilesModels = FileUtils.ProcessDirectory(_conf.InfraStructure.GetPath(_conf.InfraStructure.ModelsPath));
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

                    var pathFile = string.Concat(_conf.Domain.EntitiesPath, "/", entity,"Entity.cs"); 

                    FileUtils.WriteFile(newtext,pathFile, _conf.OverWrite, _conf.BackupOld);
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

                    var pathFile = string.Concat(_conf.Application.GetPath(_conf.Application.DTOPath), "/", dto,"Dto.cs"); 

                    FileUtils.WriteFile(newtext,pathFile, _conf.OverWrite, _conf.BackupOld);
                }
            }
        }

        private void ProcessInterfaces(bool onlyNotFound)
        {
            var readText = InterfacesTemplate.MakeTemplate(_conf,tab);

            foreach (var item in _lstNameModels)
            {
                var newtext = readText.Replace("[[CLASS]]",item);

                var pathFile = string.Concat(_conf.Domain.Interface_RepositoriesFullPath(), "/I", item,"Repository.cs"); 

                FileUtils.WriteFile(newtext,pathFile, _conf.OverWrite, _conf.BackupOld);
            }
        }

        private void ProcessRepositories(bool onlyNotFound)
        {
            foreach (var item in _lstNameModels)
            {
                var readText = RepositoryTemplate.MakeTemplate(_conf,tab,item);

                var newtext = readText.Replace("[[CLASS]]",item);

                var pathFile = string.Concat(_conf.InfraStructure.GetPath(_conf.InfraStructure.RepositoriesPath), "/", item,"Repository.cs"); 

                FileUtils.WriteFile(newtext,pathFile, _conf.OverWrite, _conf.BackupOld);
            }
        }

        private void ProcessValidation(bool onlyNotFound)
        {
            foreach (var item in _lstNameModels)
            {
                var readText = ValidationsTemplate.MakeTemplate(_conf,tab,item);

                var pathFile = string.Concat(_conf.Domain.ValidationsPathFullPath(), "/", item,"Validation.cs"); 

                FileUtils.WriteFile(readText,pathFile, _conf.OverWrite, _conf.BackupOld);
            }
        }

        private void ProcessMapping(bool onlyNotFound)
        {
            var newtext = MappingTemplate.MakeTemplate(_conf,tab,_dicSwapDto, _dicSwapEntity);

            var pathFile = string.Concat(_conf.Application.GetPath(_conf.Application.MappingProfilePath),"/MappingProfile.cs");

            FileUtils.WriteFile(newtext,pathFile, _conf.OverWrite, _conf.BackupOld);
        }

        private void ProcessDependencyInjectionMapping(bool onlyNotFound)
        {
            var newtext = DependencyInjectionMappingTemplate.MakeTemplate(_conf,tab,_dicSwapDto, _dicSwapEntity);

            var pathFile = string.Concat(_conf.Application.GetPath(_conf.Application.MappingProfilePath),"/MappingProfile.cs");

            FileUtils.WriteFile(newtext,pathFile, _conf.OverWrite, _conf.BackupOld);
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
                pathFile = string.Concat(_conf.Domain.Interface_InfrastructureFullPath(), "/IUnitOfWork.cs"); 

                template = IUnitOfWorkTemplate.MakeTemplate(_conf,tab);

                FileUtils.WriteFile(template,pathFile, _conf.OverWrite, _conf.BackupOld);
            }

            if (_conf.Flags.GenerateUnitOfWork)
            {
                pathFile = string.Concat(_conf.InfraStructure.GetPath( _conf.InfraStructure.DbContextPath), "/UnitOfWork.cs"); 

                template = UnitOfWorkTemplate.MakeTemplate(_conf,tab);

                FileUtils.WriteFile(template,pathFile, _conf.OverWrite, _conf.BackupOld);
            }

            #endregion

            #region IBaseRepository, BaseRepository

            if (_conf.Flags.GenerateIBaseRepository)
            {
                pathFile = string.Concat(_conf.Domain.Interface_RepositoriesFullPath(), "/IBaseRepository.cs"); 

                template = IBaseRepositoryTemplate.MakeTemplate(_conf,tab);

                FileUtils.WriteFile(template,pathFile, _conf.OverWrite, _conf.BackupOld);
            }

            if (_conf.Flags.GenerateBaseRepository)
            {
                pathFile = string.Concat(_conf.InfraStructure.GetPath(_conf.InfraStructure.RepositoriesPath) , "/BaseRepository.cs"); 

                template = BaseRepositoryTemplate.MakeTemplate(_conf,tab);

                FileUtils.WriteFile(template,pathFile, _conf.OverWrite, _conf.BackupOld);
            }

            #endregion

            #region IBaseValidation, BaseValidation

            if (_conf.Flags.GenerateIBaseValidation)
            {
                pathFile = string.Concat(_conf.Domain.Interface_ValidationsFullPath(), "/IBaseValidation.cs"); 

                template = IBaseValidationTemplate.MakeTemplate(_conf,tab);

                FileUtils.WriteFile(template,pathFile, _conf.OverWrite, _conf.BackupOld);
            }

            if (_conf.Flags.GenerateBaseValidation)
            {
                pathFile = string.Concat(_conf.Domain.ValidationsPathFullPath(), "/BaseValidation.cs"); 

                template = BaseValidationTemplate.MakeTemplate(_conf,tab);

                FileUtils.WriteFile(template,pathFile, _conf.OverWrite, _conf.BackupOld);
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

            //Passo 8 - Criar as Validations
            if (_conf.Flags.GenerateValidaions)
            {
                ProcessValidation(!_conf.OverWrite);
            }
        }
    }
}
