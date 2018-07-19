using System;

namespace Lojinha.MVC.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsValue(this String str)
        {
            return !String.IsNullOrWhiteSpace(str) ? true
                                                   : false;
        }

        public static string GetValueOrNull(this String str)
        {
            return !String.IsNullOrWhiteSpace(str) ? str
                                                   : null;
        }
    }
}