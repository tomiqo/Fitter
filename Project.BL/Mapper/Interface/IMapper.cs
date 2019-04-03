using Fitter.BL.Model;
using Fitter.DAL.Entity;

namespace Fitter.BL.Mapper.Interface
{
    public interface IMapper
    {
        User MapUserToEntity(UserDetailModel model);
        UserDetailModel MapUserDetailModelFromEntity(User user);
        UserListModel MapUserListModelFromEntity(User user);
        
    }
}