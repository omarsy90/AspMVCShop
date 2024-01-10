namespace Marketing.Controllers
{
    public class FileHandler : ISavingFileHandler
    {
        public bool Save(IFormFile file,out string  pathDb)
        {
            pathDb = string.Empty;
                try
                {


                    string extendedname = file.FileName;
                    string extension = extendedname.Split('.').Last();
                    extension = extension.ToLower();

                    if (extension == "png" || extension == "jpg" || extension == "jepg")
                    {
                        string directory = Directory.GetCurrentDirectory()+"/wwwroot";
                        string storage = "images/products";
                        string fullDirectory = Path.Combine(directory.ToString(), storage);

                        string randomFileName = Guid.NewGuid().ToString().Replace("-", "").Replace(".", "") + "." + extension;
                        string fullpath = Path.Combine(fullDirectory, randomFileName);

                        using (var stream = new FileStream(fullpath, FileMode.Create))
                        {
                             file.CopyToAsync(stream, new CancellationTokenSource(2000).Token).GetAwaiter().GetResult();
                        }

                         pathDb = Path.Combine(storage, randomFileName);
                        return true;
                       }
                return false;

                }
                catch (Exception ex)
                {
             
                    return false;
                }


            
            
        }
    }
}
