using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Parser
    {
        private TokenListesi tl;
        Blok suAnkiBlok = null;
        Stack<Blok> blokStack = new Stack<Blok>();
        List<Durum> agac = new List<Durum>();
        List<string> eklentiler = new List<string>();
        public Parser(TokenListesi tl)
        {
            this.tl = tl;
            parseEt();
        }

        private void parseEt()
        {
            while (true)
            {
                Token t = tl.TokenAl();
                if (t == null) break;
                if (t.tur == Tokenlar.Eklenti)
                {
                    eklentiParseEt();
                }
                else if (t.tur == Tokenlar.Fonk)
                {
                    suAnkiBlok = fonksiyonParseEt();
                }
                else if (t.tur == Tokenlar.Tanim)
                {
                    if (tl.Gozat().tur == Tokenlar.Esittir)
                    {
                        tl.GeriGit();
                        Atama a = atamaParseEt();
                        suAnkiBlok.durumListesi.Add(a);
                    }
                    else if (tl.Gozat().tur == Tokenlar.ParantezAc)
                    {
                        tl.GeriGit();
                        Cagirma c = cagirmaParseEt();
                        suAnkiBlok.durumListesi.Add(c);
                    }
                }
                else if (t.tur == Tokenlar.Eger)
                {
                    blokStack.Push(suAnkiBlok);
                    suAnkiBlok = egerParseEt();
                }
                else if (t.tur == Tokenlar.YaDa)
                {
                    blokStack.Push(suAnkiBlok);
                    suAnkiBlok = yaDaParseEt();

                }
                else if (t.tur == Tokenlar.Degilse)
                {
                    blokStack.Push(suAnkiBlok);
                    suAnkiBlok = degilseParseEt();
                }
                else if (t.tur == Tokenlar.EgerSonu)
                {
                    suAnkiBlok.durumListesi.Add(new EgerSonu());
                    Blok b = suAnkiBlok;
                    suAnkiBlok = blokStack.Pop();
                    suAnkiBlok.durumListesi.Add(b);
                }
                else if (t.tur == Tokenlar.SusluKapat)
                {
                    agac.Add(suAnkiBlok);
                    suAnkiBlok = null;
                }
            }
        }

        private DegilseBlogu degilseParseEt()
        {
            return new DegilseBlogu();
        }

        private YaDaBlogu yaDaParseEt()
        {
            tl.IleriGit();
            Ifade solIfade = ifadeParseEt();
            Tokenlar islem = tl.TokenAl().tur;
            Ifade sagIfade = ifadeParseEt();
            tl.IleriGit();
            return new YaDaBlogu(solIfade, islem, sagIfade);
        }

        private EgerBlogu egerParseEt()
        {
            tl.IleriGit();
            Ifade solIfade = ifadeParseEt();
            Tokenlar islem = tl.TokenAl().tur;
            Ifade sagIfade = ifadeParseEt();
            tl.IleriGit();
            return new EgerBlogu(solIfade, islem, sagIfade);
        }

        private Cagirma cagirmaParseEt()
        {
            string ad = tl.TokenAl().deger;
            tl.IleriGit();
            List<Ifade> argumanlar = new List<Ifade>();
            if (tl.Gozat().tur == Tokenlar.ParantezKapat)
                argumanlar = argumanParseEt();
            return new Cagirma(ad, argumanlar);
        }

        private List<Ifade> argumanParseEt()
        {
            List<Ifade> argumanlar = new List<Ifade>();
            while (true)
            {
                argumanlar.Add(ifadeParseEt());
                if (tl.Gozat().tur == Tokenlar.Virgul)
                    tl.IleriGit();
                else if (tl.Gozat().tur == Tokenlar.ParantezKapat)
                {
                    tl.IleriGit();
                    break;
                }
            }
            return argumanlar;
        }

        private Atama atamaParseEt()
        {
            string ad = tl.TokenAl().deger;
            tl.IleriGit();
            Ifade ifade = ifadeParseEt();
            return new Atama(ad, ifade);
        }

        private Ifade ifadeParseEt()
        {
            Ifade donus = null;
            Token t = tl.TokenAl();
            if (t.tur == Tokenlar.TamSayi)
                donus = new Tamsayi(Convert.ToInt32(t.deger));
            else if (t.tur == Tokenlar.OndalikliSayi)
                donus = new OndalikliSayi(Convert.ToDouble(t.deger));
            else if (t.tur == Tokenlar.Metin)
                donus = new Metin(t.deger);
            else if (t.tur == Tokenlar.Tanim)
                donus = new Tanim(t.deger);
            else if (t.tur == Tokenlar.ParantezAc)
            {
                Ifade i = ifadeParseEt();
                tl.IleriGit();
                donus = new ParantezIfadesi(i);
            }
            if (tl.Gozat().tur == Tokenlar.Arti ||
                tl.Gozat().tur == Tokenlar.Eksi ||
                tl.Gozat().tur == Tokenlar.Carpi ||
                tl.Gozat().tur == Tokenlar.Bolu)
            {
                Ifade solIfade = donus;
                Tokenlar islem = tl.TokenAl().tur;
                Ifade sagIfade = ifadeParseEt();
                donus = new MatematikIfadesi(solIfade, islem, sagIfade);
            }
            return donus;
        }

        private Fonksiyon fonksiyonParseEt()
        {
            string ad = tl.TokenAl().deger;
            tl.IleriGit();
            List<string> parametreler = new List<string>();
            if (tl.Gozat().tur == Tokenlar.ParantezKapat)
                tl.IleriGit();
            else
                parametreler = parametreParseEt();
            return new Fonksiyon(ad, parametreler);
        }

        private List<string> parametreParseEt()
        {
            List<string> parametreler = new List<string>();
            while (true)
            {
                Token t = tl.TokenAl();
                if (t.tur == Tokenlar.Tanim)
                    parametreler.Add(t.deger);
                if (tl.Gozat().tur == Tokenlar.Virgul)
                    tl.IleriGit();
                else if (tl.Gozat().tur == Tokenlar.ParantezKapat)
                {
                    tl.IleriGit();
                    break;
                }
            }
            return parametreler;
        }

        private void eklentiParseEt()
        {
            eklentiler.Add(tl.TokenAl().deger);
        }
    }
}
