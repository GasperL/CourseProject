using System;

namespace Core.ApplicationManagement.Services.Utils
{
    public static class AssertionsUtils
    {
        public static void AssertIsNotNull(object instance, string message)
        {
            if (instance == null)
            {
                throw new ArgumentNullException (message);
            }
        }
    }
}