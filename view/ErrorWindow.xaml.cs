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

namespace FilghtSim.view
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : UserControl
    {
        private int count = 0;
        private List<Error> errors = new List<Error>();
        public ErrorWindow()
        {
            InitializeComponent();
        }

        public void addError(String errorName)
        {
            count++;
            errors.Add(new Error() { Number = count, errorName = errorName });
            /*
            if(errors.Count > 4)
            {
                errors.RemoveAt(0);
            }
            */
            errorsView.ItemsSource = null;
            errorsView.ItemsSource = errors;
        }

        public class Error
        {
            public int Number { get; set; }
            public string errorName { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            addError("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }
    }
}
