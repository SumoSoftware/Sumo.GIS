﻿using System;
using System.Collections.Generic;

namespace Sumo.GIS.Geometry
{
    public partial class PointCollection : FigureCollection<Point>, IFigure
    {
        public PointCollection()
        {
        }

        public PointCollection(IEnumerable<Point> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException(nameof(points));
            }

            AddRange(points);
        }

        //todo: implement GetExtent
        public override Rectangle GetExtent()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"[{String.Join(",", this)}]";
        }
    }
}
