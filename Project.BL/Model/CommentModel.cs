using System;

namespace Fitter.BL.Model
{
    public class CommentModel
    {
        public UserListModel Author { get; set; }
        public DateTime Created { get; set; }
        public string Text { get; set; }
        //public TagModel Tags { get; set; }        iCollection ??? TagListModel ???
    }
}