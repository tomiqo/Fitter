using System;

namespace Fitter.BL.Model
{
    public class PostListModel : BaseModel
    {
        public string Title { get; set; }
        public UserListModel Author { get; set; }
        public DateTime Created { get; set; }
    }
}