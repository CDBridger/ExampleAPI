using ApiWrapper;
using System;
using System.Collections.Generic;

namespace CallingClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("C++ Vector Class ----------------->");
            var cppVector = new Vector3(1, 2, 3);
            Console.WriteLine(cppVector);
            cppVector.MakeUnitVector();
            Console.WriteLine(cppVector);
            var otherVector = cppVector.GetUnitVector();
            Console.WriteLine($" Copied Vector : {otherVector}");

            Console.WriteLine("C++ API Class ----------------->");

            var api = new API();
            Console.WriteLine("Inited: ");
            var collection = api.GetVectorCollection();
            foreach (var vec in collection)
            {
                Console.WriteLine(vec);
            }
            List<Vector3> sendingCollection;
            var rnd = new Random();
            for (int i = 0; i < 100000000; i++)
            {
                sendingCollection = new List<Vector3>();
                sendingCollection.Add(new Vector3(rnd.Next(10), rnd.Next(10), rnd.Next(10)));
                sendingCollection.Add(new Vector3(rnd.Next(10), rnd.Next(10), rnd.Next(10)));
                api.SendVectorCollection(sendingCollection);
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
