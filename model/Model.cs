using FilghtSim.telnet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;


namespace FilghtSim.model
{
    public class Model : IModel
    {

        private ITelnetClient Client;
        private double roll;
        private double pitch;
        private double verticalSpeed;
        private double aileron;
        private double rudder;
        private double elevator;
        private double throttle;
        private double groundSpeed;
        private double airSpeed;
        private double heading;
        private double altitude;
        private double altimeter;
        private double latitude;
        private double longitude;
        private string location;
        private bool StopBool = false;
        private static Mutex mutex = new Mutex();
        public event PropertyChangedEventHandler PropertyChanged;

        public double Roll { get { return roll; } set { roll = value; NotifyPropertyChanged("Roll"); } }
        public double VerticalSpeed { get { return verticalSpeed; } set { verticalSpeed = value; NotifyPropertyChanged("VerticalSpeed"); } }
        public double Pitch { get { return pitch; } set { pitch = value; NotifyPropertyChanged("Pitch"); } }
        public double GroundSpeed { get { return groundSpeed; } set { groundSpeed = value; NotifyPropertyChanged("GroundSpeed"); } }
        public double AirSpeed { get { return airSpeed; } set { airSpeed = value; NotifyPropertyChanged("AirSpeed"); } }
        public double Heading { get { return heading; } set { heading = value; NotifyPropertyChanged("Heading"); } }
        public double Altitude { get { return altitude; } set { altitude = value; NotifyPropertyChanged("Altitude"); } }
        public double Altimeter { get { return altimeter; } set { altimeter = value; NotifyPropertyChanged("Altimeter"); } }
        public double Latitude { get { return latitude; } set { latitude = value; NotifyPropertyChanged("Latitude"); } }
        public double Longitude { get { return longitude; } set { longitude = value; NotifyPropertyChanged("Longitude"); } }
        public double Aileron { get { return aileron; } set { aileron = value; NotifyPropertyChanged("Aileron"); } }
        public double Rudder { get { return rudder; } set { rudder = value; NotifyPropertyChanged("Rudder"); } }
        public double Throttle { get { return throttle; } set { throttle = value; NotifyPropertyChanged("Throttle"); } }
        public double Elevator { get { return elevator; } set { elevator = value; NotifyPropertyChanged("Elevator"); } }

        public string Location { get { return location; } set { location = value; NotifyPropertyChanged("Location"); } }

        public Model(ITelnetClient c)
        {
            this.Client = c;
            Latitude = 32.002644;
            Longitude = 34.888781;
            Location = Latitude.ToString() + " , " + Longitude.ToString();
        }
        public void Connect(string ip, int port)
        {
            this.Client.connect(ip, port);
        }
        public void Disconnect()
        {
            this.Client.disconnect();
        }

        public void Start()
        {
            new Thread(delegate ()
            {
                while (!StopBool)
                {
                    try
                    {
                        mutex.WaitOne();
                        Client.write("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                        Roll = Convert.ToDouble(Client.read());
                        Client.write("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                        Pitch = Convert.ToDouble(Client.read());
                        Client.write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                        VerticalSpeed = Convert.ToDouble(Client.read());
                        Client.write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                        GroundSpeed = Convert.ToDouble(Client.read());
                        Client.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n");
                        AirSpeed = Convert.ToDouble(Client.read());
                        Client.write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                        Heading = Convert.ToDouble(Client.read());
                        Client.write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                        Altitude = Convert.ToDouble(Client.read());
                        Client.write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                        Altimeter = Convert.ToDouble(Client.read());
                        Client.write("get /position/latitude-deg\r\n");
                        Latitude = Convert.ToDouble(Client.read());
                        Client.write("get /position/longitude-deg\r\n");
                        Longitude = Convert.ToDouble(Client.read());
                        Client.write("get /controls/flight/aileron\r\n");
                        Aileron = Convert.ToDouble(Client.read());
                        Client.write("get /controls/flight/elevator\r\n");
                        Elevator = Convert.ToDouble(Client.read());
                        Client.write("get /controls/flight/rudder\r\n");
                        Rudder = Convert.ToDouble(Client.read());
                        Client.write("get /controls/engines/current-engine/throttle\r\n");
                        Throttle = Convert.ToDouble(Client.read());
                        if ((longitude <= 180 && longitude >= -180) && (latitude <= 81 && latitude >= -81))
                        {
                            Location = latitude + "," + longitude;
                        }
                        mutex.ReleaseMutex();
                        Thread.Sleep(250);
                    }
                    catch (Exception Err)
                    {
                        Console.WriteLine("Invalid input from server" + Err.StackTrace);
                    }
                }
            }).Start();
        }

        public void Stop()
        {
            StopBool = true;
        }
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        public void setThrottle(double value)
        {
            mutex.WaitOne();
            Client.write("set /controls/engines/current-engine/throttle " + value + "\r\n");
            Console.WriteLine("Throttle is set to- " + Client.read());
            mutex.ReleaseMutex();
        }

        public void setRudder(double value)
        {
            mutex.WaitOne();
            Client.write("set /controls/flight/rudder " + value + "\r\n");
            Console.WriteLine("Rudder is set to- " + Client.read());
            mutex.ReleaseMutex();
        }

        public void setAileron(double value)
        {
            mutex.WaitOne();
            Client.write("set /controls/flight/aileron " + value + "\r\n");
            Console.WriteLine("Aileron is set to- " + Client.read());

            mutex.ReleaseMutex();
        }

        public void setElevator(double value)
        {
            mutex.WaitOne();
            if (value > 1) { value = 1; }
            else if (value < -1) { value = -1; }
            Client.write("set /controls/flight/elevator " + value + "\r\n");
            Console.WriteLine("Elevator is set to- " + Client.read());
            mutex.ReleaseMutex();
        }
    }
}