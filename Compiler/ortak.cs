using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    public enum Tokenlar
    {
        Eklenti = 0,
        Tanim = 1,
        Fonk = 2,
        ParantezAc = 3,
        ParantezKapat = 4,
        SusluAc = 5,
        SusluKapat = 6,
        Esittir = 7,
        TamSayi = 8,
        OndalikliSayi = 9,
        Metin = 10,
        Arti = 11,
        Carpi = 12,
        Eksi = 13,
        Bolu = 14,
        Eger = 15,
        Kucuktur = 16,
        Virgul = 17,
        YaDa = 18,
        Degilse = 19,
        EgerSonu = 20,
        Buyuktur = 21,
        Bosluk = 22,
        YeniSatir = 23,
        Tanimsiz = 24
    }
    public class Token
    {
        public Tokenlar tur { get; set; }
        public string deger { get; set; }
        public Token(Tokenlar tur, string deger)
        {
            this.tur = tur;
            this.deger = deger;
        }
    }
    public class TokenListesi
    {
        private List<Token> tokenListesi;
        int sayac = 0;
        public TokenListesi(List<Token> tokenListesi)
        {
            this.tokenListesi = tokenListesi;
        }
        public Token TokenAl()
        {
           return tokenListesi[sayac++];
        }
        public Token Gozat()
        {
            return tokenListesi[sayac];
        }
        public void IleriGit()
        {
            sayac++;
        }
        public void GeriGit()
        {
            sayac--;
        }
    }
}
