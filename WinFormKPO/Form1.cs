using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace WinFormKPO
{
    public partial class MainForm : Form
    {
        private static Random random;
        private const int m = 6, N = 200, R = 100;
        private List<Tuple<int, bool>> tree;
        private int freeNodesQuantity;
        private double alpha;
        private double expectedValue;
        private int level;

        public MainForm()
        {
            random = new Random();
            tree = new List<Tuple<int, bool>>();
            freeNodesQuantity = 0;
            alpha = 0.0;
            expectedValue = 0.0;
            level = 1;
            InitializeComponent();
        }

        private void buttonTask1_Click(object sender, EventArgs e)
        {
            tree.Clear();
            freeNodesQuantity = 0;
            alpha = 0.0;
            level = 1;
            textBox.Text = "";
            GraphGeneration(1);
            CreateBarChart();
            PrintTreeNodes(tree.Count);
        }

        private void buttonTask2_Click(object sender, EventArgs e)
        {
            tree.Clear();
            freeNodesQuantity = 0;
            alpha = 0.0;
            level = 1;
            textBox.Text = "";
            GraphGeneration(0);
            CreateBarChart();
            PrintTreeNodes(tree.Count);
        }

        private void buttonTask3_Click(object sender, EventArgs e)
        {
            textBox.Text = "";
            List<double> alphas = new List<double>();
            List<int> freeNodesQuantities = new List<int>();
            List<int> levels = new List<int>();
            List<int> nodeQuantities = new List<int>();
            for (int i = 0; i < R; i++)
            {
                tree = new List<Tuple<int, bool>>();
                freeNodesQuantity = 0;
                alpha = 0.0;
                level = 1;
                GraphGeneration(1);
                alphas.Add(alpha);
                freeNodesQuantities.Add(freeNodesQuantity);
                levels.Add(level);
                nodeQuantities.Add(tree.Count - 1);
                //textBox.Text += "№" + i + Environment.NewLine;
                //PrintTreeNodes(tree.Count);
            }

            textBox.Text += "Average node quantities = " + nodeQuantities.Average() + Environment.NewLine;
            textBox.Text += "Average free nodes = " + freeNodesQuantities.Average() + Environment.NewLine;
            textBox.Text += "Average alpha = " + alphas.Average() + Environment.NewLine;
            textBox.Text += "Average level = " + levels.Average() + Environment.NewLine;

            double a = 0.0;
            double alphaDispersion = 0.0;
            for (int i = 0; i < alphas.Count; i++)
                a += Math.Pow(alphas[i], 2);
            a /= alphas.Count;
            alphaDispersion = a - Math.Pow(alphas.Sum() / alphas.Count(), 2);
            textBox.Text += "Alpha dispersion = " + alphaDispersion;
            
        }

        private void buttonTask4_Click(object sender, EventArgs e)
        {
            tree = new List<Tuple<int, bool>>();
            freeNodesQuantity = 0;
            alpha = 0.0;
            level = 1;
            textBox.Text = "";
            List<double> alphas = GraphGenerationTask4(1);
            CreateFunctionGraph(alphas);
            PrintTreeNodes(21);
            textBox.Text += "Alphas:" + Environment.NewLine;
            for (int i = 1; i < alphas.Count; i++)
            {
                textBox.Text += i + ": " + alphas[i] + Environment.NewLine;
            }

            CreateDotFile();
            System.Diagnostics.Process graphviz = new System.Diagnostics.Process();
            graphviz.StartInfo.FileName = @"D:\Projects C#\KPO\WinFormKPO\graphviz\dot.exe";
            graphviz.StartInfo.RedirectStandardOutput = true;
            graphviz.StartInfo.UseShellExecute = false;
            graphviz.StartInfo.CreateNoWindow = true;
            graphviz.StartInfo.Arguments = string.Format("{0} -Tjpg -0", @"D:\Projects C#\KPO\WinFormKPO\graphviz\graph.dot");
            graphviz.Start();
            graphviz.WaitForExit();
        }

        private void CreateDotFile()
        {
            StringBuilder dotString = new StringBuilder();
            dotString.Append("graph test{" + Environment.NewLine + "node [shape = box]" + Environment.NewLine);
            for(int i = 2; i < 21; i++)
            {
                dotString.Append(string.Format("\"{0} - {1}\" -- \"{1}\"", i, tree[i].Item1)).
                    Append(Environment.NewLine);
            }
            dotString.Append("}");
            File.WriteAllText(@"D:\Projects C#\KPO\WinFormKPO\graphviz\graph.dot", dotString.ToString());
        }

        private void CreateBarChart()
        {
            GraphPane pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();

            List<double> XValues = new List<double>();
            List<double> YValues = new List<double>();
            int lastNode = tree[tree.Count - 1].Item1;
            int count = 1;
            int quantity = 0;
            for (int i = 2; i < tree.Count; i++)
            {
                if(tree[i].Item1 == count)
                {
                    quantity++;
                }
                else
                {
                    XValues.Add((double)count);
                    YValues.Add((double)quantity);
                    count++;
                    quantity = 0;
                    i--;
                }
            }

            BarItem bar = pane.AddBar("Node quantities", XValues.ToArray(), YValues.ToArray(), Color.Blue);
            pane.BarSettings.MinClusterGap = 2.5f;

            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        public void CreateFunctionGraph(List<double> alphas)
        {
            GraphPane pane = zedGraphControl.GraphPane;
            pane.CurveList.Clear();
            PointPairList points = new PointPairList();
            for(int i = 1; i <= tree.Count - 1; i++)
            {
                points.Add(i, alphas[i]);
            }
            LineItem line = pane.AddCurve("line", points, Color.Blue, SymbolType.None);
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }

        private void GraphGeneration(int flag)
        {
            int childQuantity = 3;
            treeGeneration:
            tree.Add(null);
            tree.Add(new Tuple<int, bool>(0, false));
            try
            {
                for (int i = 1; ; i++)
                {
                    if (flag == 1)
                        childQuantity = random.Next(-1, m);
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

            int lastNode = tree[tree.Count - 1].Item1;
            while (tree[lastNode].Item1 != 1)
            {
                lastNode = tree[lastNode].Item1;
                level++;
            }
            level += 2;

            for (int i = 1; i < tree.Count; i++)
            {
                if (tree[i].Item2)
                {
                    freeNodesQuantity++;
                }
            }
            alpha = (double)(tree.Count - 1) / (double)freeNodesQuantity;
            expectedValue = (double)(tree.Count - 1) / (double)tree.Count;
        }

        private List<double> GraphGenerationTask4(int flag)
        {
            List<double> alphas;
            int childQuantity = 3;
            treeGeneration:
            alphas = new List<double>();
            alphas.Add(0);
            alphas.Add(1);
            tree.Add(null);
            tree.Add(new Tuple<int, bool>(0, false));
            try
            {
                for (int i = 1; ; i++)
                {
                    if (flag == 1)
                        childQuantity = random.Next(-1, m);
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
                                    Console.WriteLine(i + " - " + tree[k].Item1);
                                    freeNodes++;
                                }
                            }
                            alpha = (double)(tree.Count - 1) / (double)freeNodes;
                            alphas.Add(alpha);
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

            int lastNode = tree[tree.Count - 1].Item1;
            while (tree[lastNode].Item1 != 1)
            {
                lastNode = tree[lastNode].Item1;
                level++;
            }
            level += 2;

            for (int i = 1; i < tree.Count; i++)
            {
                if (tree[i].Item2)
                {
                    freeNodesQuantity++;
                }
            }
            alpha = (double)(tree.Count - 1) / (double)freeNodesQuantity;
            expectedValue = (double)(tree.Count - 1) / (double)tree.Count;

            return alphas;
        }

        private void PrintTreeNodes(int count)
        {
            for (int i = 1; i < count; i++)
            {
                textBox.Text += i + " - " + tree[i].Item1 + Environment.NewLine;
            }

            textBox.Text += "Free nodes" + Environment.NewLine;
            for (int i = 1; i < count; i++)
            {
                if (tree[i].Item2)
                {
                    textBox.Text += i + " - " + tree[i].Item1 + Environment.NewLine;
                }
            }
            textBox.Text += "alpha = " + alpha + Environment.NewLine;
            textBox.Text += "expected value = " + expectedValue + Environment.NewLine;
            textBox.Text += "level: " + level + Environment.NewLine;
            textBox.Text += Environment.NewLine;
        }
    }
}
