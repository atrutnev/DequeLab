using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DequeLab
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Инициализация экземпляра первой очереди,
        /// отображающейся на форме слева.
        /// </summary>

        Deque<int> dLeft = new Deque<int>(Properties.Settings.Default.dLeftMaxSize,
                                         Properties.Settings.Default.dLeftMinFront,
                                         Properties.Settings.Default.dLeftMaxFront);
        /// <summary>
        /// Инициализация экземпляра второй очереди,
        /// отображающейся на форме справа.
        /// </summary>
        Deque<int> dRight = new Deque<int>(Properties.Settings.Default.dRightMaxSize,
                                          Properties.Settings.Default.dRightMinFront,
                                          Properties.Settings.Default.dRightMaxFront);

        /// <summary>
        /// Обработка нажатия кнопки "Заполнить". Вызывает метод разбора строки
        /// чисел, введёных пользователем или сгенерированных автоматически,
        /// отображение элементов в первой и второй очереди на форме, очистку текстовых полей
        /// ввода и результата расчета.
        /// </summary>
        /// 
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Clear();
            }
            Parser(textBox1.Text);
            dLeftDisplay();
            dRightDisplay();
            textBox1.Clear();
        }

        /// <summary>
        /// Разбор строки чисел, введёных пользователем
        /// или сгенерированных автоматически. В случае нахождения в строке элементов
        /// при максимальной заполненности обеих очередей появляется сообщение с указанием элементов,
        /// которые не будут добавлены.
        /// </summary>
        private void Parser(string s)
        {
            char delimiter = ' ';
            string[] substrings = s.Split(delimiter);
            foreach (var substring in substrings)
            {
                if (!dLeft.IsFull())
                {
                    dLeft.Fill(substring);
                }
                else
                {
                    dRight.Fill(substring);
                }
            }
        }

        /// <summary>
        /// Отображение на форме первой очереди (слева).
        /// Если элемент добавлен в начало очереди, его значение будет отображено зеленым цветом.
        /// Если в конец - красным.
        /// Под элементами располагается счетчик, отображающий количество текущих элементов в очереди.
        /// В случае максимального заполнения очереди на месте счетчика отображается информация об этом. 
        /// </summary>
        private void dLeftDisplay()
        {
            listView1.Items.Clear();
            foreach (var element in dLeft.Elements)
            {
                ListViewItem lst = new ListViewItem();
                lst.Text = string.Join(Environment.NewLine, element.ToString());
                if (dLeft.InFrontRange(element))
                {
                    lst.ForeColor = Color.Green;
                }
                else
                {
                    lst.ForeColor = Color.Red;
                }
                listView1.Items.Add(lst);
            }

            if (dLeft.IsFull())
            {
                label1.ForeColor = Color.Green;
                label1.Text = "Дек заполнен!";
            }
            else
            {
                label1.ForeColor = Color.Red;
                label1.Text = dLeft.currSize() + " из " +dLeft.MaxSize + " элементов";
            }
        }

        /// <summary>
        /// Отображение на форме второй очереди (справа).
        /// Если элемент добавлен в начало очереди, его значение будет отображено зеленым цветом.
        /// Если в конец - красным.
        /// Под элементами располагается счетчик, отображающий количество текущих элементов в очереди.
        /// В случае максимального заполнения очереди на месте счетчика отображается информация об этом. 
        /// </summary>
        private void dRightDisplay()
        {
            listView2.Items.Clear();
            foreach (var element in dRight.Elements)
            {
                ListViewItem lst = new ListViewItem();
                lst.Text = string.Join(Environment.NewLine, element.ToString());
                if (dRight.InFrontRange(element))
                {
                    lst.ForeColor = Color.Green;
                }
                else
                {
                    lst.ForeColor = Color.Red;
                }
                listView2.Items.Add(lst);
            }
            if (dRight.IsFull())
            {
                label2.ForeColor = Color.Green;
                label2.Text = "Дек заполнен!";
            }
            else
            {
                label2.ForeColor = Color.Red;
                label2.Text = dRight.currSize() + " из " + dRight.MaxSize + " элементов";
            }

        }

        /// <summary>
        /// Метод, отслеживающий изменение текста в строке ввода.
        /// Проверяет вводимую строку на соответствие допустимому формату.
        /// В зависимости от этого соответствия меняет картинку справа от строки,
        /// а также активирует или блокирует кнопку "Заполнить".
        /// При максимальной заполненности обеих очередей активирует кнопки выбора арифметических действий,
        /// а также блокирует кнопку "Заполнить" до проведения расчета.
        /// </summary>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string pattern = @"^(-?\d{1,3})$|^(-?\d{1,3}\s){1}(-?\d{1,3}\s){0," + 
                            ((dLeft.MaxSize + dRight.MaxSize) - (dLeft.currSize() + dRight.currSize()) - 2) + @"}(-?\d{1,3})$";
            if (!dLeft.IsFull() || !dRight.IsFull())
            {
                if (!Regex.IsMatch(textBox1.Text, pattern))
                {
                    pictureBox1.Image = Properties.Resources.red_cross;
                    button1.Enabled = false;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.green_check;
                    button1.Enabled = true;
                }
            }
            else
            {
                pictureBox1.Image = Properties.Resources.red_cross;
                button1.Enabled = false;
                rbInGbEnabled();
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки "Расчет". В зависимости от обозначения кнопки
        /// арифметической операции вызывает проведение этой арифметической операции
        /// для элементов из каждой очереди с выводом результатов в текстовое поле.
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            foreach (RadioButton item in groupBox1.Controls)
            {
                if ((item).Checked)
                {
                    Calculation(Convert.ToChar(item.Text));                   
                }
            }
        }

        private void Calculation(char c)
        {
            while (!dLeft.IsEmpty() && !dRight.IsEmpty())
            {
                richTextBox1.AppendText(Utils.Calc(dLeft.Pop_Front(), dRight.Pop_Back(), c));
            }
            if (dLeft.currSize() > 1 && dLeft.currSize() % 2 == 0)
            {
                while (dLeft.currSize() > 1)
                {
                    int x = dLeft.Pop_Front();
                    dRight.Push_Back(x);
                    richTextBox1.AppendText(Utils.Calc(dLeft.Pop_Front(), dRight.Pop_Back(), c));
                }

            }
            else if (dLeft.currSize() > 1 && dLeft.currSize() % 2 != 0)
            {
                while (dLeft.currSize() > 1)
                {
                    int x = dLeft.Pop_Front();
                    dRight.Push_Back(x);
                    richTextBox1.AppendText(Utils.Calc(dLeft.Pop_Front(), dRight.Pop_Back(), c));
                }
                richTextBox1.AppendText(dLeft.Pop_Front().ToString() + " - для этого элемента отсутствует пара.");
            }

            else if (dLeft.currSize() == 1)
            {
                richTextBox1.AppendText(dLeft.Pop_Front().ToString() + " - для этого элемента отсутствует пара.");
            }

            else if (dRight.currSize() > 1 && dRight.currSize() % 2 == 0)
            {
                while (dRight.currSize() > 1)
                {
                    int x = dRight.Pop_Back();
                    dLeft.Push_Front(x);
                    richTextBox1.AppendText(Utils.Calc(dLeft.Pop_Front(), dRight.Pop_Back(), c));
                }
            }
            else if (dRight.currSize() > 1 && dRight.currSize() % 2 != 0)
            {
                while (dRight.currSize() > 1)
                {
                    int x = dRight.Pop_Back();
                    dLeft.Push_Front(x);
                    richTextBox1.AppendText(Utils.Calc(dLeft.Pop_Front(), dRight.Pop_Back(), c));
                }
                richTextBox1.AppendText(dRight.Pop_Front().ToString() + " - для этого элемента отсутствует пара.");
            }
            
            else if (dRight.currSize() == 1)
            {
                richTextBox1.AppendText(dRight.Pop_Front().ToString() + " - для этого элемента отсутствует пара.");
            }
        }

        /// <summary>
        /// Метод, отслеживающий изменение состояния какой-либо из кнопок
        /// выбора арифметических действий. При активации одной из указанных кнопок,
        /// активирует кнопку "Расчет".
        /// </summary>
        private void radioButtonCheckedChanged(object sender, EventArgs e)
        {
            foreach (RadioButton item in groupBox1.Controls)
            {
                if (((RadioButton)sender).Checked)
                {
                    button2.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Метод, отслеживающий изменение состояния текстового поля, в которое выводятся
        /// результаты арифметических операций. При изменении этого поля метод блокирует кнопки
        /// выбора арифметических действий и кнопку "Расчет".
        /// </summary>
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            rbInGbDisabled();
            button2.Enabled = false;
        }

        /// <summary>
        /// Метод активации кнопок арифметических операций.
        /// </summary>
        private void rbInGbEnabled()
        {
            foreach (RadioButton item in groupBox1.Controls)
            {
                item.Enabled = true;
            }
        }

        /// <summary>
        /// Метод блокировки кнопок арифметических операций.
        /// Также снимает отметку выбора с данных кнопок.
        /// </summary>
        private void rbInGbDisabled()
        {
            foreach (RadioButton item in groupBox1.Controls)
            {
                if ((item).Checked)
                {
                    (item).Checked = false;
                }
                item.Enabled = false;
            }
        }

        /// <summary>
        /// Обработка нажатия кнопки "Автогенерация". В зависимости от количества элементов,
        /// уже находящихся в очередях, автоматически генерирует оставшееся число элементов,
        /// необходимых для максимального заполнения обеих очередей. Сгенерированные элементы
        /// отображаются в строке ввода в допустимом формате.
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            int i = 1; 
            int diff = (dLeft.MaxSize + dRight.MaxSize) - (dLeft.currSize() + dRight.currSize());
            if (diff != 0)
            {
                while (i < diff)
                {
                    textBox1.AppendText(Utils.genInt().ToString() + " ");
                    i++;
                }
                textBox1.AppendText(Utils.genInt().ToString());
            }
            richTextBox1.Clear();
        }

        /// <summary>
        /// Вызов формы настроек
        /// </summary>
        private void настройкиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsForm sf = new SettingsForm();
            sf.ShowDialog();
        }

        private void справкаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            HelpForm hf = new HelpForm();
        }
    }
}
