﻿using Sumo.Geo.Metrics;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sumo.Geo.Primitives
{
    public class Point : IEquatable<Point>
    {
        public Point() { }

        public Point(double latitude, double longitude, Distance elevation = null) 
        {
            Latitude = latitude;
            Longitude = longitude;
            Elevation = elevation;
        }

        public Point(Point point) 
        {
            Latitude = point.Latitude;
            Longitude = point.Longitude;
            Elevation = point.Elevation;
        }

        //todo: add validation
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Distance Elevation { get; set; }

        public Distance GeodesicDistance(Point point)
        {
            double phi_s = Latitude.ToRadians(),
                   lamda_s = Longitude.ToRadians(),
                   phi_f = point.Latitude.ToRadians(),
                   lamda_f = point.Longitude.ToRadians();

            // Vincenty formula
            double y = 
                Math.Sqrt(Math.Pow((Math.Cos(phi_f) * Math.Sin(lamda_s - lamda_f)), 2) + 
                Math.Pow((Math.Cos(phi_s) * Math.Sin(phi_f) - Math.Sin(phi_s) * Math.Cos(phi_f) * Math.Cos(lamda_s - lamda_f)), 2));
            double x = Math.Sin(phi_s) * Math.Sin(phi_f) + Math.Cos(phi_s) * Math.Cos(phi_f) * Math.Cos(lamda_s - lamda_f);
            double delta = Math.Atan2(y, x);
            return new Distance(delta.ToDegrees() * 60, UnitsOfMeasure.NauticalMile);

            //Vincenty formula
            //phi_s = latitude_s
            //lamda_s = longitude_s
            //phi_f = latitude_f
            //lamda_f = longitude_f

            //atan2(
            //sqrt(
            //(cos(phi_f)*sin(lamda_s-lamda_f))^2 + (cos(phi_s)*sin(phi_f)-sin(phi_s)*cos(phi_f)*cos(lamda_s-lamda_f))^2
            //)
            //,
            //(sin(phi_s)*sin(phi_f) + cos(phi_s)*cos(phi_f)*cos(lamda_s-lamda_f))
            //)

            //Haversine formula
            //dlon = lon2 - lon1
            //dlat = lat2 - lat1
            //a = sin^2(dlat/2) + cos(lat1) * cos(lat2) * sin^2(dlon/2)
            //c = 2 * arcsin(min(1,sqrt(a)))
            //d = R * c
        }

        public override string ToString()
        {
            if (Elevation != null)
            {
                return String.Format($"({Latitude.ToString("F5")}, {Longitude.ToString("F5")}, {Elevation.Value.ToString("F5")})");
            }
            return String.Format($"({Latitude.ToString("F5")}, {Longitude.ToString("F5")})");
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }

        public bool Equals(Point other)
        {
            return other != null &&
                   Latitude == other.Latitude &&
                   Longitude == other.Longitude &&
                   EqualityComparer<Distance>.Default.Equals(Elevation, other.Elevation);
        }

        public override int GetHashCode()
        {
            var hashCode = 1960202551;
            hashCode = hashCode * -1521134295 + Latitude.GetHashCode();
            hashCode = hashCode * -1521134295 + Longitude.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Distance>.Default.GetHashCode(Elevation);
            return hashCode;
        }

        public static bool operator ==(Point point1, Point point2)
        {
            return EqualityComparer<Point>.Default.Equals(point1, point2);
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return !(point1 == point2);
        }
    }

    public sealed class OrderedPoint : Point, IComparable, IComparable<OrderedPoint>, IComparer, IEquatable<OrderedPoint>
    {
        public OrderedPoint() : base() { }

        public OrderedPoint(double latitude, double longitude, Distance elevation, int order) : base(latitude, longitude, elevation)
        {
            Order = order;
        }

        public OrderedPoint(Point point, int order) : base(point)
        {
            Order = order;
        }

        public int Order { get; set; }

        public int Compare(object x, object y)
        {
            return ((OrderedPoint)x).CompareTo((OrderedPoint)y);
        }

        public int CompareTo(object obj)
        {
            return CompareTo((OrderedPoint)obj);
        }

        public int CompareTo(OrderedPoint other)
        {
            return Order.CompareTo(other.Order);
        }

        public override string ToString()
        {
            return $"{base.ToString()}: {Order}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as OrderedPoint);
        }

        public bool Equals(OrderedPoint other)
        {
            return other != null &&
                   base.Equals(other) &&
                   Order == other.Order;
        }

        public override int GetHashCode()
        {
            var hashCode = 1041847501;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Order.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(OrderedPoint point1, OrderedPoint point2)
        {
            return EqualityComparer<OrderedPoint>.Default.Equals(point1, point2);
        }

        public static bool operator !=(OrderedPoint point1, OrderedPoint point2)
        {
            return !(point1 == point2);
        }
    }
}
