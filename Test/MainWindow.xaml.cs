using System;
using System.Collections.Generic;
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
    public partial class MainWindow : Window
    {
        private IBidirectionalGraph<object, IEdge<object>> _graphToVisualize;

        public IBidirectionalGraph<object, IEdge<object>> GraphToVisualize
        {
            get { return _graphToVisualize; }
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
            TxtOut = new TextBlock
            {
                Text = ""
            };
            Start();
            CreateGraphToVisualize();
            InitializeComponent();
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

            //g.AddEdge(new Edge<object>(vertices[0], vertices[1]));
            //g.AddEdge(new Edge<object>(vertices[1], vertices[2]));
            //g.AddEdge(new Edge<object>(vertices[2], vertices[3]));
            //g.AddEdge(new Edge<object>(vertices[3], vertices[1]));
            //g.AddEdge(new Edge<object>(vertices[1], vertices[4]));

            _graphToVisualize = g;
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
                TxtOut.Text += i + " - " + tree[i].Item1 + Environment.NewLine;
            }

            //Console.WriteLine("Free nodes");
            for (int i = 1; i < tree.Count; i++)
            {
                if (tree[i].Item2)
                {
                    //Console.WriteLine(i + " - " + tree[i].Item1);
                    freeNodesQuantity++;
                }
            }
            alpha = (double)tree.Count / (double)freeNodesQuantity;
            //Console.WriteLine("alpha = " + alpha);
        }
    }
}
