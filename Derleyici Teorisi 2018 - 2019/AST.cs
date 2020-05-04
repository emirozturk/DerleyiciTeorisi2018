using System;
using System.Collections.Generic;
using System.Text;

namespace DT
{
    class Durum { }
    class Ifade { }

    class Blok : Durum
    {
        public List<Durum> durumListesi = new List<Durum>();
    }
    class Fonksiyon : Blok
    {
        public string ad;
        public List<string> parametreler = new List<string>();
    }
    class Cagirma : Durum
    {
        public string ad;
        public List<Ifade> argumanlar = new List<Ifade>();
    }
    class Atama : Durum
    {
        public string ad;
        public Ifade deger;
    }
    class Dondurme : Durum
    {
        public Ifade ifade = null;
    }
    class CagirmaIfadesi : Ifade
    {
        public string ad;
        public List<Ifade> argumanlar = new List<Ifade>();
    }
    class ParantezIfadesi : Ifade
    {
        public Ifade deger;
    }
    class MatematikIfadesi : Ifade
    {
        public Ifade solIfade;
        public Ifade sagIfade;
        public Tokenlar islem;
    }
    class TamsayiKalibi : Ifade
    {
        public int deger;
    }
    class StringKalibi : Ifade
    {
        public string deger;
    }
    class Tanim : Ifade
    {
        public string deger;
    }
}
