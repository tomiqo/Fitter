using System;

namespace Fitter.BL.Model
{
    public class CommentModel : BaseModel
    {
        public UserDetailModel Author { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }
        public PostDetailModel Post { get; set; }
    }
}