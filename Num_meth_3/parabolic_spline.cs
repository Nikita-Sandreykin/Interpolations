using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Num_meth_3
{
    class parabolic_spline
    {
        internal double[] tabX = new double[4];
        internal double[] tabY = new double[4];
        private double[] a = new double[3]; //Коэффициенты сплайнов
        private double[] b = new double[3];
        private double[] c = new double[3];
        public void setTabPoints(double[] tabX, double[] tabY)
        {
            this.tabX = tabX;
            this.tabY = tabY;
        }
        public void calculateSplineCoefficients()
        {
            c[0] = 0;
            c[2] = 0; ;
            b[0] = (tabY[0] - tabY[1]) / (tabX[0] - tabX[1]);
            b[2] = (tabY[2] - tabY[3]) / (tabX[2] - tabX[3]);
            c[1] = (b[0]-b[2])/(tabX[1]-tabX[2]);// (l * (tabX[1] - tabX[2]) + tabY[2] - tabY[1]) / (2 * tabX[1] * (tabX[1] - tabX[2]) - tabX[1] * tabX[1] + tabX[2] * tabX[2]);
            b[1] = (tabY[1] - tabY[2] - c[1] * (tabX[1] * tabX[1] - tabX[2] * tabX[2])) / (tabX[1] - tabX[2]);
            a[0] = tabY[1] - b[0] * tabX[1] - c[0] * tabX[1] * tabX[1];
            a[1] = tabY[2] - b[1] * tabX[2] - c[1] * tabX[2] * tabX[2];
            a[2] = tabY[3] - b[2] * tabX[3] - c[2] * tabX[3] * tabX[3];
        }
        public double getPoint(double x)
        {
            int k = 0;
            for (int i = 0; i < 3; i++)
            {
                if (x >= tabX[i] && x <= tabX[i + 1])
                {
                    k = i;
                }
            }
            return a[k] + b[k] * (x) + c[k] * (x) * (x);
        }
    }
}
