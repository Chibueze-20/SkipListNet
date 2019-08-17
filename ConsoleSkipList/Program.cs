using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkipListNet;
namespace ConsoleSkipList
{
     class Program
     {
          static void Main(string[] args)
          {
               SkipList<int> skiper = new SkipList<int>(int.MinValue, int.MaxValue, 3);
               Node<int> a = new Node<int>(5);
               Node<int> b = new Node<int>(15);
               Node<int> c = new Node<int>(1);
               Node<int> d = new Node<int>(4);
               Node<int> e = new Node<int>(3);
               Node<int> f = new Node<int>(10);
               Node<int> g = new Node<int>(13);
               Node<int> h = new Node<int>(20);
               Node<int> i = new Node<int>(24);
               Node<int> j = new Node<int>(8);
               //Console.WriteLine(a == b);
               skiper.Insert(a);
               skiper.Insert(a);
               skiper.Insert(b);
               skiper.Insert(c);
               skiper.Insert(d);
               skiper.Insert(e);
               skiper.Insert(f);
               skiper.Insert(g);
               skiper.Insert(h);
               skiper.Insert(i);
               skiper.Insert(j);
               Console.WriteLine(skiper.Search(e));
               skiper.Print();
               //skiper.Delete(f);
               Console.WriteLine(skiper.Delete(new Node<int>(100)));
               skiper.Print();
               Console.Read();
          }
     }
}
