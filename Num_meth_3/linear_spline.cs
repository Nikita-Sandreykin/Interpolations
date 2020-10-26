using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Num_meth_3
{
    class linear_spline
    {
        internal double[] tabX = new double[4];
        internal double[] tabY = new double[4];
        private double[] a = new double[3]; //Коэффициенты сплайнов
        private double[] b = new double[3];
        public void setTabPoints(double[] tabX, double[] tabY)
        {
            this.tabX = tabX;
            this.tabY = tabY;
        }
        public void calculateSplineCoefficients()
        {
            for(int i = 0; i < 3; i++)
            {
                a[i] = (tabY[i] - tabY[i + 1]) / (tabX[i] - tabX[i + 1]);
                b[i] = tabY[i] - a[i] * tabX[i];
            }
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
            return a[k] * x + b[k];
        }
        }
}
