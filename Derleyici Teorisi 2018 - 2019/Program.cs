using System;
using System.Collections.Generic;
using System.IO;

namespace DT
{
    class Program
    {
        static void Main(string[] args)
        {
            string kod = File.ReadAllText(args[0]);
            Lexer l = new Lexer(kod);
            List<Token> tokenListesi = new List<Token>();
            while (true)
            {
                Token t = l.tokenAl();
                if (t == null) break;
                if (t._tokenTuru != Tokenlar.Bosluk && t._tokenTuru != Tokenlar.YeniSatir && t._tokenTuru != Tokenlar.tanimsiz)
                    tokenListesi.Add(t);
            }
            tokenListesi.Add(new Token(Tokenlar.EOF, "EOF"));
            TokenListesi tl = new TokenListesi(tokenListesi);

            Parser parser = new Parser(tl);
            List<Durum> agac = parser.agac;

            Compiler compiler = new Compiler(agac);
            string compilerKodu = compiler.kod;

            File.WriteAllText(Path.Combine(Path.GetDirectoryName(args[0]),Path.GetFileNameWithoutExtension(args[0])+".ara") , compilerKodu);
            List<string> hkl = parser.hazirKullanListesi;
            foreach (string x in hkl)
                File.AppendAllText(Path.Combine(
                    Path.GetDirectoryName(args[0]), 
                    Path.GetFileNameWithoutExtension(args[0]) + ".ara"), 
                    Environment.NewLine + File.ReadAllText(x + ".txt"));
        }
    }
}
