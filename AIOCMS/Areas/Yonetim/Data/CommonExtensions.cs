using AIOCMS.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AIOCMS.Areas.Yonetim.Data
{
    public static class CommonExtensions
    {
        public static int ToStringBitInt(this string yetkiSValue)
        {
            int yetkiVal = 0;
            //1010101
            //1*2^6+0*2^5+...1*2^0
            for (int i = 0; i < yetkiSValue.Length; i++)
            {
                yetkiVal += (int)Math.Pow(2, i) * (int)(yetkiSValue[(yetkiSValue.Length - 1) - i]-'0');
            }
            return yetkiVal;
        }
        public static string ToIntStringBit(this int intValue)
        {
            string yetki = "";
            //1010101
            //0110100
            //1*2^6+0*2^5+...1*2^0
            // int sonHane = intValue % 2 == 1 ? 1 : 0;
            //   intValue = intValue - sonHane;
            while (intValue > 0)
            {
                if (intValue % 2 == 0)
                {
                    yetki = "0" + yetki;
                }
                else
                {
                    yetki = "1" + yetki;
                }

                intValue = (int)Math.Floor(intValue / 2.0);
            }

            //typeof(enmYetkiler).GetProperties().Count();
            int len = 0;
            int ToplamYetki = (int)enmYetkiler.ButunYetkiler + 1;
            while (ToplamYetki > 1)
            {
                len++;
                ToplamYetki = ToplamYetki / 2;
            }
            int yetkiLen = yetki.Length;
            for (int i = len; i > yetkiLen; i--)
            {
                yetki = "0" + yetki;
            }
            return yetki;
        }
        public static Dictionary<string, object> Status(this Dictionary<string, object> dic, enmStatus stats)
        {
            dic["status"]=stats.ToString();
            return dic;
        }
        public static Dictionary<string, object> Reload(this Dictionary<string, object> dic, bool reload = true)
        {
            dic["reload"] = reload?true:false;
            return dic;
        }
        public static Dictionary<string, object> Message(this Dictionary<string, object> dic, string mesaj)
        {
            dic["message"] = mesaj;
            return dic;
        }   
        public static Dictionary<string, object> Href(this Dictionary<string, object> dic, string href)
        {
            dic["href"] = MyHelper.YonetimUrl(href);
            return dic;
        }
        public static Dictionary<string, object> CustomAdd(this Dictionary<string, object> dic, string key,string val)
        {
            dic[key] = val;
            return dic;
        }
    }
}