namespace Scaffolddd.Core.Models
{
    public abstract class BaseModel
    {
        public string NameSpace { get; set; }
        public string PathRoot { get; set; }

        public string GetPath(string path)
        {
             return string.Concat(PathRoot,path);
        }
    }
}