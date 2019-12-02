using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Models.Requests;
using AdventOfCode.Models.Responses._2019;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdventOfCode.Controllers
{
    [Produces("application/json")]
    [Route("api/2019")]
    [ApiController]
    public class Year2019Controller : ControllerBase
    {
        public Year2019Controller()
        {

        }

        [HttpPost]
        [Route("1")]
        public Day1Response Day1([FromBody] Day1Request input)
        {
            var sum1 = input.Input.Sum(x => Math.Round(double.Parse(x) / 3, MidpointRounding.ToZero) - 2);

            double sum2 = 0;

            foreach (var module in input.Input)
            {
                var fuelRequirement = Math.Round(double.Parse(module) / 3, MidpointRounding.ToZero) - 2;

                while (fuelRequirement > 5)
                {
                    sum2 += fuelRequirement;

                    fuelRequirement = Math.Round(fuelRequirement / 3, MidpointRounding.ToZero) - 2;
                }

                sum2 += fuelRequirement;
            }

            return new Day1Response(sum1, sum2);
        }
    }
}