using ApiWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallingClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C++ Vector Class ----------------->");
            var cppVector = new Vector3(1,2,3);
            Console.WriteLine(cppVector);
            cppVector.MakeUnitVector();
            Console.WriteLine(cppVector);
            var otherVector = cppVector.GetUnitVector();
            Console.WriteLine($" Copied Vector : {otherVector}");

            Console.WriteLine("C++ API Class ----------------->");

            var api = new API();
            Console.WriteLine("Inited: ");
            var collection = api.GetVectorCollection();
            foreach(var vec in collection)
            {
                Console.WriteLine(vec);
            }
                var sendingCollection = new List<Vector3>();
                sendingCollection.Add(new Vector3(5, 5, 5));
                sendingCollection.Add(new Vector3(6, 6, 6));
                sendingCollection.Add(new Vector3(7, 7, 7));
                sendingCollection.Add(new Vector3(8, 8, 8));
                api.SendVectorCollection(sendingCollection);

            for (int i = 0; i < 10000; i++) {
                Console.WriteLine($"Retrieving Sent {i}:");
                collection = api.GetVectorCollection();
                foreach (var vec in collection)
                {
                    Console.WriteLine(vec);
                }

            }

            Console.ReadKey();
        }
    }
}
