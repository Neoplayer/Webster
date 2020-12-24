namespace Webster
{
    public static class RequestDispatcher
    {



        public static string CreateUrl(string url)
        {
            if (url.StartsWith("http"))
            {
                return url;
            }
            else
            {
                return $"https://www.google.com/search?q={url}";
            }
        }
    }
}