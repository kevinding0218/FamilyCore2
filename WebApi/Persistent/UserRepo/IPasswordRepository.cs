using System;

namespace WebApi.Persistent.User
{
    public interface IPasswordRepository
    {
        String Encrypt(string password);
        String CreatePassword(int maxSize = 8);
    }
}
