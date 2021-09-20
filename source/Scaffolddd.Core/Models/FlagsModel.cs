namespace Scaffolddd.Core.Models
{
    public class FlagsModel
    {
        public bool GenerateIUnitOfWork { get; set; }
        public bool GenerateUnitOfWork { get; set; }
        public bool GenerateIBaseRepository { get; set; }
        public bool GenerateBaseRepository { get; set; }
        public bool GenerateEntities { get; set; }
        public bool GenerateRepositoryInterfaces { get; set; }
        public bool GenerateRepositories { get; set; }
        public bool GenerateIBaseService { get; set; }
        public bool GenerateBaseService { get; set; }
        public bool GenerateDTOs { get; set; }
        public bool GenerateMappings { get; set; }
        public bool GenerateDependencyInjection { get; set; }
        public bool GenerateIBaseValidation { get; set; }
        public bool GenerateBaseValidation { get; set; }
        public bool GenerateValidaions { get; set; }

    }
}