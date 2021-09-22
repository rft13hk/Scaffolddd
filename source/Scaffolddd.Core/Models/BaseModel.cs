namespace Scaffolddd.Core.Models
{
    public abstract class BaseModel
    {
        public string NameSpace { get; set; }
        public string Project { get; set; }

        public string GetPath(string path)
        {
            return string.Concat(Project,path);
        }
    }
}