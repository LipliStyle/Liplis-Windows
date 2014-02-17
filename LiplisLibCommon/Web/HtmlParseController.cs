using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Liplis.Control;
using System.Windows.Forms;

namespace Liplis.Web
{
    public static class HtmlParseController
    {
        public static List<string> getLinkList(string url)
        {
            List<string> result = new List<string>();

            try
            {
                NonDispBrowser ndb = new NonDispBrowser();

                //2011/10/10 スクリプトエラーをもみけす
                ndb.ScriptErrorsSuppressed = true;

                //ナビゲート
                ndb.NavigateAndWait(url);

                HtmlDocument doc = ndb.Document;

                // リンク文字列とそのURLの列挙
                foreach (HtmlElement et in doc.GetElementsByTagName("A"))
                {
                    string href = et.GetAttribute("href"); // HREF属性の値
                    string text = et.InnerText; // リンク文字列

                    if (!string.IsNullOrEmpty(href)
                        && !string.IsNullOrEmpty(text))
                    {
                        result.Add(href);
                    }
                }

                ndb.Dispose();
            }
            catch
            {
            }

            return result;
        }
    }
}
