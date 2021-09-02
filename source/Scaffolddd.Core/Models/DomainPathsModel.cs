namespace Scaffolddd.Core.Models
{
    public class DomainPathsModel: PathBaseModel
    {
        public string Entities { get; set; }
        public string Infrastructure { get; set; }
        public string Repositories { get; set; }
        public string Services { get; set; }
    }
}