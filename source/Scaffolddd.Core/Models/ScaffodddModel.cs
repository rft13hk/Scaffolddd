namespace Scaffolddd.Core.Models
{
    public class ScaffoldddModel
    {
        
        public ScaffoldddModel()
        {
            Application = new ApplicationModel();
            Domain = new DomainModel();    
            InfraStructure = new InfraStructureModel();
        }

        public string ProjectName { get; set; } 

        public ApplicationModel Application { get; set; }
        public DomainModel Domain  { get; set; }
        public InfraStructureModel InfraStructure { get; set; }


        
        
    }
}