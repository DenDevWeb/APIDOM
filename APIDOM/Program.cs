using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace APIDOM
{
    class Program
    {
        static string GetResponseStr(string url)
        {
            HttpWebRequest proxy_request = (HttpWebRequest)WebRequest.Create(url);
            proxy_request.Method = "GET";
            proxy_request.ContentType = "application/x-www-form-urlencoded";
            proxy_request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";
            proxy_request.KeepAlive = true;
            //proxy_request.Proxy
            HttpWebResponse resp = proxy_request.GetResponse() as HttpWebResponse;
            string html = "";
            using (StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                html = sr.ReadToEnd();
            html = html.Trim();
            return html;
        }

        /// <summary>
        /// Вырезать из str подстроку от from до to
        /// </summary>
        /// <param name="str"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        static string CutOutSubstring(string str, string from, string to)
        {
            int indx_from = str.IndexOf(from);
            int indx_first = indx_from + from.Length;
            var length = str.IndexOf(to) - indx_first;
            if (indx_from != -1 && length > 0)
            {
                return str.Substring(indx_first, length);
            }
            else
            {
                return "";
            }
        }
        static string CutOutSubstring2(string str, string from, string to)
        {
            int indx_from = str.IndexOf(from);
            int indx_first = indx_from;// + from.Length - 1;
            int indx2 = str.IndexOf(to);
            var length = indx2 - indx_first;
            if (indx_from != -1 && length > 0)
            {
                return str.Substring(indx_first, length);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Вырезать из str подстроку от from до конца строки
        /// </summary>
        /// <param name="str"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        static string CutOutSubstringLast(string str, string from)
        {
            int indx_from = str.IndexOf(from);
            if (indx_from != -1)
            {
                int indx_first = indx_from + from.Length;
                var length = str.Length - indx_first;
                return str.Substring(indx_first, length);
            }
            else
            {
                return "";
            }
        }
        static int GetPriceApartment(string html)
        {
            //string from = ;
            //string to = ;
            string period = CutOutSubstring(html, "₽</span></span>&nbsp", " \n </span>\n </div>\n </div> </div> <div class=\"sticky-header-right\"");
            if (period == "в месяц" || period == " месяц")
            {
                int price;
                //string tt = CutOutSubstring(html, "itemprop=\"price\">", "</span>&nbsp;<span itemprop=\"priceCurrency");
                //bool tmp = int.TryParse(tt, out price);
                if (int.TryParse(CutOutSubstring(html, "itemprop=\"price\">", "</span>&nbsp;<span itemprop=\"priceCurrency").Replace(" ",""), out price))
                    return price;
                else return -1;
            }
            else return -1;


            //int indx_from = html.IndexOf(from);
            //int indx_first = indx_from + from.Length;
            //var length = html.IndexOf(to) - indx_first;
            //if (indx_from != -1 && length > 0)
            //{
            //    string period = html.Substring(indx_first, length);
            //    if (period != "за сутки")
            //    {
            //        from = "itemprop=\"price\">";
            //        to = "</span>&nbsp;<span itemprop=\"priceCurrency";
            //        (int.TryParse())
            //    }
            //    else
            //    {
            //        return -1;
            //    }
            //    //return html.Substring(indx_first, length);
            //}
            //else
            //{
            //    return -1;
            //}
        }
        static void Main(string[] args)
        {
            //ApartmentLib.Apartment2 apartment2 = new ApartmentLib.Apartment2(0, 0, "Test2", 0, 0, 0, 0, 0);
            //bool oi = apartment2.CheckApartment();
            //apartment2.insertApartment();
            ApartmentLib.Apartment2 apartment = new ApartmentLib.Apartment2();
            for (int i = 1; i < 46; i++)
            {

                //Thread.Sleep(5000);
                Console.WriteLine(i.ToString() + ":");
                string html = GetResponseStr("https://www.avito.ru/belgorodskaya_oblast/kvartiry/sdam-ASgBAgICAUSSA8gQ?p=" + i.ToString());
                string[] list_ads = Regex.Split(html, "<!-- new snippet -->");
                for (int j = 1; j < list_ads.Length - 1; j++)
                {
                    Thread.Sleep(90000);
                    string url_add = list_ads[j].Contains("serp-vips-item") ?
                                     "https://www.avito.ru/" + CutOutSubstring(list_ads[j], "photo-wrapper js-item-link js-photo-wrapper large-picture\"\n href=\"", "\"\n target=") :
                                     "https://www.avito.ru/" + CutOutSubstring(list_ads[j], "js-item-slider item-slider\" href=\"", "\" target=");
                    //url_add = "https://www.avito.ru/" + CutOutSubstring(list_ads[j], "js-item-slider item-slider\" href=\"", "\" target=");
                    if (url_add != "https://www.avito.ru/")
                    {
                        string html_add = GetResponseStr(url_add);

                        apartment.Price = GetPriceApartment(html_add);
                        if (apartment.Price != -1)
                        // вырезаем часть с характеристиками квартиры
                        {
                            int indx_param = html_add.IndexOf("item-params-list");
                            if (indx_param != -1)
                            {
                                html_add = html_add.Remove(0, indx_param);
                                indx_param = html_add.IndexOf("</ul>");
                                html_add = html_add.Remove(indx_param, html_add.Length - indx_param);
                                string[] list_add = Regex.Split(html_add, "</li>");



                                foreach (string ad in list_add)
                                {
                                    var el = CutOutSubstring(ad, "class=\"item-params-label\">", "</span>");
                                    switch (el)
                                    {
                                        case "Этаж: ":
                                            int.TryParse(CutOutSubstringLast(ad, el + "</span>"), out apartment.Florr);
                                            break;
                                        case "Этажей в доме: ":
                                            int.TryParse(CutOutSubstringLast(ad, el + "</span>"), out apartment.Num_Floors);
                                            break;
                                        case "Тип дома: ":
                                            apartment.TypeHome = CutOutSubstringLast(ad, el + "</span>");
                                            break;
                                        case "Количество комнат: ":
                                            int.TryParse(CutOutSubstring(ad, el + "</span>", "-комнатные"), out apartment.Num_Rooms);
                                            break;
                                        case "Общая площадь: ":
                                            int.TryParse(CutOutSubstring(ad, el + "</span>", "&nbsp;"), out apartment.Total_Area);
                                            break;
                                        case "Жилая площадь: ":
                                            int.TryParse(CutOutSubstring(ad, el + "</span>", "&nbsp;"), out apartment.LivingArea);
                                            break;
                                        case "Площадь кухни: ":
                                            int.TryParse(CutOutSubstring(ad, el + "</span>", "&nbsp;"), out apartment.KitchenArea);
                                            break;
                                        case "Год постройки: ":
                                            int.TryParse(CutOutSubstringLast(ad, el + "</span>"), out apartment.Year);
                                            break;

                                    }

                                }



                                if (!apartment.CheckApartment())
                                {
                                    apartment.insertApartment();
                                    Console.WriteLine("   " + apartment.Price.ToString());
                                }
                                //Console.WriteLine("   " + apartment.Price.ToString());
                            }
                        }
                    }
                    apartment.Clear();
                }
            }
            //Console.WriteLine(html);
            Console.ReadKey();
        }
    }
}
