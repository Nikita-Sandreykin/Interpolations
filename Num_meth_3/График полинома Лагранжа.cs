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
    public partial class График_полинома_Лагранжа : Form
    {
        public double a, b;
        bool Check = false;
        bool Check2 = false;
        public График_полинома_Лагранжа()
        {
            InitializeComponent();
        }
        public PointPairList Newton;
        public CurveItem NewtonCurve;
        public CurveItem BasicCurve;
        private void ZedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
        }

       
    }
}
