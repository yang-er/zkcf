using System;
using System.IO;
using System.Net;
using System.Text;

namespace zkcf.Parallel
{
    class TestInfo
    {
        public string Name;
        string Source;
        public string Url;
        public string Pattern;
        public bool NeedZw;
        public WebClient wc;

        internal static TestInfo Create(string source, string name, string url, string pattern, bool NeedZw)
        {
            TestInfo ti = new TestInfo();
            ti.Source = source;
            ti.Name = name;
            ti.Pattern = pattern;
            ti.Url = url;
            ti.NeedZw = NeedZw;
            return ti;
        }

        public bool Download2(long a, long b = 0)
        {
            wc.DownloadFile(this.Url + "?" + String.Format(this.Pattern, a, b), Program.CommonFolder + "\\" + this.Source + "\\" + a + ".log");/*
            respHtml = respHtml.Replace("\r", "");
            respHtml = respHtml.Replace("\n", "");
            respHtml = respHtml.Replace(" ", "");
            if (respHtml == "") return false;
            File.WriteAllText(Program.CommonFolder + "\\" + this.Source + "\\" + a + ".log", respHtml);*/
            return true;
        }
        public bool Download(long a, long b = 0)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.Url);
            request.Method = "POST";
            request.Accept = "*/*";
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.SetCookies(new Uri(this.Url), " _gscu_464756295=53546322gvp3jm12");
            request.CookieContainer.SetCookies(new Uri(this.Url), " pgv_pvi=389944320");
            request.ContentType = "application/x-www-form-urlencoded";
            string post = String.Format(this.Pattern, a, b);
            byte[] buffer = Encoding.UTF8.GetBytes(post);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            try
            {
            
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string respHtml = reader.ReadToEnd();
                reader.Close();
                respHtml = respHtml.Replace("\r", "");
                respHtml = respHtml.Replace("\n", "");
                respHtml = respHtml.Replace(" ", "");
                if (respHtml == "") return false;
                File.WriteAllText(Program.CommonFolder + "\\" + this.Source + "\\" + a + ".log", respHtml);
                return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

    }
}
