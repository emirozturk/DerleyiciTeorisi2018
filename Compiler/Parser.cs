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
            throw new NotImplementedException();
        }

        private YaDaBlogu yaDaParseEt()
        {
            throw new NotImplementedException();
        }

        private EgerBlogu egerParseEt()
        {
            throw new NotImplementedException();
        }

        private Cagirma cagirmaParseEt()
        {
            throw new NotImplementedException();
        }

        private Atama atamaParseEt()
        {
            throw new NotImplementedException();
        }

        private Fonksiyon fonksiyonParseEt()
        {
            throw new NotImplementedException();
        }

        private void eklentiParseEt()
        {
            throw new NotImplementedException();
        }
    }
}
