using System;

namespace Fitter.BL.Model
{
    public class PostDetailModel : BaseModel
    {
        public UserDetailModel Author { get; set; }
        public DateTime Created { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public TeamDetailModel Team { get; set; }
    }
}