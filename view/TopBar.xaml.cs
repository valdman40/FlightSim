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

                int intPort;
                string stringPort = PortFromUser.Text;
                string stringIP = IPFromUser.Text;
                //default connection 
                if (stringPort.Equals("") && stringIP.Equals(""))
                {
                    //MessageBox.Show($"def");
                    vm.Connect("IP", 5402);
                    if (vm.IsConnected())
                    {
                        vm.Start();
                    }
                    return;
                }
                //default por

                if (stringPort.Equals(""))
                {
                    //MessageBox.Show($"def Port");
                    vm.Connect(stringIP, 5402);
                    if (vm.IsConnected())
                    {
                        vm.Start();
                    }
                    return;
                }
                //default IP
                if (stringIP.Equals(""))
                {
                    //MessageBox.Show($"def IP");
                    intPort = int.Parse(stringPort);
                    vm.Connect("IP", intPort);
                    if (vm.IsConnected())
                    {
                        vm.Start();
                    }
                    return;
                }

                //MessageBox.Show($"ON!");
                intPort = int.Parse(stringPort);
                vm.Connect(stringIP, intPort);
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
                //MessageBox.Show($"OFF");

            }
        }
    }
    }



    /*
     * 
            ViewModel vm = (Application.Current as App).vm;
            if (!vm.IsConnected())
            {

                int intPort;
                string PortAsString = PortFromUser.Text;
                string IPAsString = IPFromUser.Text;
                //default connection 
                if (PortAsString.Equals("") && IPAsString.Equals(""))
                {
                    vm.model.Connect("127.0.0.1", 5402);
                  //  vm.Connect("IP", 5402);
                    if (vm.IsConnected())
                    {
                        vm.Start();
                    }
                    
                }
                //default por

                if (PortAsString.Equals(""))
                {
                    vm.Connect(IPAsString, 5402);
                    if (vm.IsConnected())
                    {
                        vm.Start();
                    }
              //      return;
                }
                //default IP
                if (IPAsString.Equals(""))
                {
                    intPort = int.Parse(PortAsString);
                    vm.Connect("IP", intPort);
                    if (vm.IsConnected())
                    {
                        vm.Start();
                    }
                  //  return;
                }

                intPort = int.Parse(PortAsString);
                vm.Connect(IPAsString, intPort);
                if (vm.IsConnected())
                {
                    vm.Start();
                }
            }
        }
         */