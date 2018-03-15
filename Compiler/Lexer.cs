using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Compiler
{
    class Lexer
    {
        Dictionary<Tokenlar, string> tokenTanimlari;
        Dictionary<Tokenlar, MatchCollection> tokenSozlugu;
        int sayac = 0;
        public string kaynakKodu { get; set; }
        public Lexer(string kaynakKodu)
        {
            this.kaynakKodu = kaynakKodu;
            tokenTanimlariDoldur();
            tokenSozluguDoldur();
        }

        private void tokenSozluguDoldur()
        {
            tokenSozlugu = new Dictionary<Tokenlar, MatchCollection>();
            foreach (KeyValuePair<Tokenlar, string> tokenTanimi in tokenTanimlari)
                tokenSozlugu.Add(tokenTanimi.Key, Regex.Matches(kaynakKodu, tokenTanimi.Value));
        }

        private void tokenTanimlariDoldur()
        {
            tokenTanimlari = new Dictionary<Tokenlar, string>();
            tokenTanimlari.Add(Tokenlar.Eklenti, "Eklenti");
            tokenTanimlari.Add(Tokenlar.Fonk, "Fonk");
            tokenTanimlari.Add(Tokenlar.ParantezAc, "\\(");
            tokenTanimlari.Add(Tokenlar.ParantezKapat, "\\)");
            tokenTanimlari.Add(Tokenlar.SusluAc, "\\{");
            tokenTanimlari.Add(Tokenlar.SusluKapat, "\\}");
            tokenTanimlari.Add(Tokenlar.Esittir, "\\=");
            tokenTanimlari.Add(Tokenlar.OndalikliSayi, "[0-9]+\\.[0-9]+");
            tokenTanimlari.Add(Tokenlar.TamSayi, "[0-9]+");
            tokenTanimlari.Add(Tokenlar.Metin, "\".*\"");
            tokenTanimlari.Add(Tokenlar.Arti, "\\+");
            tokenTanimlari.Add(Tokenlar.Eksi, "\\-");
            tokenTanimlari.Add(Tokenlar.Carpi, "\\*");
            tokenTanimlari.Add(Tokenlar.Bolu, "\\/");
            tokenTanimlari.Add(Tokenlar.EgerSonu, "EgerSonu");
            tokenTanimlari.Add(Tokenlar.Eger, "Eger");
            tokenTanimlari.Add(Tokenlar.YaDa, "YaDa");
            tokenTanimlari.Add(Tokenlar.Degilse, "Degilse");
            tokenTanimlari.Add(Tokenlar.Kucuktur, "\\<");
            tokenTanimlari.Add(Tokenlar.Buyuktur, "\\>");
            tokenTanimlari.Add(Tokenlar.Virgul, "\\,");
            tokenTanimlari.Add(Tokenlar.Bosluk, "[ \t]+");
            tokenTanimlari.Add(Tokenlar.YeniSatir, "(\n|\r\n)+");
            tokenTanimlari.Add(Tokenlar.Tanim, "[a-zA-Z]+[a-z-A-Z0-9]*");
        }
        private Token TokenAl()
        {
            foreach (var ikili in tokenSozlugu)
            {
                foreach (Match m in ikili.Value)
                {
                    if (sayac == m.Index)
                    {
                        sayac += m.Length;
                        return new Token(ikili.Key, m.Value);
                    }
                }
            }
            return new Token(Tokenlar.Tanimsiz, "");
        }
        internal TokenListesi TokenListesiAl()
        {
            List<Token> tokenListesi = new List<Token>();
            Token t = null;
            do
            {
                t = TokenAl();
                tokenListesi.Add(t);
            } while (t.tur != Tokenlar.Tanimsiz);
            return new TokenListesi(tokenListesi);
        }
    }
}
