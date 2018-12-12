﻿using System;
using System.Collections.Generic;

namespace Sumo.Geo.Metrics
{
    //https://en.wikipedia.org/wiki/Angle
    public enum UnitsOfAngle
    {
        Undefined = 0,
        BinaryDegree = 1,
        ClockPosition = 2,
        CompassPoint = 3,
        Degree = 4,
        Gradian = 5,
        Milliradian = 6,
        MinuteOfArc = 7,
        Quadrant = 8,
        Radian = 9,
        SecondOfArc = 10,
        Sextant = 11,
        Turn = 12
    }

    public class Angle : IEquatable<Angle>
    {
        public Angle() { }

        public Angle(double value, UnitsOfAngle units)
        {
            Value = value;
            Units = units;
        }

        public double Value { get; set; }
        public UnitsOfAngle Units { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Angle);
        }

        public bool Equals(Angle other)
        {
            return other != null &&
                   Value == other.Value &&
                   Units == other.Units;
        }

        public override int GetHashCode()
        {
            var hashCode = -77776330;
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + Units.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Angle heading1, Angle heading2)
        {
            return EqualityComparer<Angle>.Default.Equals(heading1, heading2);
        }

        public static bool operator !=(Angle heading1, Angle heading2)
        {
            return !(heading1 == heading2);
        }

        public override string ToString()
        {
            var units = Units.ToString();
            switch (Units)
            {
                case UnitsOfAngle.Undefined:
                    break;
                case UnitsOfAngle.BinaryDegree:
                    break;
                case UnitsOfAngle.ClockPosition:
                    break;
                case UnitsOfAngle.CompassPoint:
                    break;
                case UnitsOfAngle.Degree:
                    break;
                case UnitsOfAngle.Gradian:
                    break;
                case UnitsOfAngle.Milliradian:
                    break;
                case UnitsOfAngle.MinuteOfArc:
                    break;
                case UnitsOfAngle.Quadrant:
                    break;
                case UnitsOfAngle.Radian:
                    break;
                case UnitsOfAngle.SecondOfArc:
                    break;
                case UnitsOfAngle.Sextant:
                    break;
                case UnitsOfAngle.Turn:
                    break;
                default:
                    break;
            }
            return $"{Value} {units}";
        }
    }
}
