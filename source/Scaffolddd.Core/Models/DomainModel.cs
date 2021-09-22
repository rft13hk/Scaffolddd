namespace Scaffolddd.Core.Models
{
    public class DomainModel: BaseModel
    {
        public DomainModel()
        {
            Paths = new DomainPathsModel();   
        }

        public DomainPathsModel Paths { get; set; }

    }
}