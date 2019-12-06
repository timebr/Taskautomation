using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace PowerShellGui
{
    public class Computer
    {
        string ComputerName;
        bool Availability;
        string User;

        public Computer(String Node)
        {
            SetComputerName(Node);
            Availability = PingHost(Node);
        }

        public void SetComputerName(String name)
        {
            if(name.Length == 6 && IsDigitsOnly(name))
            {
                this.ComputerName = "hw-wop-" + name;
            }
            else
            {
                ComputerName = name;
            }
        }

        public String GetComputerName()
        {
            return ComputerName;
        }

        public bool GetAvailability()
        {
            return Availability;
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private bool PingHost(String ComputerName)
        {
            int timeout = 10000;
            Ping ping = new Ping();
            try
            {
                PingReply pingreply = ping.Send(ComputerName, timeout);
                if (pingreply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
