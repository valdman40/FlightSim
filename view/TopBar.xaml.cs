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
using System.Diagnostics;
using FlightSim;

namespace FilghtSim.view
{
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl
    {
        private ViewModel vm;
        public TopBar()
        {
            InitializeComponent();
        }

        private void ConnectButtonClick(object sender, RoutedEventArgs e)
        {
            if (!(vm.IsConnected()))
            {
                string stringPort = PortFromUser.Text;
                string stringIP = IPFromUser.Text;
                if (String.IsNullOrEmpty(stringIP)) { stringIP = "127.0.0.1"; }
                if (String.IsNullOrEmpty(stringPort)) { stringPort = "5402"; }
                vm.Connect(stringIP, int.Parse(stringPort));
                if (vm.IsConnected())
                {
                    vm.Start();
                }
            }

        }

        internal void SetVM(ViewModel vm)
        {
            this.vm = vm;
        }

        private void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (vm.IsConnected())
            {
                vm.Stop();
            }
        }
    }
    }
