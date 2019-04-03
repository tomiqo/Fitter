using System;

namespace Fitter.BL.Model
{
    public class PostDetailModel : BaseModel
    {
        public UserListModel Author { get; set; }
        public DateTime Created { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public AttachmentModel Attachment { get; set; }
        //public TagModel Tags { get; set; }                    iCollection ??? TagListModel ???
        //public CommentDetailModel Comments { get; set; }      iCollection ???
    }
}