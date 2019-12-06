using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Domain.Models
{
    public class PlanateryBody
    {
        public PlanateryBody(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public PlanateryBody OrbitingAround { get; set; }

        public int TotalOrbits(int countSoFar)
        {
            if (this.OrbitingAround == null)
            {
                return countSoFar;
            }

            countSoFar++;
            return this.OrbitingAround.TotalOrbits(countSoFar);
        }

        public List<PlanateryBody> GetAllPlanetsOrbiting(List<PlanateryBody> planetSoFar)
        {
            if (this.OrbitingAround == null)
            {
                return planetSoFar;
            }

            planetSoFar.Add(this.OrbitingAround);
            return this.OrbitingAround.GetAllPlanetsOrbiting(planetSoFar);
        }
    }
}
