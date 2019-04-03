using System;

namespace Fitter.BL.Model
{
    public class TeamDetailModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public UserListModel Admin { get; set; } 
    }
}