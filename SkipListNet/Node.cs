using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipListNet
{
    public class Node<T>:IComparable<Node<T>> where T :struct,IComparable 
    {
          T value; // value of the node
          IList<Node<T>> next = new List<Node<T>>(); // list of nodes this nodes points to
          int zeroLevelIndex=0; // index at zero level          
          /// <summary>
          /// Initializes a new instance of the <see cref="Node{T}"/> class.
          /// </summary>
          /// <param name="value">The value.</param>
          public Node(T value)
          {
               this.value = value;
          }
          /// <summary>
          /// Gets or sets the index at the zero level.
          /// </summary>
          /// <value>
          /// The index at the zero level.
          /// </value>
          public int ZeroLevelIndex {
               get { return zeroLevelIndex; }
               set { zeroLevelIndex = value; }
          }
          /// <summary>
          /// Gets the value.
          /// </summary>
          /// <value>
          /// The value.
          /// </value>
          public T Value { get {return value; }  }
          /// <summary>
          /// Gets the list of nodes the node points to.
          /// </summary>
          /// <value>
          /// The list of nodes adjacent to this node.
          /// </value>
          public IList<Node<T>> Next { get { return next; } }
          /// <summary>
          /// Inserts a node referecne to the list of adjacent nodes.
          /// </summary>
          /// <param name="index">The index where to insert.</param>
          /// <param name="node">The node.</param>
          public void InsertNext(int index,Node<T> node)
          {
               if (index > next.Count-1) // check is the index is indexable in the next list, if not make space for it
               {
                    for (int i = next.Count; i <= index; i++)
                    {
                         next.Add(null);
                    }
               }
               next[index] = node;
          }
          /// <summary>
          /// Compares to another node.
          /// </summary>
          /// <param name="other">The other node.</param>
          /// <returns></returns>
          public int CompareTo(Node<T> other)
          {
               if (other.Equals(null)) return 1;
               return value.CompareTo(other.value);
          }
          /// <summary>
          /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
          /// </summary>
          /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
          /// <returns>
          ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
          /// </returns>
          public override bool Equals(object obj)
          {
               return base.Equals(obj);
          }
          /// <summary>
          /// Returns a hash code for this instance.
          /// </summary>
          /// <returns>
          /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
          /// </returns>
          public override int GetHashCode()
          {
               return base.GetHashCode();
          }
          /// <summary>
          /// Converts to string.
          /// </summary>
          /// <returns>
          /// A <see cref="System.String" /> that represents this instance.
          /// </returns>
          public override string ToString()
          {
               return "Node: " +value;
          }
     }
}
