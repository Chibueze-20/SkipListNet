using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipListNet
{
     public interface IUpdater
     {
           void Update(int level, int nodeValue);
     }
}
