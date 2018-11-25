using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuickGraph;

namespace Test
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private IBidirectionalGraph<object, IEdge<object>> _graphToVisualize;

        public IBidirectionalGraph<object, IEdge<object>> GraphToVisualize
        {
            get { return this._graphToVisualize; }
            set
            {
                if (!Equals(value, this._graphToVisualize))
                {
                    this._graphToVisualize = value;
                    this.RaisePropChanged("GraphToVisualize");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropChanged(string name)
        {
            var eh = this.PropertyChanged;
            if (eh != null)
            {
                eh(this, new PropertyChangedEventArgs(name));
            }
        }

        private static Random random;
        private const int m = 6, N = 200, R = 100;
        private List<Tuple<int, bool>> tree;
        private int freeNodesQuantity;
        private double alpha;

        public MainWindow()
        {
            random = new Random();
            tree = new List<Tuple<int, bool>>();
            freeNodesQuantity = 0;
            alpha = 0.0;
            InitializeComponent();
        }

        private void Task1Button_Click(object sender, RoutedEventArgs e)
        {
            tree.Clear();
            freeNodesQuantity = 0;
            alpha = 0.0;
            TxtOut.Text = "";
            GraphGeneration(random.Next(0, m));
            CreateGraphToVisualize();
            PrintTree();
        }

        private void Task2Button_Click(object sender, RoutedEventArgs e)
        {
            tree = new List<Tuple<int, bool>>();
            freeNodesQuantity = 0;
            alpha = 0.0;
            TxtOut.Text = "";
            GraphGeneration(3);
            CreateGraphToVisualize();
            PrintTree();
        }

        private void Task3Button_Click(object sender, RoutedEventArgs e)
        {
            List<double> alphas = new List<double>();
            List<int> freeNodesQuantities = new List<int>();
            for (int i = 0; i < R; i++)
            {
                tree = new List<Tuple<int, bool>>();
                freeNodesQuantity = 0;
                alpha = 0.0;
                TxtOut.Text = "";
                GraphGeneration(random.Next(0, m));
                alphas.Add(alpha);
                freeNodesQuantities.Add(freeNodesQuantity);
            }

            TxtOut.Text += "Average free nodes" + alphas.Average() + Environment.NewLine;
            TxtOut.Text += "Average alphas" + alphas.Average() + Environment.NewLine;

            double a = 0.0;
            double alphaDispersion = 0.0;
            for (int i = 0; i < alphas.Count; i++)
                a += Math.Pow(alphas[i], 2);
            a /= alphas.Count;
            alphaDispersion = a - Math.Pow(alphas.Sum() / alphas.Count(), 2);
            TxtOut.Text += "Alpha dispersion = " + alphaDispersion;
        }

        private void Task4Button_Click(object sender, RoutedEventArgs e)
        {
            tree = new List<Tuple<int, bool>>();
            freeNodesQuantity = 0;
            alpha = 0.0;
            TxtOut.Text = "";
            List<double> alphas = GraphGenerationTask4(random.Next(0, m));
            CreateGraphToVisualize();
            PrintTree();
            TxtOut.Text += "Alphas:" + Environment.NewLine;
            for(int i = 0; i < alphas.Count; i++)
            {
                TxtOut.Text += alphas[i] + " " + (i + 1) + Environment.NewLine;
            }
        }

        private void CreateGraphToVisualize()
        {
            var g = new BidirectionalGraph<object, IEdge<object>>();

            string[] vertices = new string[tree.Count];
            for (int i = 0; i < tree.Count; i++)
            {
                vertices[i] = i.ToString();
                g.AddVertex(vertices[i]);
            }

            for(int i = 2; i < tree.Count; i++)
            {
                g.AddEdge(new Edge<object>(vertices[tree[i].Item1], vertices[i]));
            }

            GraphToVisualize = g;
        }

        public void GraphGeneration(int childQuantity)
        {
            treeGeneration:
            tree.Add(null);
            tree.Add(new Tuple<int, bool>(0, false));
            try
            {
                for (int i = 1; ; i++)
                {
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

            for (int i = 1; i < tree.Count; i++)
            {
                if (tree[i].Item2)
                {
                    freeNodesQuantity++;
                }
            }
            alpha = (double)tree.Count / (double)freeNodesQuantity;
        }

        private List<double> GraphGenerationTask4(int childQuantity)
        {
            List<double> alphas;
            treeGeneration:
            alphas = new List<double>();
            tree.Add(null);
            tree.Add(new Tuple<int, bool>(0, false));
            try
            {
                for (int i = 1; ; i++)
                {
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
                            alpha = (double)tree.Count / (double)freeNodes;
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
            return alphas;
        }

        private void PrintTree()
        {
            for (int i = 1; i < tree.Count; i++)
            {
                TxtOut.Text += i + " - " + tree[i].Item1 + Environment.NewLine;
            }

            TxtOut.Text += "Free nodes" + Environment.NewLine;
            for (int i = 1; i < tree.Count; i++)
            {
                if (tree[i].Item2)
                {
                    TxtOut.Text += i + " - " + tree[i].Item1 + Environment.NewLine;
                }
            }
            TxtOut.Text += "alpha = " + alpha + Environment.NewLine;
        }
    }
}
