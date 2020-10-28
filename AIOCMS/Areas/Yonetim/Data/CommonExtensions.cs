using System;

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
                yetkiVal += (int)Math.Pow(2, i) * (int)yetkiSValue[(yetkiSValue.Length - 1) - i];
            }
            return yetkiVal;
        }
        public static string ToIntStringBit(this int intValue)
        {
            string yetki = "";
            //1010101
            //0110100
            //1*2^6+0*2^5+...1*2^0
            int sonHane = intValue % 2 == 1 ? 1 : 0;
            intValue = intValue - sonHane;
            while (intValue > 1)
            {
                if(intValue % 2 == 0)
                {
                    yetki += "1";
                }
                else
                {
                    yetki += "0";
                    intValue--;
                }
                    
                intValue = intValue / 2;
            }
            yetki += sonHane;
            //typeof(enmYetkiler).GetProperties().Count();
            int len = 0;
            int ToplamYetki = (int)enmYetkiler.ButunYetkiler + 1;            
            while (ToplamYetki>1)
            {
                len++;
                ToplamYetki = ToplamYetki / 2;
            }
            for (int i = (int)enmYetkiler.ButunYetkiler; i > yetki.Length; i--)
            {
                yetki = "0" + yetki;
            } 
            return yetki;
        }
    }
}