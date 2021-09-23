namespace Scaffolddd.Core.Models
{
    public class DomainPathsModel: PathBaseModel
    {
        public DomainPathsImplementationModel Implementation { get; set; }
        public DomainPathsInterfacesModel Interface { get; set; }

        public DomainPathsModel()
        {
            Implementation = new DomainPathsImplementationModel();
            Interface = new DomainPathsInterfacesModel();
        }

    }
}