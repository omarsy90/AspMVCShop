namespace Marketing.Controllers
{
    public interface ISavingFileHandler
    {

        public  bool  Save(IFormFile file, out string path);
    }
}
