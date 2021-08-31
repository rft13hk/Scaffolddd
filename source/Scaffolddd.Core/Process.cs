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

            LoadFiles();

            GenerateSwapNames();
        }

        private void LoadFiles()
        {
            _lstFilesModels = FileUtils.ProcessDirectory(_conf.InfraStructure.PathForModels);
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

                    var pathFile = string.Concat(_conf.Domain.PathForEntities, "/", entity,"Entity.cs1"); 

                    if (!File.Exists(pathFile))
                    {
                        File.WriteAllText(pathFile,newtext);
                    }
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

                    var pathFile = string.Concat(_conf.Application.PathForDTO, "/", dto,"Dto.cs1"); 

                    if (!File.Exists(pathFile))
                    {
                        File.WriteAllText(pathFile,newtext);
                    }
                }
                
            }
        }

        private void ProcessInterfaces(bool onlyNotFound)
        {
            var readText = Templates.GetTextForInterfaces(_conf,tab);

            foreach (var item in _lstNameModels)
            {
                var newtext = readText.Replace("[[CLASS]]",item);

                Console.WriteLine(newtext);
            }
        }

        private void ProcessRepositories(bool onlyNotFound)
        {
            var readText = Templates.GetTextForBaseRepository(_conf,tab);

            foreach (var item in _lstNameModels)
            {
                var newtext = readText.Replace("[[CLASS]]",item);

                Console.WriteLine(newtext);
            }
        }

        public string GetTemplate()
        {
            return Templates.GetTextForRepository(_conf,tab,"Abacaxi");
        }


        public void Start()
        {
            #region Passo 1 - Criar as Classes e Interfaces Base caso as mesmas nao existao: IUnitOfWork, UnitOfWork, IBaseRepository, BaseRepositori

            #region IUnitOfWork, UnitOfWork
            string pathFile = string.Concat(_conf.InfraStructure.PathForDbContext, "/UnitOfWork.cs1"); 

            if (!File.Exists(pathFile))
            {
                var template = Templates.GetTextForUnitOfWork(_conf,tab);

                // Gravar arquivo...
                File.WriteAllText(pathFile,template);
            }

            pathFile = string.Concat(_conf.Domain.PathForInterfaces, "/Infrastructure/IUnitOfWork.cs1"); 

            if (!File.Exists(pathFile))
            {
                var template = Templates.GetTextForUnitOfWork(_conf,tab);

                // Gravar arquivo...
                File.WriteAllText(pathFile,template);
            }

            pathFile = string.Concat(_conf.Domain.PathForInterfaces, "/Infrastructure/IUnitOfWork.cs1"); 

            if (!File.Exists(pathFile))
            {
                var template = Templates.GetTextForUnitOfWork(_conf,tab);

                // Gravar arquivo...
                File.WriteAllText(pathFile,template);
            }

            #endregion

            #region IBaseRepository, BaseRepository

            pathFile = string.Concat(_conf.Domain.PathForInterfaces, "/Repositories/IBaseRepository.cs1"); 

            if (!File.Exists(pathFile))
            {
                var template = Templates.GetTextForIBaseRepository(_conf,tab);

                // Gravar arquivo...
                File.WriteAllText(pathFile,template);
            }

            pathFile = string.Concat(_conf.InfraStructure.PathForRepositories, "/BaseRepository.cs1"); 

            if (!File.Exists(pathFile))
            {
                var template = Templates.GetTextForBaseRepository(_conf,tab);

                // Gravar arquivo...
                File.WriteAllText(pathFile,template);
            }

            #endregion

            #endregion

            #region Passo 2 - Criar as Entidades

            ProcessEntities(true);

            ProcessDtos(true);

            #endregion

            #region Passo 3 - Criar os DTOs
            #endregion

            #region Passo 4 - Criar as Interfaces dos Repositorios
            #endregion

            #region Passo 5 - Criar os Repositorios
            #endregion
        }
    }
}
