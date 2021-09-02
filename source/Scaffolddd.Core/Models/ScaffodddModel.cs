namespace Scaffolddd.Core.Models
{
    public class ScaffoldddModel
    {
        
        public ScaffoldddModel()
        {
            Application = new ApplicationModel();
            Domain = new DomainModel();    
            InfraStructure = new InfraStructureModel();
            BackupOld = true;
            OverWrite = false;
        }

        public string ProjectName { get; set; } 
        public bool OverWrite { get; set; }
        public bool BackupOld { get; set; }


        public ApplicationModel Application { get; set; }
        public DomainModel Domain  { get; set; }
        public InfraStructureModel InfraStructure { get; set; }
        
        
    }
}