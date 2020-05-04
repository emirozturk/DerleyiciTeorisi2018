using System;
using System.Collections.Generic;
using System.Text;

namespace DT
{
    class Compiler
    {
        public string kod = "";
        public Compiler(List<Durum> agac)
        {
            derle(agac);
        }

        private void derle(List<Durum> agac)
        {
            foreach (Durum s in agac)
            {
                if (s is Fonksiyon)
                    fonksiyonDerle((Fonksiyon)s);
                else if (s is Atama)
                    atamaDerle((Atama)s);
                else if (s is Cagirma)
                    cagirmaDerle((Cagirma)s);
                else if (s is Dondurme)
                {
                    Dondurme d = (Dondurme)s;
                    if (d.ifade != null)
                        ifadeDerle(d.ifade);
                    Yaz("don");
                }
            }
        }

        private void ifadeDerle(Ifade e)
        {
            if (e is TamsayiKalibi)
                Yaz("PushTamsayi " + ((TamsayiKalibi)e).deger);
            else if (e is StringKalibi)
                Yaz("PushString " + ((StringKalibi)e).deger);
            else if (e is Tanim)
                Yaz("PushTanim " + ((Tanim)e).deger);
            else if (e is CagirmaIfadesi)
            {
                CagirmaIfadesi ci = (CagirmaIfadesi)e;
                foreach (Ifade exp in ci.argumanlar)
                    ifadeDerle(exp);
                Yaz("Cagir " + ci.ad);
            }
            else if (e is MatematikIfadesi)
            {
                MatematikIfadesi mi = (MatematikIfadesi)e;
                ifadeDerle(mi.solIfade);
                ifadeDerle(mi.sagIfade);
                Yaz(mi.islem.ToString());
            }
            else if (e is ParantezIfadesi)
            {
                ParantezIfadesi pi = (ParantezIfadesi)e;
                ifadeDerle(pi.deger);
            }
        }

        private void cagirmaDerle(Cagirma c)
        {
            c.argumanlar.Reverse();
            foreach (Ifade e in c.argumanlar)
                ifadeDerle(e);
            Yaz("Cagir " + c.ad);
        }

        private void atamaDerle(Atama a)
        {
            ifadeDerle(a.deger);
            Yaz("DegiskenAta " + a.ad);
        }

        private void fonksiyonDerle(Fonksiyon f)
        {
            Yaz("$" + f.ad);
            foreach (string p in f.parametreler)
                Yaz("DegiskenAta " + p);
            derle(f.durumListesi);
        }

        private void Yaz(string yazilacak)
        {
            if (kod == "")
                kod = yazilacak;
            else
                kod += Environment.NewLine + yazilacak;
        }
    }
}
