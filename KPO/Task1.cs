using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPO
{

    public class Task1
    {
        static Random random = new Random();
        const int m = 6, N = 200, R = 100;
        List<int> tree = new List<int>();
        List<Tuple<int, int>> freeNodes = new List<Tuple<int, int>>();
        double alpha = 0.0;

        public void Start()
        {
            tree.Add(0);
            tree.Add(0);

            for (int i = 1; ; i++)
            {
                int childQuantity = random.Next(0, 6);
                if (childQuantity == 0)
                {
                    freeNodes.Add(new Tuple<int, int>(i, tree[i]));//node, parent
                    continue;
                }
                for (int j = 0; j < childQuantity; j++)
                {                    
                    if (tree.Count <= N)
                        tree.Add(i);
                    else
                        break;
                }
                if (tree.Count == N)
                    break;
            }

            for (int i = 1; i < tree.Count; i++)
            {
                Console.WriteLine(i + " - " + tree[i]);
            }

            if (freeNodes.Count == 0)
            {
                Console.WriteLine("No free nodes");
            }
            else
            {
                Console.WriteLine("Free nodes");
                for (int i = 0; i < freeNodes.Count; i++)
                {
                    Console.WriteLine(freeNodes[i].Item1 + " - " + freeNodes[i].Item2);
                }
                alpha = tree.Count / freeNodes.Count;
                Console.WriteLine("alpha = " + alpha);
            }
        }

    }
}
