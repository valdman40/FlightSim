
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FilghtSim.model
{
    public interface IModel : INotifyPropertyChanged
    {

        double Roll { get; set; }
        double Rudder { get; set; }
        double Elevator { get; set; }
        double Aileron { get; set; }
        double Throttle { get; set; }
        double Pitch { get; set; }
        double VerticalSpeed { get; set; }
        double GroundSpeed { get; set; }
        double AirSpeed { get; set; }
        double Heading { get; set; }
        double Altitude { get; set; }
        double Altimeter { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
        string Location { get; set; }



        void Start();
        void Stop();
        void Connect(string ip, int port);
        void setElevator(double value);
        void setThrottle(double value);
        void setRudder(double value);
        void setAileron(double value);



    }
}