using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Durum{}
    class Ifade{}
    class Blok : Durum
    {
        public List<Durum> durumListesi { get; set; }
        public Blok()
        {
            durumListesi = new List<Durum>();
        }
    }
    class Fonksiyon : Blok
    {
        string ad;
        List<string> parametreler;
        public Fonksiyon(string ad,List<string> parametreler)
        {
            this.ad = ad;
            this.parametreler = parametreler;
        }
    }
    class EgerBlogu: Blok
    {
        Ifade solIfade;
        Tokenlar islem;
        Ifade sagIfade;
        public EgerBlogu(Ifade solIfade,Tokenlar islem,Ifade sagIfade)
        {
            this.solIfade = solIfade;
            this.islem = islem;
            this.sagIfade = sagIfade;
        }
    }
    class YaDaBlogu : Blok
    {
        Ifade solIfade;
        Tokenlar islem;
        Ifade sagIfade;
        public YaDaBlogu(Ifade solIfade, Tokenlar islem, Ifade sagIfade)
        {
            this.solIfade = solIfade;
            this.islem = islem;
            this.sagIfade = sagIfade;
        }
    }
    class DegilseBlogu : Blok
    {
    }
    class EgerSonu : Blok
    {        
    }
    class Atama : Durum
    {
        string ad;
        Ifade deger;
        public Atama(string ad,Ifade deger)
        {
            this.ad = ad;
            this.deger = deger;
        }
    }
    class Cagirma : Durum
    {
        string ad;
        List<Ifade> parametreler;
        public Cagirma(string ad,List<Ifade> parametreler)
        {
            this.ad = ad;
            this.parametreler = parametreler;
        }
    }
    class Tamsayi:Ifade
    {
        int deger;
        public Tamsayi(int deger)
        {
            this.deger = deger;
        }
    }
    class OndalikliSayi : Ifade
    {
        double deger;
        public OndalikliSayi(double deger)
        {
            this.deger = deger;
        }
    }
    class Metin : Ifade
    {
        string deger;
        public Metin(string deger)
        {
            this.deger = deger;
        }
    }
    class MatematikIfadesi : Ifade
    {
        Ifade solIfade;
        Tokenlar islem;
        Ifade sagIfade;
        public MatematikIfadesi(Ifade solIfade,Tokenlar islem,Ifade sagIfade)
        {
            this.solIfade = solIfade;
            this.islem = islem;
            this.sagIfade = sagIfade;
        }
    }
    class ParantezIfadesi : Ifade
    {
        Ifade ifade;
        public ParantezIfadesi(Ifade ifade)
        {
            this.ifade = ifade;
        }
    }
}