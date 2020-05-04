using System;
using System.Collections.Generic;
using System.Text;

namespace DT
{
    class Parser
    {
        public List<string> hazirKullanListesi = new List<string>();
        TokenListesi tokenListesi;
        Blok suAnkiBlok;
        Stack<Blok> blockStack = new Stack<Blok>();
        public List<Durum> agac = new List<Durum>();

        Token t = null;
        bool calisiyorMu = true;

        public Parser(TokenListesi tl)
        {
            tokenListesi = tl;
            parseEt();
        }

        private void parseEt()
        {
            while (calisiyorMu)
            {
                t = tokenListesi.tokenAl();

                if (t._tokenTuru == Tokenlar.hazirKullan)
                {
                    string kutuphane = hazirKullanParseEt();
                    hazirKullanListesi.Add(kutuphane);
                }
                else if (t._tokenTuru == Tokenlar.Fonksiyon)
                {
                    Fonksiyon f = fonksiyonParseEt();
                    if (suAnkiBlok == null)
                        suAnkiBlok = f;
                    else
                    {
                        suAnkiBlok.durumListesi.Add(new Dondurme());
                        agac.Add(suAnkiBlok);
                        suAnkiBlok = f;
                    }
                }
                else if (t._tokenTuru == Tokenlar.Tanim)
                {
                    if (tokenListesi.gozat()._tokenTuru == Tokenlar.Esittir)
                    {
                        Atama a = atamaParseEt();
                        suAnkiBlok.durumListesi.Add(a);
                    }
                    else if (tokenListesi.gozat()._tokenTuru == Tokenlar.SolParantez)
                    {
                        Cagirma c = cagirmaParseEt();
                        suAnkiBlok.durumListesi.Add(c);
                    }
                }
                else if (t._tokenTuru == Tokenlar.Dondur)
                {
                    Dondurme d = dondurmeParseEt();
                    suAnkiBlok.durumListesi.Add(d);
                }
                else if (t._tokenTuru == Tokenlar.SagParantez)
                {
                    if (suAnkiBlok is Fonksiyon)
                    {
                        suAnkiBlok.durumListesi.Add(new Dondurme());
                        agac.Add(suAnkiBlok);
                        suAnkiBlok = null;
                    }
                }
                else if (t._tokenTuru == Tokenlar.EOF)
                {
                    agac.Add(suAnkiBlok);
                    calisiyorMu = false;
                }
            }
        }

        private Dondurme dondurmeParseEt()
        {
            Dondurme d = new Dondurme();
            d.ifade = expressionParseEt();
            return d;
        }

        private Atama atamaParseEt()
        {
            Atama a = new Atama();
            tokenListesi._sayac--;
            a.ad = tokenListesi.tokenAl()._tokenDegeri;
            tokenListesi._sayac++;
            a.deger = expressionParseEt();
            return a;
        }

        private Fonksiyon fonksiyonParseEt()
        {
            Fonksiyon f = new Fonksiyon();
            if (tokenListesi.gozat()._tokenTuru == Tokenlar.Tanim)
                f.ad = tokenListesi.tokenAl()._tokenDegeri;
            if (tokenListesi.gozat()._tokenTuru == Tokenlar.SolParantez)
                tokenListesi._sayac++;
            if (tokenListesi.gozat()._tokenTuru == Tokenlar.SagParantez)
                tokenListesi._sayac++;
            else
                f.parametreler = fonksiyonParametreleriniParseEt();
            if (tokenListesi.gozat()._tokenTuru == Tokenlar.SolSuslu)
                tokenListesi._sayac++;
            return f;
        }

        private Cagirma cagirmaParseEt()
        {
            Cagirma c = new Cagirma();
            tokenListesi._sayac--;
            Token t = tokenListesi.tokenAl();
            if (t._tokenTuru == Tokenlar.Tanim)
                c.ad = t._tokenDegeri;
            if (tokenListesi.gozat()._tokenTuru == Tokenlar.SolParantez)
                tokenListesi._sayac++;
            if (tokenListesi.gozat()._tokenTuru == Tokenlar.SagParantez)
                tokenListesi._sayac++;
            else
                c.argumanlar = cagirmaArgumaniParseEt();
            return c;
        }

