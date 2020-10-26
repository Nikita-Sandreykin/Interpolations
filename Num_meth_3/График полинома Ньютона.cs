using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;
namespace Num_meth_3
{
    public partial class График_полинома_Ньютона : Form
    {
        public График_полинома_Ньютона()
        {
            InitializeComponent();
        }
        public double a, b;
        bool Check = false;
        PointPairList Lagrange;
        private void Button1_Click(object sender, EventArgs e)
        {
            Check = !Check;
            if (Check)
            {
                PointPairList list = new PointPairList();
                for (double x = a; x <= b; x += 0.001)
                {
                    // добавим в список точку
                    list.Add(x, FunctionsApp.f(x));
                }
                this.zedGraphControl1.GraphPane.AddCurve("Source function", list, Color.Red);
                this.zedGraphControl1.AxisChange();

                // Обновляем график
                this.zedGraphControl1.Invalidate();

                button1.Text = "Убрать исходный график";
            }
            else
            {
                this.zedGraphControl1.GraphPane.CurveList.RemoveAt(1);
                this.zedGraphControl1.AxisChange();

                // Обновляем график
                this.zedGraphControl1.Invalidate();
                button1.Text = "Исходный график";
            }
        }
    }
}
