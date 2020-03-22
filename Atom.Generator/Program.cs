using Atom.Generator.DataCore;
using Orm.Son.Core;
using System;

namespace Atom.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SonFact.init("server=localhost;database=LLR;uid=sa;pwd=123456Aa;Max Pool Size=1000");

                Console.WriteLine("=====================");
                Console.WriteLine("Generator Start...");
                Console.WriteLine("=====================");

                GeneratorCore.Generate("UserTecherExt");

                Console.WriteLine("=====================");
                Console.WriteLine("Generator Success!");
                Console.WriteLine("=====================");
                Console.WriteLine("Press Any Key To Exit!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Generator Error:" + ex.Message);
            }
            finally
            {
                Console.ReadKey();
            }


        }
    }
}
