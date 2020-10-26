using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Num_meth_3
{
    class cubic_spline
    {
        internal double[] tabX = new double[4];
        internal double[] tabY = new double[4];
        private double[] a = new double[3]; //Коэффициенты сплайнов
        private double[] b = new double[3];
        private double[] c = new double[3];
        private double[] cAdd = new double[4]; //В методе прогонки используется c[-1], для чего выделяется отдельный массив
        private double[] d = new double[3];
        private double[] h = new double[3]; //Первый вспомогательный коэффициент
        private double[] l = new double[3]; //Второй вспомогательный коэффициент
        private double[] q = new double[2]; //Первый прогоночный коэффициент
        private double[] v = new double[2]; //Второй прогоночный коэффициент
        public void setTabPoints(double[] tabX, double[] tabY)
        {
            this.tabX = tabX;
            this.tabY = tabY;
        }
        public void calculateAdditionalCoefficients()
        {
            for(int i = 0; i < 3; i++)
            {
                h[i] = tabX[i + 1] - tabX[i];
                l[i] = (tabY[i + 1] - tabY[i]) / h[i];
            }
            cAdd[0] = 0;
        }
        public void calculateRunningCoefficients()
        {
            q[0] = -h[1] / (2 * (h[0] + h[1]));
            v[0] = 3 * (l[1] - l[0]) / (2 * (h[0] + h[1]));
            for(int i = 2; i < 3; i++)
            {
                q[i - 1] = -h[i] / (2 * h[i - 1] + 2 * h[i] + h[i - 1] * q[i - 2]);
                v[i - 1] = (3 * l[i] - 3 * l[i - 1] - h[i - 1] * v[i - 2]) / (2 * h[i - 1] + 2 * h[i] + h[i - 1] * q[i - 2]);
            }
        }
        public void calculateSplineCoefficients()
        {
            cAdd[3] = 0;
            for(int i = 2; i > 0; i--)
            {
                cAdd[i] = q[i - 1] * cAdd[i + 1] + v[i - 1];
            }
            for(int i = 0; i < 3; i++)
            {
                a[i] = tabY[i + 1];
            }
            for(int i = 0; i < 3; i++)
            {
                c[i] = cAdd[i + 1];
            }
            for(int i = 0; i < 3; i++)
            {
                b[i] = l[i] + (2 * cAdd[i + 1] * h[i] + h[i] * cAdd[i])/3;
                d[i] = (cAdd[i + 1] - cAdd[i]) / (3 * h[i]);
            }
        }
        public double getPoint(double x)
        {
            int k = 0;
            for(int i = 0; i < 3; i++)
            {
                if(x >= tabX[i] && x <= tabX[i + 1])
                {
                    k = i;
                }
            }
            Console.WriteLine(k + " " + x);
     
            return a[k] + b[k] * (x - tabX[k + 1]) + c[k] * (x - tabX[k + 1]) * (x - tabX[k + 1]) + d[k] * (x - tabX[k + 1]) * (x - tabX[k + 1]) * (x - tabX[k + 1]);
        }
    }
}
