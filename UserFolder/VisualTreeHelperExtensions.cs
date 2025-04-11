using System.Windows;
using System.Windows.Media;

namespace Castle.UserFolder
{
    public static class VisualTreeHelperExtensions
    {
        public static T FindVisualAncestor<T>(this DependencyObject obj) where T : DependencyObject
        {
            while (obj != null)
            {
                if (obj is T ancestor)
                    return ancestor;
                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }
    }
}