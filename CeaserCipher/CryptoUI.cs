using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeaserCipher
{
    class CryptoUI
    {
        public static String getKey()
        {
            String cKey;
            Console.WriteLine("Please input Key: ");
            cKey = Console.ReadLine();

            Console.WriteLine(cKey);

            return cKey;
        }


    }
}
