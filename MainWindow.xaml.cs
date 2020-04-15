using FilghtSim.model;
using FilghtSim.telnet;
using FlightSim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace FilghtSim
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Console.WriteLine("Hello World!");
            ITelnetClient a = new MyTelnetClient();
            IModel m = new Model(a);
            ViewModel vm = new ViewModel(m);
            Indicators.DataContext = vm;
            Map.DataContext = vm;
            Controls.DataContext = vm;
            ErrorWindow.DataContext = vm;
            // TopBar.DataContext = vm;
            TopBar.SetVM(vm);
        }
    }
}

/*
while (true)
{
    Console.WriteLine("vm._Roll:" + String.Format("{0:0.##}", vm._Roll));
    Console.WriteLine("vm._Pitch:" + String.Format("{0:0.##}", vm._Pitch));
    Console.WriteLine("vm._VerticalSpeed:" + String.Format("{0:0.##}", vm._VerticalSpeed));
    Console.WriteLine("vm._GroundSpeed:" + String.Format("{0:0.##}", vm._GroundSpeed));
    Console.WriteLine("vm._AirSpeed:" + String.Format("{0:0.##}", vm._AirSpeed));
    Console.WriteLine("vm._Heading:" + String.Format("{0:0.##}", vm._Heading));
    Console.WriteLine("vm._Altitude:" + String.Format("{0:0.##}", vm._Altitude));
    Console.WriteLine("vm._Altimeter:" + String.Format("{0:0.##}", vm._Altimeter));
    Console.WriteLine("vm._Latitude:" + String.Format("{0:0.##}", vm._Latitude));
    Console.WriteLine("vm._Longtitude:" + String.Format("{0:0.##}", vm._Longtitude));
    Console.WriteLine("/////////////////////////////////////////");
    Thread.Sleep(250);
}
*/
