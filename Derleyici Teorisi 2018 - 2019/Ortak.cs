using System;
using System.Collections.Generic;
using System.Text;

namespace DT
{
    public enum Tokenlar
    {
        tanimsiz = 0,
        hazirKullan = 1,
        Fonksiyon = 2,
        Eger = 3,
        YokODegilDe = 4,
        HicOlmadi = 5,
        Dongu = 6,
        Tekrarla = 7,
        Dondur = 8,
        TamSayiKalibi = 9,
        StringKalibi = 10,
        Tanim = 11,
        Bosluk = 12,
        YeniSatir = 13,
        Topla = 14,
        Cikar = 15,
        Carp = 16,
        Bol = 17,
        Esittir = 18,
        CiftEsit = 19,
        EsitDegil = 20,
        SolParantez = 21,
        SagParantez = 22,
        SolSuslu = 23,
        SagSuslu = 24,
        Virgul = 25,
        Nokta = 26,
        EOF = 27
    }
    class Token
    {
        public Tokenlar _tokenTuru { get; set; }
        public string _tokenDegeri { get; set; }

        public Token(Tokenlar tokenTuru, string tokenDegeri)
        {
            _tokenTuru = tokenTuru;
            _tokenDegeri = tokenDegeri;
        }
    }
    class TokenListesi
    {
        public List<Token> _tokenListesi = new List<Token>();
        public int _sayac;

        public TokenListesi(List<Token> tokenListesi)
        {
            _tokenListesi = tokenListesi;
        }
        public Token tokenAl()
        {
            return _tokenListesi[_sayac++];
        }
        public Token gozat()
        {
            return _tokenListesi[_sayac];
        }
    }

}
