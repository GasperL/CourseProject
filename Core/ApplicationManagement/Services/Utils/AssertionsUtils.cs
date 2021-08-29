using System;

namespace Core.ApplicationManagement.Services.Utils
{
    public static class AssertionsUtils
    {
        public static void AssertIsNotNull<T>(T type, string message)
        {
            if (type == null)
            {
                throw new NullReferenceException(message);
            }
        }
    }
}