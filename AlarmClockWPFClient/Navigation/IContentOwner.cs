using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AlarmClockWPFClient.Navigation
{
    interface IContentOwner
    {
        ContentControl ContentControl { get; }
    }
}
