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
    public partial class Многочлен_Лагранжа : Form
    {
        double a; //= Convert.ToDouble(textBox1.Text);
        double b; //= Convert.ToDouble(textBox2.Text);
        bool visible = true;
        bool check = true;
        double[] tabX = new double[4];
        double[] tabY = new double[4];
        linear_spline spline1 = new linear_spline();
        parabolic_spline spline2 = new parabolic_spline();
        cubic_spline spline3 = new cubic_spline();
        double d4f(double x)
        {
            return 120 * Math.Pow(Math.PI, 5) * Math.Pow(Math.PI * x + 10, -6);
        }
        double d5f(double x)
        {
            return -720 * Math.Pow(Math.PI, 6) * Math.Pow(Math.PI * x + 10, -7);
        }
        double d6f(double x)
        {
            return 5040 * Math.Pow(Math.PI, 7) * Math.Pow(Math.PI * x + 10, -8);
        }
        double absE(double a, double b)
        {
            return FunctionsApp.newtonDecent(d4f, d5f) * Math.Pow((b - a) / 2, 4) / 192;
        }
        double pL(double x)
        {
            double s0, s1, s2, s3;
            s0 = tabY[0] * (x - tabX[1]) * (x - tabX[2]) * (x - tabX[3]) / ((tabX[0] - tabX[1]) * (tabX[0] - tabX[2]) * (tabX[0] - tabX[3]));
            s1 = tabY[1] * (x - tabX[0]) * (x - tabX[2]) * (x - tabX[3]) / ((tabX[1] - tabX[0]) * (tabX[1] - tabX[2]) * (tabX[1] - tabX[3]));
            s2 = tabY[2] * (x - tabX[0]) * (x - tabX[1]) * (x - tabX[3]) / ((tabX[2] - tabX[0]) * (tabX[2] - tabX[1]) * (tabX[2] - tabX[3]));
            s3 = tabY[3] * (x - tabX[0]) * (x - tabX[1]) * (x - tabX[2]) / ((tabX[3] - tabX[0]) * (tabX[3] - tabX[1]) * (tabX[3] - tabX[2]));
            return s0 + s1 + s2 + s3;
        }
        double pN(double x)
        {
            double fx0x1, fx1x2, fx2x3, fx0x1x2, fx1x2x3, fx0x1x2x3;
            fx0x1 = (tabY[1] - tabY[0]) / (tabX[1] - tabX[0]);
            fx1x2 = (tabY[2] - tabY[1]) / (tabX[2] - tabX[1]);
            fx2x3 = (tabY[3] - tabY[2]) / (tabX[3] - tabX[2]);
            fx0x1x2 = (fx1x2 - fx0x1) / (tabX[2] - tabX[0]);
            fx1x2x3 = (fx2x3 - fx1x2) / (tabX[3] - tabX[1]);
            fx0x1x2x3 = (fx1x2x3 - fx0x1x2) / (tabX[3] - tabX[0]);
            return tabY[0] + fx0x1 * (x - tabX[0]) + fx0x1x2 * (x - tabX[0]) * (x - tabX[1]) + fx0x1x2x3 * (x - tabX[0]) * (x - tabX[1]) * (x - tabX[2]);
        }
        public Многочлен_Лагранжа()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            visible = !visible;
            textBox1.Visible = visible;
            textBox2.Visible = visible;
            textBox3.Visible = visible;
            label4.Visible = !visible;
            label5.Visible = !visible;
            label7.Visible = !visible;
            if (visible)
            {
                button2.Text = "Использовать по умолчанию";

            }
            if (!visible)
            {
                button2.Text = "Настраиваемые значения";
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int eps;
            bool is_double1 = true; //= double.TryParse(textBox1.Text, out a);
            bool is_double2 = double.TryParse(textBox2.Text, out b);
            bool is_int = true; //= int.TryParse(textBox3.Text, out eps);
            if (visible)
            {
                is_double1 = double.TryParse(textBox1.Text, out a);
                is_double2 = double.TryParse(textBox2.Text, out b);
                is_int = int.TryParse(textBox3.Text, out eps);
            }
            else
            {
                a = 0;
                b = 0.5;
                eps = 4;
            }
            if (is_double1 && is_double2 && is_int)
            {
                if (true)//a >= 0 && b < 3.1 && a < b)
                {
                    double abs = absE(a, b);
                    for (double i = a; i < b; i += 0.00001)
                    {
                        if (abs / FunctionsApp.f(i) > Math.Pow(10, -eps + 1)) check = false;
                    }
                    if (!check)
                    {
                        MessageBox.Show("Нарушен порядок точности", "Ошибка", MessageBoxButtons.OK);
                    }
                    else
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            tabX[3 - i] = (a + b) / 2 + ((b - a) / 2) * Math.Cos((2 * i + 1) * Math.PI / (8));
                            tabY[3 - i] = FunctionsApp.f(tabX[3 - i]);
                        }
                        spline1.setTabPoints(this.tabX, this.tabY);
                        spline1.calculateSplineCoefficients();
                        spline2.setTabPoints(this.tabX, this.tabY);
                        spline2.calculateSplineCoefficients();
                        spline3.setTabPoints(this.tabX, this.tabY);
                        spline3.calculateAdditionalCoefficients();
                        spline3.calculateRunningCoefficients();
                        spline3.calculateSplineCoefficients();
                        label12.Text = tabX[0].ToString().Remove(5);
                        label14.Text = tabY[0].ToString().Remove(5);
                        label15.Text = tabX[1].ToString().Remove(5);
                        label22.Text = tabY[1].ToString().Remove(5);
                        label16.Text = tabX[2].ToString().Remove(5);
                        label21.Text = tabY[2].ToString().Remove(5);
                        label17.Text = tabX[3].ToString().Remove(5);
                        label23.Text = tabY[3].ToString().Remove(5);
                        button3.Visible = true;
                        button4.Visible = true;
                        button5.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Допустимые границы интервала:" + "\n" + "0 < a < b < 3.1", "Ошибка", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Неверный формат чисел", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            График_полинома_Лагранжа график_Полинома_Лагранжа = new График_полинома_Лагранжа();
            GraphPane pane = график_Полинома_Лагранжа.zedGraphControl1.GraphPane;
            pane.CurveList.Clear();

            // Создадим список точек
            PointPairList list = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();
            for (double x = a; x <= b; x += 0.000001)
            {
                // добавим в список точку
                list.Add(x, pL(x));
                list2.Add(x, pN(x));
                list3.Add(x, FunctionsApp.f(x));
            }
            LineItem myCurve = pane.AddCurve("Lagrange polynomial", list, Color.Blue, SymbolType.None);
            LineItem myCurve2 = pane.AddCurve("Newton polynomial", list2, Color.Green, SymbolType.Circle);
            LineItem myCurve3 = pane.AddCurve("Basic function", list3, Color.Red, SymbolType.None);

            pane.XAxis.MajorGrid.IsVisible = true;
            график_Полинома_Лагранжа.Newton = list2;
            // Задаем вид пунктирной линии для крупных рисок по оси X:
            // Длина штрихов равна 10 пикселям, ...
            pane.XAxis.MajorGrid.DashOn = 10;

            // затем 5 пикселей - пропуск
            pane.XAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив крупных рисок по оси Y
            pane.YAxis.MajorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.YAxis.MajorGrid.DashOn = 10;
            pane.YAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив мелких рисок по оси X
            pane.YAxis.MinorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси Y:
            // Длина штрихов равна одному пикселю, ...
            pane.YAxis.MinorGrid.DashOn = 1;

            // затем 2 пикселя - пропуск
            pane.YAxis.MinorGrid.DashOff = 2;

            // Включаем отображение сетки напротив мелких рисок по оси Y
            pane.XAxis.MinorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.XAxis.MinorGrid.DashOn = 1;
            pane.XAxis.MinorGrid.DashOff = 2;
            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            график_Полинома_Лагранжа.zedGraphControl1.AxisChange();

            // Обновляем график
            график_Полинома_Лагранжа.zedGraphControl1.Invalidate();
            график_Полинома_Лагранжа.a = this.a;
            график_Полинома_Лагранжа.b = this.b;
            график_Полинома_Лагранжа.Show();
        }

       
        private void Button5_Click(object sender, EventArgs e)
        {
            Абсолютные_погрешности_полиномов Абсолютные_погрешности_полиномов = new Абсолютные_погрешности_полиномов();
            Ошибка_сплайнов ошибка_Сплайнов = new Ошибка_сплайнов();
            GraphPane pane = Абсолютные_погрешности_полиномов.zedGraphControl1.GraphPane;
            GraphPane pane2 = ошибка_Сплайнов.zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            // Создадим список точек
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();
            PointPairList list4 = new PointPairList();
            PointPairList list5 = new PointPairList();
            for (double x = a; x <= b; x += 0.000001)
            {
                // добавим в список точку
                list1.Add(x, Math.Abs(pN(x) - FunctionsApp.f(x)));
                list2.Add(x, Math.Abs(pL(x) - FunctionsApp.f(x)));
            }
            for (double x = tabX[0]; x < tabX[3]; x += 0.0001)
            {
                list3.Add(x, Math.Abs((FunctionsApp.f(x) - spline1.getPoint(x))) / FunctionsApp.f(x));
                list4.Add(x, Math.Abs((FunctionsApp.f(x) - spline2.getPoint(x))) / FunctionsApp.f(x));
                list5.Add(x, Math.Abs((FunctionsApp.f(x) - spline3.getPoint(x))) / FunctionsApp.f(x));
            }
            LineItem myCurve = pane.AddCurve("Newton polynomial absolute error", list1, Color.Blue, SymbolType.None);
            LineItem myCurve2 = pane.AddCurve("Langrange polynomial absolute error", list2, Color.Red, SymbolType.Circle);
            LineItem myCurve3 = pane2.AddCurve("Linear spline error", list3, Color.Violet, SymbolType.None);
            LineItem myCurve4 = pane2.AddCurve("Parabolic spline error", list4, Color.Red, SymbolType.None);
            LineItem myCurve5 = pane2.AddCurve("Cubic spline error", list5, Color.Blue, SymbolType.None);
            pane.XAxis.MajorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси X:
            // Длина штрихов равна 10 пикселям, ...
            pane.XAxis.MajorGrid.DashOn = 10;

            // затем 5 пикселей - пропуск
            pane.XAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив крупных рисок по оси Y
            pane.YAxis.MajorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.YAxis.MajorGrid.DashOn = 10;
            pane.YAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив мелких рисок по оси X
            pane.YAxis.MinorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси Y:
            // Длина штрихов равна одному пикселю, ...
            pane.YAxis.MinorGrid.DashOn = 1;

            // затем 2 пикселя - пропуск
            pane.YAxis.MinorGrid.DashOff = 2;

            // Включаем отображение сетки напротив мелких рисок по оси Y
            pane.XAxis.MinorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.XAxis.MinorGrid.DashOn = 1;
            pane.XAxis.MinorGrid.DashOff = 2;

            pane2.XAxis.MajorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси X:
            // Длина штрихов равна 10 пикселям, ...
            pane2.XAxis.MajorGrid.DashOn = 10;

            // затем 5 пикселей - пропуск
            pane2.XAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив крупных рисок по оси Y
            pane2.YAxis.MajorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane2.YAxis.MajorGrid.DashOn = 10;
            pane2.YAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив мелких рисок по оси X
            pane2.YAxis.MinorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси Y:
            // Длина штрихов равна одному пикселю, ...
            pane2.YAxis.MinorGrid.DashOn = 1;

            // затем 2 пикселя - пропуск
            pane2.YAxis.MinorGrid.DashOff = 2;

            // Включаем отображение сетки напротив мелких рисок по оси Y
            pane2.XAxis.MinorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane2.XAxis.MinorGrid.DashOn = 1;
            pane2.XAxis.MinorGrid.DashOff = 2;
            ///////////////////////////////////////////////////

            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            ошибка_Сплайнов.zedGraphControl1.AxisChange();
            ошибка_Сплайнов.zedGraphControl1.Invalidate();
            ошибка_Сплайнов.Show();
            Абсолютные_погрешности_полиномов.zedGraphControl1.AxisChange();
            // Обновляем график
            Абсолютные_погрешности_полиномов.zedGraphControl1.Invalidate();

            Абсолютные_погрешности_полиномов.Show();
            
        }

        private void Button4_Click_1(object sender, EventArgs e)
        {

            Сплайны сплайны = new Сплайны();
            GraphPane pane = сплайны.zedGraphControl1.GraphPane;
            pane.CurveList.Clear();

            // Создадим список точек
            PointPairList list0 = new PointPairList();
            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            PointPairList list3 = new PointPairList();
            for (double x = tabX[0]; x <= tabX[3]; x += 0.0001)
            {
                // добавим в список точку
                list0.Add(x, FunctionsApp.f(x));
                list1.Add(x, spline1.getPoint(x));
                list2.Add(x, spline2.getPoint(x));
                list3.Add(x, spline3.getPoint(x));
            }
            LineItem myCurve0 = pane.AddCurve("Primary function", list0, Color.Green, SymbolType.None);
            LineItem byCurve1 = pane.AddCurve("linear spline", list1, Color.Violet, SymbolType.None);
            LineItem myCurve2 = pane.AddCurve("parabolic spline", list2, Color.Red, SymbolType.None);
            LineItem myCurve3 = pane.AddCurve("cubic spline", list3, Color.Blue, SymbolType.None);
            pane.XAxis.MajorGrid.IsVisible = true;
            
            // Задаем вид пунктирной линии для крупных рисок по оси X:
            // Длина штрихов равна 10 пикселям, ...
            pane.XAxis.MajorGrid.DashOn = 10;

            // затем 5 пикселей - пропуск
            pane.XAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив крупных рисок по оси Y
            pane.YAxis.MajorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.YAxis.MajorGrid.DashOn = 10;
            pane.YAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив мелких рисок по оси X
            pane.YAxis.MinorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси Y:
            // Длина штрихов равна одному пикселю, ...
            pane.YAxis.MinorGrid.DashOn = 1;

            // затем 2 пикселя - пропуск
            pane.YAxis.MinorGrid.DashOff = 2;

            // Включаем отображение сетки напротив мелких рисок по оси Y
            pane.XAxis.MinorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.XAxis.MinorGrid.DashOn = 1;
            pane.XAxis.MinorGrid.DashOff = 2;
            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            сплайны.zedGraphControl1.AxisChange();

            // Обновляем график
            сплайны.zedGraphControl1.Invalidate();
            сплайны.Show();
        }
    }
}
