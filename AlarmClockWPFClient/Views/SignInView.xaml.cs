using AlarmClockWPFClient.Navigation;
using AlarmClockWPFClient.ViewModels;
using System.Windows.Controls;

namespace AlarmClockWPFClient.Views
{
    public partial class SignInView : UserControl, INavigatable
    {
        public SignInView()
        {
            InitializeComponent();
            DataContext = new SignInViewModel();
        }
    }
}
