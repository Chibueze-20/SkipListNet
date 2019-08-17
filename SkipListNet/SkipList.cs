using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipListNet
{
     public class SkipList<T> where T :struct,IComparable
     {
          Random rand = new Random(); //Random number genrator for randomized skip list
          Node<T> Head; // sentinel head node
          Node<T> Tail; // sentinel tail node
          HashSet<Node<T>> nodes = new HashSet<Node<T>>(); // keeps all nodes in the skip list just for reference sake
          int MaxLevel; // maximum levels in the allowable in the skip list
          bool randomized = false; // if the skip list is ranomized          
          /// <summary>
          /// Initializes a new instance of the <see cref="SkipList{T}"/> class.
          /// </summary>
          /// <param name="head">The head node value.</param>
          /// <param name="tail">The tail node value.</param>
          /// <param name="maxLevel">The maximum level in the skip list.</param>
          /// <param name="IsRandomized">if set to <c>true</c> skip list is randomized.</param>
          public SkipList(T head,T tail,int maxLevel,bool IsRandomized=false)
          {
               randomized = IsRandomized; //true if a random skip list
               Head = new Node<T>(head);
               Tail = new Node<T>(tail);
               MaxLevel = maxLevel;
               setUp(0, maxLevel);
          }
          /// <summary>
          /// Sets up the levels in the skip list.
          /// </summary>
          /// <param name="start">The start level.</param>
          /// <param name="stop">The stop level (not inclusive).</param>
          private void setUp(int start,int stop)
          {
               for (int i = start; i <= stop; i++)
               {
                    Head.InsertNext(i, Tail);
               }
          }
          /// <summary>
          /// Inserts the specified node into the skip list.
          /// </summary>
          /// <param name="node">The node.</param>
          /// <returns><c>true</c> if insertion was done, <c>false</c> otherwise</returns>
          public bool Insert(Node<T> node)
          {
               
                    nodes.Add(node);
                    int desired = (int)Math.Ceiling(Math.Log(nodes.Count, 2)); // get the expected number of levels given n nodes in the nodes list
                    if (desired > MaxLevel)
                    {
                         setUp(MaxLevel+1, desired);
                         MaxLevel = desired;
                    }
                    if (randomized)
                    {
                         int levl = GenerateLevel();
                         bool x = Insert(node, Head, MaxLevel, levl);
                         return x;
                    }
                    else
                    {
                        bool x = Insert(node, Head, 0);
                         Fix(); // fix skip list after insertion for perfect skip list
                         return x;
                    }
               
          }
          /// <summary>
          /// Inserts the specified value into the skip list.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <returns><c>true</c> if insertion was done, <c>false</c> otherwise</returns>
          public bool Insert(T value)
          {
               Node<T> node = new Node<T>(value);
               return Insert(node);
          }
          /// <summary>
          /// Inserts the specified node into a perfect skip list.
          /// </summary>
          /// <param name="node">The node.</param>
          /// <param name="root">The root node to start from.</param>
          /// <param name="level">The level where the node is inserted.</param>
          private bool Insert(Node<T> node,Node<T> root,int level)
          {
               while (root.Next[level].CompareTo(node)<0)
               {
                    root = root.Next[level];
               }
               if (root.Next[level].CompareTo(node) > 0)
               {
                    if (level == 0)
                    {
                         Node<T> old = root.Next[level];
                         //update zero level index, a thing i just use to know who to promote during a fix
                         root.InsertNext(level, node); node.ZeroLevelIndex = root.ZeroLevelIndex + 1;
                         node.InsertNext(level, old); old.ZeroLevelIndex = node.ZeroLevelIndex + 1;
                         return true;
                    }
                    else
                    {
                         Node<T> old = root.Next[level];
                         root.InsertNext(level, node);
                         node.InsertNext(level, old);
                         return true;
                    }
               }
               return false;
          }
          /// <summary>
          /// Inserts the specified node into the randomized skip list.
          /// </summary>
          /// <param name="node">The node.</param>
          /// <param name="root">The root node to start from.</param>
          /// <param name="maxHeight">The maximum height(level) in the skip list.</param>
          /// <param name="level">The max level the node will be inserted to.</param>
          private bool Insert(Node<T> node, Node<T> root, int maxHeight,int level)
          {
               if (level>maxHeight)
               {
                    level = maxHeight; // if level > max height make level same as max height
               }
               for (int i = level; i >=0; i--)
               {
                    while (root.Next[i].CompareTo(node)<0) // if root next is less than node.. move right
                    {
                         root = root.Next[i];
                    }
                    if (root.Next[i].CompareTo(node)>0)
                    {

                         Node<T> old = root.Next[i];
                         root.InsertNext(i, node);
                         node.InsertNext(i, old);
                    }
                    else
                    {
                         return false;
                    }
               }
               return true;
          }
          /// <summary>
          /// Fixes the the perfect skip list
          /// </summary>
          private void Fix()
          {
               //reset list
               Node<T> cuurnode = Head;
               while (cuurnode.Next[0] !=Tail)
               {
                    cuurnode.Next[0].ZeroLevelIndex = cuurnode.ZeroLevelIndex + 1;
                    cuurnode = cuurnode.Next[0];
               }
               for (int i = 1; i <= MaxLevel; i++)
               {
                    Head.Next[i] = Tail;
               }
               //clear old levels
               foreach (Node<T> item in nodes)
               {
                    if (item.Next.Count>1)
                    {
                         for (int i = 1; i < item.Next.Count; i++)
                         {
                              item.Next[i] = null;
                         }
                    }
               }
               //done resetting list
               foreach (Node<T> item in nodes)
               {
                    int levl = 0;
                    int i = 1;int pow = (int)Math.Pow(2, i);
                    //checking if level promotion is possible at current index in list level 0
                    while (item.ZeroLevelIndex % pow == 1)
                    {
                         levl = levl + 1;
                         if (levl<=MaxLevel)
                         {
                              Insert(item, Head, levl);
                              i = i + 1;
                              pow = (int)Math.Pow(2, i);
                         }
                         else
                         {
                              break;
                         }

                    }
               }
          }
          /// <summary>
          /// Generates the level of a node randomly.
          /// </summary>
          /// <returns>the level of the node</returns>
          private int GenerateLevel()
          {
               int n = 0;
               
               while (rand.Next(2) != 1)
               {
                    n++;
               }
               return n;
          }
          /// <summary>
          /// Searches for the specified node in the skip list.
          /// </summary>
          /// <param name="node">The node.</param>
          /// <returns><c>true</c> if node is found, <c>false</c> otherwise</returns>
          public bool Search(Node<T> node)
          {
               if (nodes.Count>0)
               {
                    return Search(node, Head, MaxLevel);
               }
               else
               {
                    return false;
               }
             
          }
          /// <summary>
          /// Searches for the specified value in the skip list.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <returns><c>true</c> if value is found, <c>false</c> otherwise</returns>
          public bool Search(T value)
          {
               Node<T> node = new Node<T>(value);
               return Search(node);
          }
          /// <summary>
          /// Searches for the specified node in the skip list.
          /// </summary>
          /// <param name="node">The node.</param>
          /// <param name="start">The start node.</param>
          /// <param name="level">The level.</param>
          /// <returns><c>true</c> if the node is found, <c>false</c> otherwise</returns>
          private bool Search(Node<T> node,Node<T> start,int level)
          {
               for (int i = level ; i >= 0; i--)
               {
                    if (node.CompareTo(start) == 0 && i == 0) //Node is equal to start and it is at the last level
                    {
                         return true;
                    }
                    int compare = node.CompareTo(start.Next[i]);
                    if (compare < 0) // if node < start next go down a level
                    {
                       continue;
                    }
                    if (compare >= 0) // if node is >= start next move right
                    {
                         start = start.Next[i];
                         i = i + 1;
                    }
               }
               return false;
          }
          /// <summary>
          /// Deletes the specified node from the skip list.
          /// </summary>
          /// <param name="node">The node.</param>
          /// <returns><c>true</c> if node was deleted, <c>false</c> otherwise</returns>
          public bool Delete(Node<T> node) {
               //Check if node exist and remove it
               if (nodes.Count>0)
               {
                    Delete(node, Head);
                    int x = nodes.RemoveWhere(elem => (elem.Value.CompareTo(node.Value)) == 0); // remove from set of nodes
                    if (!randomized && x > 0) // if deleted andis perfect fix the skip list
                    {
                         Fix();
                    }
                    return x > 0;
               }
               else
               {
                    return false;
               }
          }
          /// <summary>
          /// Deletes the specified value from the skip list.
          /// </summary>
          /// <param name="value">The value.</param>
          /// <returns><c>true</c> if value was deleted, <c>false</c> otherwise</returns>
          public bool Delete(T value)
          {
               Node<T> node = new Node<T>(value);
               return Delete(node);
          }
          /// <summary>
          /// Deletes the specified node from the skip list.
          /// </summary>
          /// <param name="node">The node.</param>
          /// <param name="start">The start node.</param>
          private void Delete(Node<T> node, Node<T> start)
          {
               int level = MaxLevel;
               for (int i = level; i >= 0; i--)
               {
                    int compare = node.CompareTo(start.Next[i]);
                    if (compare == 0) // node == next node from start
                    {
                         Node<T> old = start.Next[i];
                         start.InsertNext(i, old.Next[i]);     
                    }
                    else if (compare < 0) // node < next node from start
                    {
                         continue;
                    }
                    else if (compare > 0)// node > next node from start
                    {
                         start = start.Next[i];
                         i = i + 1;
                    }
               }
          }
          /// <summary>
          /// Prints this instance as a skip list.
          /// </summary>
          public void Print()
          {
               for (int i = MaxLevel; i >= 0; i--)
               {
                    Node<T> currNode = Head;
                    if (currNode.Next[i]==Tail)
                    {
                         continue;
                    }
                    Console.Write("Head" + " --> ");
                    while (currNode.Next[i] != Tail)
                    {
                         currNode = currNode.Next[i];
                         Console.Write(currNode + " --> ");
                    }
                    Console.WriteLine("Tail");
               }
          }
          public IList<List<Node<T>>> SkipLists()
          {
               List<List<Node<T>>> nodes = new List<List<Node<T>>>(MaxLevel);
               for (int i = 0; i <= MaxLevel; i++)
               {
                    nodes.Add(new List<Node<T>>());
               }
               for (int i = 0; i <= MaxLevel; i++)
               {
                    Node<T> currNode = Head;
                    if (currNode.Next[i] == Tail)
                    {
                         continue;
                    }
                    nodes[i].Add(Head);
                    while (currNode.Next[i]!=Tail)
                    {
                         currNode = currNode.Next[i];
                         nodes[i].Add(currNode);
                    }
                    nodes[i].Add(Tail);
               }
               return nodes;
          }
     }
}