        private List<string> fonksiyonParametreleriniParseEt()
        {
            List<string> parametreler = new List<string>();
            while (true)
            {
                Token t = tokenListesi.tokenAl();
                if (t._tokenTuru == Tokenlar.Tanim)
                    parametreler.Add(t._tokenDegeri);
                if (tokenListesi.gozat()._tokenTuru == Tokenlar.Virgul)
                    tokenListesi._sayac++;
                if (tokenListesi.gozat()._tokenTuru == Tokenlar.SagParantez)
                {
                    tokenListesi._sayac++;
                    break;
                }
            }
            return parametreler;
        }

        private List<Ifade> cagirmaArgumaniParseEt()
        {
            List<Ifade> argumanlar = new List<Ifade>();
            while (true)
            {
                argumanlar.Add(expressionParseEt());
                if (tokenListesi.gozat()._tokenTuru == Tokenlar.Virgul)
                    tokenListesi._sayac++;
                else if (tokenListesi.gozat()._tokenTuru == Tokenlar.SagParantez)
                {
                    tokenListesi._sayac++;
                    break;
                }
            }
            return argumanlar;
        }

        private Ifade expressionParseEt()
        {
            Ifade donus = null;
            Token t = tokenListesi.tokenAl();
            if (tokenListesi.gozat()._tokenTuru == Tokenlar.SolParantez)
            {
                CagirmaIfadesi c = new CagirmaIfadesi();
                if (t._tokenTuru == Tokenlar.Tanim)
                    c.ad = t._tokenDegeri;
                tokenListesi._sayac++;
                if (tokenListesi.gozat()._tokenTuru == Tokenlar.SagParantez)
                {
                    c.argumanlar = new List<Ifade>();
                    donus = c;
                }
                else
                {
                    c.argumanlar = cagirmaArgumaniParseEt();
                    donus = c;
                }
            }
            else if (t._tokenTuru == Tokenlar.TamSayiKalibi)
            {
                TamsayiKalibi tsk = new TamsayiKalibi();
                tsk.deger = Convert.ToInt32(t._tokenDegeri);
                donus = tsk;
            }
            else if (t._tokenTuru == Tokenlar.StringKalibi)
            {
                StringKalibi sk = new StringKalibi();
                sk.deger = t._tokenDegeri;
                donus = sk;
            }
            else if (t._tokenTuru == Tokenlar.Tanim)
            {
                Tanim tanim = new Tanim();
                tanim.deger = t._tokenDegeri;
                donus = tanim;
            }
            else if (t._tokenTuru == Tokenlar.SolParantez)
            {
                Ifade ifade = expressionParseEt();

                if (tokenListesi.gozat()._tokenTuru == Tokenlar.SagParantez)
                    tokenListesi._sayac++;

                ParantezIfadesi p = new ParantezIfadesi();
                p.deger = ifade;

                if (tokenListesi.gozat()._tokenTuru == Tokenlar.Topla || tokenListesi.gozat()._tokenTuru == Tokenlar.Cikar || tokenListesi.gozat()._tokenTuru == Tokenlar.Carp || tokenListesi.gozat()._tokenTuru == Tokenlar.Bol)
                {
                    tokenListesi._sayac++;
                    Ifade e = expressionParseEt();
                    MatematikIfadesi mi = new MatematikIfadesi();
                    mi.solIfade = p;
                    mi.sagIfade = e;
                    mi.islem = tokenListesi.gozat()._tokenTuru;
                    donus = mi;
                }
                else
                    donus = p;
            }
            if (tokenListesi.gozat()._tokenTuru == Tokenlar.Topla || tokenListesi.gozat()._tokenTuru == Tokenlar.Cikar || tokenListesi.gozat()._tokenTuru == Tokenlar.Carp || tokenListesi.gozat()._tokenTuru == Tokenlar.Bol)
            {
                MatematikIfadesi mi = new MatematikIfadesi();
                mi.islem = tokenListesi.gozat()._tokenTuru;
                tokenListesi._sayac++;
                mi.solIfade = donus;
                mi.sagIfade = expressionParseEt();
                donus = mi;
            }
            return donus;
        }

        private string hazirKullanParseEt()
        {
            Token t = tokenListesi.tokenAl();
            if (t._tokenTuru == Tokenlar.Tanim)
                return t._tokenDegeri;
            return "";
        }
    }
}
