using System;
using System.Windows.Controls;

namespace WpfApp1
{
    public static class Pager
    {
        internal static void ChangePageTo(Page page)
        {
            ChangePage?.Invoke(null, page);
        }

        internal static event EventHandler<Page> ChangePage;
    }
}