namespace Amoeba.Utilities.Extensions;

public static class FileExtension
{
    public static bool CheckType(this IFormFile file,string type)
    {
        return file.ContentType.Contains(type);
    }
    public static bool CheckSize(this IFormFile file,int size)
    {
        return file.Length / 1024 > size;
    }
    public static async Task<string> SaveFileAsync(this IFormFile file,string root,string mainpath)
    {
        string UniqueFileName=Guid.NewGuid().ToString()+"_"+file.FileName;
        string path=Path.Combine(root,"assets","img",UniqueFileName);
        FileStream stream=new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);
        return UniqueFileName;
    }
}
