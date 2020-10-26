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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Многочлен_Лагранжа многочлен_Лагранжа = new Многочлен_Лагранжа();
            многочлен_Лагранжа.Show();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {

            double xmin;
            double xmax;
            bool is_double1 = double.TryParse(textBox1.Text, out xmin);
            bool is_double2 = double.TryParse(textBox2.Text, out xmax);
            if (is_double1 && is_double2)
            {
                График_Функции график_Функции = new График_Функции();
                GraphPane pane = график_Функции.zedGraphControl1.GraphPane;
                pane.CurveList.Clear();

                // Создадим список точек
                PointPairList list = new PointPairList();
                for (double x = xmin; x <= xmax; x += 0.00001)
                {
                    // добавим в список точку
                    list.Add(x, FunctionsApp.f(x));
                }
                LineItem myCurve = pane.AddCurve("f(x)", list, Color.Blue, SymbolType.None);
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
                график_Функции.zedGraphControl1.AxisChange();

                // Обновляем график
                график_Функции.zedGraphControl1.Invalidate();
                график_Функции.Show();
            }
            else
            {
                MessageBox.Show("Неверный формат чисел", "Ошибка", MessageBoxButtons.OK);
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Многочлен_Лагранжа многочлен_Лагранжа = new Многочлен_Лагранжа();
            многочлен_Лагранжа.Show();
        }
    }
}
