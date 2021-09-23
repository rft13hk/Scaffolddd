using System;

namespace Scaffolddd.Core.Models
{
    public class DomainModel: BaseModel 
    {
        #region Implementation
        public string EntitiesPath { get ; set; }
        public string EntitiesFullPath() { return GetPath(EntitiesPath); }
        public string HelpersPath { get ; set; }
        public string HelpersPathFullPath() { return GetPath(HelpersPath); }
        public string ServicesPath { get ; set; }
        public string ServicesPathFullPath() { return GetPath(ServicesPath); }
        public string ValidationsPath { get ; set; }
        public string ValidationsPathFullPath() { return GetPath(ValidationsPath); }
        public string InterfacePath { get; set; }
        public string InterfaceFullPath() { return GetPath(InterfacePath); }
        #endregion 


        #region Interfaces

        public string Interface_InfrastructurePath { get; set; }
        public string Interface_InfrastructureFullPath() { return string.Concat(InterfaceFullPath(), Interface_InfrastructurePath); }
        public string Interface_RepositoriesPath { get; set; }
        public string Interface_RepositoriesFullPath() { return string.Concat(InterfaceFullPath(), Interface_RepositoriesPath); }
        public string Interface_ServicesPath { get; set; }
        public string Interface_ServicesFullPath() { return string.Concat(InterfaceFullPath(), Interface_ServicesPath); }
        public string Interface_ValidationsPath { get; set; }
        public string Interface_ValidationsFullPath() { return string.Concat(InterfaceFullPath(), Interface_ValidationsPath); }

        #endregion       
    }
}