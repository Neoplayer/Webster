using System;
using System.Net.Mail;

namespace Webster
{
    public class TabInfo
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public Uri ImageIri { get; set; }
        public DateTime VisitTime { get; set; }

        public TabInfo(string url, string title, Uri imageIri, DateTime visitTime)
        {
            Url = url;
            Title = title;
            ImageIri = imageIri;
            VisitTime = visitTime;
        }
    }
}