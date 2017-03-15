using System;
using System.Windows.Forms;

namespace DequeLab
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckRange())
            {
                Properties.Settings.Default.dLeftMaxSize = Convert.ToInt32(numericUpDown1.Value);
                Properties.Settings.Default.dRightMaxSize = Convert.ToInt32(numericUpDown2.Value);
                Properties.Settings.Default.dLeftMinFront = Convert.ToInt32(numericUpDown3.Value);
                Properties.Settings.Default.dLeftMaxFront = Convert.ToInt32(numericUpDown4.Value);
                Properties.Settings.Default.dRightMinFront = Convert.ToInt32(numericUpDown5.Value);
                Properties.Settings.Default.dRightMaxFront = Convert.ToInt32(numericUpDown6.Value);
                Properties.Settings.Default.Save();
                Close();
                Application.Restart();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool CheckRange()
        {
            if (numericUpDown3.Value >= numericUpDown4.Value)
            {
                MessageBox.Show("Указан недопустимый диапазон значений элементов левой очереди!");
                return false;
            }
            else if (numericUpDown5.Value >= numericUpDown6.Value)
            {
                MessageBox.Show("Указан недопустимый диапазон значений элементов правой очереди!");
                return false;
            }
            return true;

        }

        private void LoadSettings()
        {
            numericUpDown1.Value = Properties.Settings.Default.dLeftMaxSize;
            numericUpDown2.Value = Properties.Settings.Default.dRightMaxSize;
            numericUpDown3.Value = Properties.Settings.Default.dLeftMinFront;
            numericUpDown4.Value = Properties.Settings.Default.dLeftMaxFront;
            numericUpDown5.Value = Properties.Settings.Default.dRightMinFront;
            numericUpDown6.Value = Properties.Settings.Default.dRightMaxFront;
        }
    }
}
