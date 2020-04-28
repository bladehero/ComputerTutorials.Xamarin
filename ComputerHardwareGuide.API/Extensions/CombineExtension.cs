using System.Linq;

namespace ComputerHardwareGuide.API.Extensions
{
    public class CombineExtension
    {
        public static string UrlCombine(string url1, string url2)
        {
            if (url1.Length == 0)
            {
                return url2;
            }

            if (url2.Length == 0)
            {
                return url1;
            }

            url1 = url1.TrimEnd('/', '\\');
            url2 = url2.TrimStart('/', '\\');

            return string.Format("{0}/{1}", url1, url2);
        }
        public static string UrlCombine(params string[] urls)
        {
            if (urls.Length == 0)
            {
                return string.Empty;
            }
            else if (urls.Length == 1)
            {
                return urls[0].TrimEnd('/', '\\');
            }
            else if (urls.Length == 2)
            {
                UrlCombine(urls[0], urls[1]);
            }
            return UrlCombine(urls[0], UrlCombine(urls.Skip(1).ToArray()));
        }
    }
}
