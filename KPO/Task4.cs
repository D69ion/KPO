using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPO
{
    class Task4
    {
        private static Random random;
        private const int m = 6, N = 200, R = 100;
        private List<Tuple<int, bool>> tree;
        private double alpha;
        private List<Tuple<double, int>> alphas;

        public Task4()
        {
            random = new Random();
            tree = new List<Tuple<int, bool>>();
            alpha = 0.0;
            alphas = new List<Tuple<double, int>>();
        }

        public void Start()
        {
            treeGeneration:
            tree.Add(null);
            tree.Add(new Tuple<int, bool>(0, false));
            try
            {
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

                            //подсчет альфа на каждой итерации генерации дерева
                            int freeNodes = 0;
                            for (int k = 1; k < tree.Count; k++)
                            {
                                if (tree[k].Item2)
                                {
                                    freeNodes++;
                                }
                            }
                            alpha = (double)tree.Count / (double)freeNodes;
                            alphas.Add(new Tuple<double, int>(alpha, tree.Count - 1));

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
            }
            catch (ArgumentOutOfRangeException)
            {
                tree.Clear();
                goto treeGeneration;
            }

            //for (int i = 1; i < tree.Count; i++)
            //{
            //    Console.WriteLine(i + " - " + tree[i].Item1);
            //}

            Console.WriteLine("alphas:");
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine(alphas[i].Item1 + " " + alphas[i].Item2);
            }
        }
    }
}
