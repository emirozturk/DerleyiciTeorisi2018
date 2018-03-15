using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler
{
    class Program
    {
        static void Main(string[] args)
        {
            string kaynakKodu = File.ReadAllText("kaynak.txt", Encoding.Default);
            Lexer l = new Lexer(kaynakKodu);
            TokenListesi tl = l.TokenListesiAl();
            Parser p = new Parser(tl);
        }
    }
}
