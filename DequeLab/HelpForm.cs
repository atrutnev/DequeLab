using System.Windows.Forms;
using System.IO;

namespace DequeLab
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            LoadHelp();
        }

        private void LoadHelp()
        {
            try
            {
                richTextBox1.Text = File.ReadAllText("help.txt");
                ShowDialog();
            }
            catch
            {
                if (richTextBox1.Text == "")
                {
                    MessageBox.Show("В каталоге с программой не найден файл справки (help.txt)!");   
                }
            }
        }
    }
}
