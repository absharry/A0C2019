using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Domain.Models
{
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; private set; }

        public int Y { get; private set; }

        public bool Equals(Coordinate a, Coordinate b)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(a, b)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(a, null) || Object.ReferenceEquals(b, null))
                return false;

            //Check whether the products' properties are equal.
            return a.X == b.X && a.Y == b.Y;
        }
    }    
}
