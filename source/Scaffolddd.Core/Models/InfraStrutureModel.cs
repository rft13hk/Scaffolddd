namespace Scaffolddd.Core.Models
{
    public class InfraStructureModel: BaseModel
    {
        public string NameDbContext { get; set; }

        public string ModelsPath { get; set; }
        public string ModelsFullPath() { return GetPath(ModelsPath); }
        public string DbContextPath { get; set; }
        public string DbContextFullPath() { return GetPath(DbContextPath); }
        public string RepositoriesPath { get; set; }
        public string RepositoriesFullPath() { return GetPath(RepositoriesPath); }
    }
}