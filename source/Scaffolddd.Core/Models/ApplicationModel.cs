namespace Scaffolddd.Core.Models
{
    public class ApplicationModel: BaseModel
    {
        public string MappingProfilePath { get; set; }
        public string MappingProfileFullPath() { return GetPath(MappingProfilePath); }
        public string InjectionMappingPath { get; set; }
        public string InjectionMappingFullPath() { return GetPath(InjectionMappingPath); }
        public string DTOPath { get; set; }
        public string DTOFullPath() { return GetPath(DTOPath); }
    }
}