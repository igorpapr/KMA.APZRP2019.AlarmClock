using System.Windows.Controls;

namespace AlarmClockWPFClient.Navigation
{
    interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}
