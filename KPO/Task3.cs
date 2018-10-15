using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPO
{
    class Task3
    {
        private static Random random;
        private const int m = 6, N = 200, R = 100;
        private List<Tuple<int, bool>> tree;
        private double alphaDispersion;
        private List<double> alphas;
        private List<int> freeNodesQuantities;

        public Task3()
        {
            random = new Random();
            tree = new List<Tuple<int, bool>>();
            alphaDispersion = 0.0;
            alphas = new List<double>();
            freeNodesQuantities = new List<int>();
        }

        public void Start()
        {
            for (int r = 0; r < R; r++)
            {
                treeGeneration:
                tree.Add(null);
                tree.Add(new Tuple<int, bool>(0, false));
                for (int i = 1; ; i++)
                {
                    int childQuantity = random.Next(0, 6);
                    if (childQuantity == 0)
                        continue;
                    for (int j = 0; j < childQuantity; j++)
                    {
                        if (tree.Count <= N)
                        {
                            tree.Add(new Tuple<int, bool>(i, true));
                            tree[i] = new Tuple<int, bool>(tree[i].Item1, false);
                        }
                        else
                            break;
                        if (tree[i].Item1 == i)
                        {
                            tree.Clear();
                            goto treeGeneration;
                        }
                    }
                    if (tree.Count >= N)
                        break;
                }

                //for (int i = 1; i < tree.Count; i++)
                //{
                //    Console.WriteLine(i + " - " + tree[i].Item1);
                //}

                int freeNodesQuantity = 0;
                //Console.WriteLine("Free nodes");
                for (int i = 1; i < tree.Count; i++)
                {
                    if (tree[i].Item2)
                    {
                        //Console.WriteLine(i + " - " + tree[i].Item1);
                        freeNodesQuantity++;
                    }
                }
                alphas.Add((double)tree.Count / (double)freeNodesQuantity);
                freeNodesQuantities.Add(freeNodesQuantity);
            }

            Console.WriteLine("Average free nodes = " + freeNodesQuantities.Average());
            Console.WriteLine("Average alpha = " + alphas.Average());
            
        }

    }
}
