using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Num_meth_3
{
    delegate double func(double x);
    delegate double dfunc(double x);
    delegate double ddfunc(double x);
    delegate void show(double x);
    class FunctionsApp
    {
        public static double f(double x)
        {
            return Math.PI / ((10 + Math.PI * x) * (10 + Math.PI * x));
        }
        public static double newtonDecent(func func, dfunc dfunc)
        {
            double x0 = 0;
            double x1 = x0 + 1;
            double step = 0.0002;
            int i = 0;
            bool first = true;
            while(Math.Abs(func.Invoke(x1) - func.Invoke(x0)) > 0.00000001)
            {
                if (!first)
                {
                    x0 = x1;
                    first = false;
                }
                x1 = x0 + step * dfunc.Invoke(x1);
                if(func.Invoke(x1) < func.Invoke(x0))
                {
                    step /= 2;
                }
                else
                {
                    step *= 2;
                }
                i++;
            }
            return func.Invoke(x1);
        }
    }
}
