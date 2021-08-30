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

        public void ProcessEntities(bool onlyNotFound)
        {
            foreach (var item in _lstFilesModels)
            {
                if (File.Exists(item))
                {
                    string readText = File.ReadAllText(item);
                    
                    Console.WriteLine(new string('>',50));
                    Console.WriteLine(readText);

                    Console.WriteLine(new string('<',50));

                    var newtext = StringUtils.Replace(readText,_dicSwapEntity);

                    Console.WriteLine(newtext);

                }
                
            }
        }

        public void ProcessDtos(bool onlyNotFound)
        {
            foreach (var item in _lstFilesModels)
            {
                if (File.Exists(item))
                {
                    string readText = File.ReadAllText(item);
                    
                    Console.WriteLine(new string('>',50));
                    Console.WriteLine(readText);

                    Console.WriteLine(new string('<',50));

                    var newtext = StringUtils.Replace(readText,_dicSwapDto);

                    Console.WriteLine(newtext);

                }
                
            }
        }

        public void ProcessInterfaces(bool onlyNotFound)
        {
            var readText = Templates.GetTextForInterfaces(_conf,tab);

            foreach (var item in _lstNameModels)
            {
                var newtext = readText.Replace("[[CLASS]]",item);

                Console.WriteLine(newtext);
            }
        }

        public void ProcessRepositories(bool onlyNotFound)
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

    }
}
