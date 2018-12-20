﻿using Sumo.Geo.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sumo.Geo.Geometries
{
    /// <summary>
    /// first and last point must be identical so if they aren't we add a point to close the polygon
    /// </summary>
    public class Polygon : Path, IRegion
    {
        public Polygon() : base() { }

        public Polygon(IEnumerable<Point> points) : base(points)
        {
            if (points.Count() < 3)
            {
                throw new ArgumentOutOfRangeException(nameof(points));
            }

            if (this[0] != this[Count - 1])
            {
                Add(new Point(this[0]));
            }
        }

        public virtual Area GetArea()
        {
            throw new NotImplementedException();
        }
    }
}
