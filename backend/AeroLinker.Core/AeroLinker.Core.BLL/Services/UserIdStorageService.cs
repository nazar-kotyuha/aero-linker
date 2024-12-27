using AeroLinker.Core.BLL.Interfaces;
using AeroLinker.Shared.Exceptions;

namespace AeroLinker.Core.BLL.Services;

public class UserIdStorageService : IUserIdGetter, IUserIdSetter
{
    private int _userId;

    public int GetCurrentUserId()
    {
        if (_userId == 0)
        {
            throw new InvalidAccessTokenException();
        }

        return _userId;
    }

    public void SetCurrentUserId(int id)
    {
        _userId = id;
    }
}
