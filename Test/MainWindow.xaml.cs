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

        private void PrintResult()
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
            TxtOut.Text += "alpha = " + alpha;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GraphGeneration(random.Next(0, m));
            CreateGraphToVisualize();
            PrintResult();
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
                    //int childQuantity = random.Next(0, 6);
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
    }
}
