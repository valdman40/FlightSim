using FilghtSim.telnet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;


namespace FilghtSim.model
{
    public class Model : IModel
    {
        private ITelnetClient Client;
        private double roll=0;
        private double pitch=0;
        private double verticalSpeed=0;
        private double aileron = 0;
        private double rudder = 0;
        private double elevator = 0;
        private double throttle = 0;
        private double groundSpeed = 0;
        private double airSpeed = 0;
        private double heading = 0;
        private double altitude = 0;
        private double altimeter = 0;
        private double latitude = 0;
        private double longitude = 0;
        private string location;
        private bool stopBool = false;
        private String errorString = "";
        private int errorCount = 0;
        private static Mutex mutex = new Mutex();
        public event PropertyChangedEventHandler PropertyChanged;

        public String ErrorString
        {
            get{
                if (errorCount > 4)
                {
                    string[] allErrors = errorString.Split('\n');
                    int len = allErrors.Length;
                    return allErrors[len - 5] + "\n" +
                           allErrors[len - 4] + "\n" +
                           allErrors[len - 3] + "\n" +
                           allErrors[len - 2] + "\n"; // the last one (len-1) is " "

                }
                return errorString;
            }
                set {
                errorCount++;
                errorString = errorString + errorCount + ". "+ value + "\n"; 
                NotifyPropertyChanged("ErrorString");
            } 
        }
        public bool StopBool { get { return stopBool; } set { stopBool = value; NotifyPropertyChanged("Bool Changed"); } }
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
            StopBool = true;
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
                while (StopBool)
                {
                    try
                    {
                        mutex.WaitOne();

                        // Roll
                        // Client.write("get /instrumentation/attitude-indicator/internal-roll-deg\r\n");
                        try  { 
                            Client.write("get /wnstrumentation/attitude-indicator/internal-roll-deg\r\n");
                            Roll = Convert.ToDouble(Client.read()); 
                        }
                        catch(Exception e1)  {  ErrorString = "Roll"; }

                        // Pitch
                        try {
                            Client.write("get /instrumentation/attitude-indicator/internal-pitch-deg\r\n");
                            Pitch = Convert.ToDouble(Client.read());
                        }
                        catch (Exception e1)  { ErrorString = "Pitch"; }

                        // VerticalSpeed
                        try
                        {
                            Client.write("get /instrumentation/gps/indicated-vertical-speed\r\n");
                            VerticalSpeed = Convert.ToDouble(Client.read());
                        }
                        catch (Exception e1) { ErrorString = "VerticalSpeed"; }

                        // GroundSpeed
                        try {
                            Client.write("get /instrumentation/gps/indicated-ground-speed-kt\r\n");
                            GroundSpeed = Convert.ToDouble(Client.read());}
                        catch (Exception e1)  { ErrorString = "GroundSpeed"; }

                        // AirSpeed
                        try {
                            Client.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\r\n"); 
                            AirSpeed = Convert.ToDouble(Client.read()); }
                        catch (Exception e1)  { ErrorString = "AirSpeed"; }

                        // Heading
                        try {
                            Client.write("get /instrumentation/heading-indicator/indicated-heading-deg\r\n");
                            Heading = Convert.ToDouble(Client.read()); }
                        catch (Exception e1) { ErrorString = "Heading"; }

                        // Altitude
                        try  {
                            Client.write("get /instrumentation/gps/indicated-altitude-ft\r\n");
                            Altitude = Convert.ToDouble(Client.read());}
                        catch (Exception e1)  { ErrorString = "Altitude";}

                        // Altimeter
                        try{
                            Client.write("get /instrumentation/altimeter/indicated-altitude-ft\r\n");
                            Altimeter = Convert.ToDouble(Client.read()); }
                        catch (Exception e1)  { ErrorString = "Altimeter"; }

                        // Latitude
                        try   {
                            Client.write("get /position/latitude-deg\r\n");
                            Latitude = Convert.ToDouble(Client.read());}
                        catch (Exception e1) { ErrorString = "Latitude";  }

                        // Longitude
                        try  {
                            Client.write("get /position/longitude-deg\r\n");
                            Longitude = Convert.ToDouble(Client.read()); }
                        catch (Exception e1) { ErrorString = "Longitude"; }

                        // Location
                        if ((longitude <= 180 && longitude >= -180) && (latitude <= 90 && latitude >= -90))
                        {
                            Location = latitude + "," + longitude;
                        } else
                        {
                            ErrorString = "plain is out of bounds";
                        }
                        mutex.ReleaseMutex();
                        Thread.Sleep(250);
                    }
                    catch (System.IO.IOException Err)
                    {
                        ErrorString = "Timeout exception";
                    }
                }
            }).Start();
        }

        public void Stop()
        {
            StopBool = false;
        }
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public void setThrottle(double value)
        {
            mutex.WaitOne();
            Throttle = value;
            Client.write("set /controls/engines/current-engine/throttle " + value + "\r\n");
            Console.WriteLine("Throttle is set to- " + Client.read());
            mutex.ReleaseMutex();
        }

        public void setRudder(double value)
        {
            mutex.WaitOne();
            Rudder = value;
            Client.write("set /controls/flight/rudder " + value + "\r\n");
            Console.WriteLine("Rudder is set to- " + Client.read());
            mutex.ReleaseMutex();
        }

        public void setAileron(double value)
        {
            mutex.WaitOne();
            Aileron = value;
            Client.write("set /controls/flight/aileron " + value + "\r\n");
            Console.WriteLine("Aileron is set to- " + Client.read());

            mutex.ReleaseMutex();
        }

        public void setElevator(double value)
        {
            mutex.WaitOne();
            if (value > 1) { value = 1; }
            else if (value < -1) { value = -1; }
            Elevator = value;
            Client.write("set /controls/flight/elevator " + value + "\r\n");
            Console.WriteLine("Elevator is set to- " + Client.read());
            mutex.ReleaseMutex();
        }
    }
}