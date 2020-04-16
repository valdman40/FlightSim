using FilghtSim.model;
using FilghtSim.telnet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FlightSim
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public ViewModel vm { get; internal set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            vm = new ViewModel(new Model(new MyTelnetClient()));
        }
    }
}
