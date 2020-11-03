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
    }
}