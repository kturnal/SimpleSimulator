using System;

namespace SimpleSimulator.Models
{
    public class ProjectileModel
    {
        public double Speed { get; set; }
        public double Angle { get; set; }
        public double Height { get; set; }
        private const double Gravity = 9.81; // Gravity constant

        public ProjectileModel(double speed, double angle, double height)
        {
            Speed = speed;
            Angle = angle;
            Height = height;
        }

        public double GetXPosition(double time)
        {
            double radians = Math.PI * Angle / 180.0;
            return Speed * Math.Cos(radians) * time;
        }

        public double GetYPosition(double time)
        {
            double radians = Math.PI * Angle / 180.0;
            return Height + (Speed * Math.Sin(radians) * time) - (0.5 * Gravity * time * time);
        }

        public bool HasHitGround(double time)
        {
            if (time == 0) 
            {
                return false;
            }

            return GetYPosition(time) < 0;
        }
    }
}
