using FilghtSim.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSim
{
    public class ViewModel : INotifyPropertyChanged
    {
        public IModel model;



        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel(IModel model1)
        {
            model = model1;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("_" + e.PropertyName);
                };
        }

        public void cleanErrorString() { model.cleanErrorString(); }
        public String _ErrorString { get { return model.ErrorString; } }
        public double _Roll { get { return model.Roll; } }
        public double _Pitch   { get    {  return model.Pitch;   } }

        public double _GroundSpeed
        {
            get
            {
                    return model.GroundSpeed;

            }
        }

        public double _AirSpeed
        {
            get
            {
                    return model.AirSpeed;


            }
        }

        public double _VerticalSpeed
        {
            get
            {

                    return model.VerticalSpeed;

            }
        }

        public double _Heading
        {
            get
            {

                    return model.Heading;

            }
        }

        public double _Altimeter
        {
            get
            {

                    return model.Altimeter;

            }
        }

        public double _Altitude
        {
            get
            {

                    return model.Altitude;

            }
        }

        public double _Latitude
        {
            get
            {
                    return model.Latitude;
            }
        }

        public double _Longtitude { get { return model.Longitude; } }

        private double throttle;

        public double _Throttle
        {
            get { return throttle; }
            set
            {
                if (IsConnected())
                {
                    throttle = value; model.setThrottle(value);
                }
            }
        }

        private double rudder;
        public double _Rudder
        {
            get { return rudder; }
            set
            {
                if (IsConnected())
                {
                    rudder = value; model.setRudder(value);
                }
            }
        }

        private double aileron;
        public double _Aileron
        {
            get { return aileron; }
            set
            {
                if (IsConnected())
                {
                    aileron = value; model.setAileron(value);
                }
            }
        }

        internal void Stop()
        {
            model.Stop();
        }

        private double elevator;
        public double _Elevator
        {
            get { return elevator; }
            set
            {
                if (IsConnected())
                {
                    elevator = value; model.setElevator(value);
                }
            }
        }

        public string _Location { get { return model.Location; } }

        public bool IsConnected()
        {
            return model.IsConnected;
        }
        public void Connect(string ip,int port)
        {
            model.Connect(ip, port);
        }
        public void Start()
        {
            model.Start();
        }

        public void NotifyPropertyChanged(string propName)
        {
          
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
