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
    /// Interaction logic for JoyStick.xaml
    /// </summary>
    public partial class JoyStick : UserControl
    {
        public JoyStick()
        {
            InitializeComponent();
        }

        public double Elevator
        {
            get { return Convert.ToDouble(GetValue(ElevatorProperty)); }
            set { SetValue(ElevatorProperty, value); }
        }
        public static readonly DependencyProperty ElevatorProperty =
            DependencyProperty.Register("Elevator", typeof(double), typeof(JoyStick), null);

        public double Rudder
        {
            get { return Convert.ToDouble(GetValue(RudderProperty)); }
            set { SetValue(RudderProperty, value); }
        }
        public static readonly DependencyProperty RudderProperty =
            DependencyProperty.Register("Rudder", typeof(double), typeof(JoyStick), null);

        private void centerKnob_Completed(object sender, EventArgs e) { }

        private Point initialPoint = new Point();

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                initialPoint = e.GetPosition(this);
            }
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed) { return; }
            double x = e.GetPosition(this).X - initialPoint.X;
            double y = e.GetPosition(this).Y - initialPoint.Y;
            if (Math.Sqrt(x * x + y * y) < Base.Width / 2)
            {
                knobPosition.X = x;
                knobPosition.Y = y;
            }
            double rudder = x / (Base.Width / 2);
            if (rudder > 1) { rudder = 1; }
            else if (rudder < -1) { rudder = -1; }
            double elevator = y / (Base.Width / 2);
            if (elevator > 1) { elevator = 1; }
            else if (elevator < -1) { elevator = -1; }
            Rudder = rudder;
            Elevator = elevator;
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
            Rudder = 0;
            Elevator = 0;
        }

        private void Knob_MouseLeave(object sender, MouseEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
            Rudder = 0;
            Elevator = 0;
        }
    }
}
