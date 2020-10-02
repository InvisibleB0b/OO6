using System;

namespace FanLibrary
{
    public class FanOutput
    {
        private double _temp;
        private double _fugt;
        private string _name;

        public int Id { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {

                if (value.Length < 2) throw new ArgumentException($"Name must be atleast 2 Charactes long");
                else
                {
                    _name = value;
                }

            }
        }

        public double Temp
        {
            get { return _temp; }
            set
            {
                if (value < 15)
                {
                    throw new ArgumentException("temperaturen er for lav, skal være over 15 grader");
                }
                else if (value > 25)
                {
                    throw new ArgumentException("temperaturen er for høj, skal være under 25 grader");
                }
                else
                {
                    _temp = value;
                }

            }
        }

        public double Fugt
        {
            get { return _fugt; }
            set
            {
                if (value < 30)
                {
                    throw new ArgumentException("fugten er for lav");
                }
                else if (value > 80)
                {
                    throw new ArgumentException("fugten er for høj");
                }
                else
                {
                    _fugt = value;
                }


            }
        }
    }
}
