using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DT
{
    class Lexer
    {
        private Dictionary<Tokenlar, string> _tokenSozlugu;
        private Dictionary<Tokenlar, MatchCollection> _regexSozlugu;
        private string _kaynakKodu;
        private int _sayac;

        public Lexer(string kaynakKodu)
        {
            _kaynakKodu = kaynakKodu;
            _tokenSozlugu = new Dictionary<Tokenlar, string>();
            _regexSozlugu = new Dictionary<Tokenlar, MatchCollection>();
            _sayac = 0;
            _tokenSozlugu.Add(Tokenlar.hazirKullan, "HazirKullan");
            _tokenSozlugu.Add(Tokenlar.Fonksiyon, "Fonksiyon");
            _tokenSozlugu.Add(Tokenlar.Eger, "Eger");
            _tokenSozlugu.Add(Tokenlar.YokODegilDe, "YokODegilDe");
            _tokenSozlugu.Add(Tokenlar.HicOlmadi, "HicOlmadi");
            _tokenSozlugu.Add(Tokenlar.Tekrarla, "Tekrarla");
            _tokenSozlugu.Add(Tokenlar.Dondur, "Dondur");
            _tokenSozlugu.Add(Tokenlar.StringKalibi, "\".*?\"");
            _tokenSozlugu.Add(Tokenlar.TamSayiKalibi, "[0-9][0-9]*");
            _tokenSozlugu.Add(Tokenlar.Tanim, "[a-zA-Z_][a-zA-Z0-9_]*");
            _tokenSozlugu.Add(Tokenlar.Bosluk, "[ \\t]+");
            _tokenSozlugu.Add(Tokenlar.YeniSatir, "\\n");
            _tokenSozlugu.Add(Tokenlar.Topla, "\\+");
            _tokenSozlugu.Add(Tokenlar.Cikar, "\\-");
            _tokenSozlugu.Add(Tokenlar.Carp, "\\*");
            _tokenSozlugu.Add(Tokenlar.Bol, "\\/");
            _tokenSozlugu.Add(Tokenlar.CiftEsit, "\\==");
            _tokenSozlugu.Add(Tokenlar.EsitDegil, "\\!=");
            _tokenSozlugu.Add(Tokenlar.Esittir, "\\=");
            _tokenSozlugu.Add(Tokenlar.SolParantez, "\\(");
            _tokenSozlugu.Add(Tokenlar.SagParantez, "\\)");
            _tokenSozlugu.Add(Tokenlar.SolSuslu, "\\{");
            _tokenSozlugu.Add(Tokenlar.SagSuslu, "\\}");
            _tokenSozlugu.Add(Tokenlar.Virgul, "\\,");
            _tokenSozlugu.Add(Tokenlar.Nokta, "\\.");

            foreach (var x in _tokenSozlugu)
            {
                _regexSozlugu.Add(x.Key, Regex.Matches(_kaynakKodu, x.Value));
            }
        }
        public Token tokenAl()
        {
            if (_sayac >= _kaynakKodu.Length)
                return null;
            else
            {
                foreach (KeyValuePair<Tokenlar, MatchCollection> ikili in _regexSozlugu)
                {
                    foreach (Match eslesme in ikili.Value)
                    {
                        if (_sayac == eslesme.Index)
                        {
                            _sayac += eslesme.Length;
                            return new Token(ikili.Key, eslesme.Value);
                        }
                    }
                }
            }
            _sayac++;
            return new Token(Tokenlar.tanimsiz, "");
        }
    }
}
