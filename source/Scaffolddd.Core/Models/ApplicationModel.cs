namespace Scaffolddd.Core.Models
{
    public class ApplicationModel: BaseModel
    {
        public ApplicationModel()
        {
            Paths = new ApplicationPathsModel();
        }
        public ApplicationPathsModel Paths { get; set; }

    }
}