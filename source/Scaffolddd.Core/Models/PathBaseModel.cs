namespace Scaffolddd.Core.Models
{
    public abstract class PathBaseModel
    {
        public string Project { get; set; }

        public string GetPath(string path)
        {
            return string.Concat(Project,path);
        }
    }
}