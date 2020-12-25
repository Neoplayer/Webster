using System;

namespace Webster
{
    public static class RequestDispatcher
    {
        public static string CreateUrl(string url)
        {
            if (url == null)
            {
                return null;
            }
            
            var trimmedUrl = url.Trim();
            
            if (trimmedUrl.StartsWith("http"))
            {
                return url;
            }
            else
            {
                return $"https://www.google.com/search?q={trimmedUrl.Replace(' ', '+')}";
            }
        }
    }
}