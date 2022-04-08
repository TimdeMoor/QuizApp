using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Quizapp
{
    /// <summary>
    /// Interaction logic for frmConfirm.xaml
    /// </summary>

    public partial class frmConfirm : Window
    {
		//constructor, (message is mandatory, yes and no are optional)
		public frmConfirm(string message, string yes = "Yes", string no = "No")
		{
			InitializeComponent();
			lblMessage.Content = message;
			btnPositive.Content = yes;
			btnNegative.Content = no;
		}

		//returns this form as true
		private void BtnPositive_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}

		//returns this form as false
		private void BtnNegative_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}
	}
}
