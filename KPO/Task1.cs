﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPO
{

    public class Task1
    {
        private static Random random;
        private const int m = 6, N = 200, R = 100;
        private List<Tuple<int, bool>> tree;
        private double alpha;

        public Task1()
        {
            random = new Random();
            tree = new List<Tuple<int, bool>>();
            alpha = 0.0;
        }

        public void Start()
        {
            treeGeneration:
            int level = 1;
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

            //поиск глубины
            int lastNode = tree[tree.Count - 1].Item1;
            while (tree[lastNode].Item1 != 1)
            {
                lastNode = tree[lastNode].Item1;
                level++;
            }
            level += 2;

            for (int i = 1; i < tree.Count; i++)
            {
                Console.WriteLine(i + " - " + tree[i].Item1);
            }

            //Console.WriteLine("Free nodes");
            int freeNodesQuantity = 0;
            for (int i = 1; i < tree.Count; i++)
            {
                if (tree[i].Item2)
                {
                    //Console.WriteLine(i + " - " + tree[i].Item1);
                    freeNodesQuantity++;
                }
            }
            alpha = (double)tree.Count / (double)freeNodesQuantity;
            Console.WriteLine("alpha = " + alpha);
            Console.WriteLine("level: " + level);
        }
    }
}
