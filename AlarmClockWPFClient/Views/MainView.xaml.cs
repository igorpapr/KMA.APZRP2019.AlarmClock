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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AlarmClockWPFClient.Navigation;
using AlarmClockWPFClient.ViewModels;

namespace AlarmClockWPFClient.Views
{
    public partial class MainView : UserControl, INavigatable
    {
        public MainView()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

        private void TimePicker_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = FocusManager.GetFocusedElement(this) as TextBox;
            if (textBox != null)
            {
                textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                textBox.Focus();
            }
        }
    }
}
