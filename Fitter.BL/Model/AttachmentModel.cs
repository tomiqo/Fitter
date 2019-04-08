using Fitter.DAL.Enums;

namespace Fitter.BL.Model
{
    public class AttachmentModel : BaseModel
    {
        public string Name { get; set; }
        public FileType FileType { get; set; }
        public byte[] File { get; set; }       
        public PostModel Post { get; set; }
    }
}