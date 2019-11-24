using AlarmClockWPFClient.Navigation;
using AlarmClockWPFClient.ViewModels;
using System.Windows.Controls;

namespace AlarmClockWPFClient.Views
{
    public partial class SignUpView : UserControl, INavigatable
    {
        public SignUpView()
        {
            InitializeComponent();
            DataContext = new SignUpViewModel();
        }
    }
}
