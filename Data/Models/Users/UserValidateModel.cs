using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Models.Users
{
    public enum  UserValidateModel
    {
        Successful = 1,
        UserNotExist = 2,
        WrongPassword = 3,
        NotActive = 4,
        Deleted = 5,
        NotRegistered = 6,
        LockedOut = 7,
    }
}
