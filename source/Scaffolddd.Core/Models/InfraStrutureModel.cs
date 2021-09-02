namespace Scaffolddd.Core.Models
{
    public class InfraStructureModel: BaseModel
    {
        public string NameDbContext { get; set; }

        public InfraStructureModel()
        {
            Paths = new InfraStruturePathsModel();
        }

        public InfraStruturePathsModel Paths { get; set; }
    }
}