using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Domain.Models;
using AdventOfCode.Models.Requests;
using AdventOfCode.Models.Requests._2019;
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

        [HttpPost]
        [Route("2")]
        public Day2Response Day2([FromBody] Day2Request input)
        {
            var value1 = new int[input.Input.Length];
            Array.Copy(input.Input, value1, value1.Length);
            this.IntCode(value1, 12, 2);
            
            var noun = -1;
            var verb = 0;
            var output = 0;

            while(output != 19690720)
            {
                if (noun == 99)
                {
                    verb++;
                    noun = 0;
                }
                else
                {
                    noun++;
                }

                var value2 = new int[input.Input.Length];
                Array.Copy(input.Input, value2, value2.Length);

                this.IntCode(value2, noun, verb);

                output = value2[0];
            }

            return new Day2Response(value1[0], 100 * noun + verb);
        }

        [HttpPost]
        [Route("3")]
        public Day3Response Day3([FromBody] Day3Request input)
        {
            var firstLine = new List<Point> { new Point(0, 0) };

            foreach (var instruction in input.FirstLine)
            {
                var direction = instruction[0];
                var amountOfSteps = int.Parse(instruction[1..]);

                for (int i = 0; i < amountOfSteps; i++)
                {
                    firstLine.Add(this.GetCoordinateAfterInstruction(direction, firstLine.Last()));
                }
            }

            var secondLine = new List<Point> { new Point(0, 0) };

            foreach (var instruction in input.SecondLine)
            {
                var direction = instruction[0];
                var amountOfSteps = int.Parse(instruction[1..]);

                for (int i = 0; i < amountOfSteps; i++)
                {
                    secondLine.Add(this.GetCoordinateAfterInstruction(direction, secondLine.Last()));
                }
            }            
            var intersects = firstLine.Intersect(secondLine);

            Dictionary<Point, double> distances = new Dictionary<Point, double>();

            foreach (var item in intersects)
            {
                if (item.X != 0 && item.Y != 0)
                {
                    distances.Add(item, Math.Abs(0 - item.X) + Math.Abs(0 - item.Y));
                }
            }

            Dictionary<Point, double> steps = new Dictionary<Point, double>();
            foreach (var item in intersects)
            {
                if (item.X != 0 && item.Y != 0)
                {
                    var index1 = firstLine.IndexOf(item);
                    var index2 = secondLine.IndexOf(item);

                    steps.Add(item, index1 + index2);
                }
            }

            return new Day3Response(distances.Min(x => x.Value), steps.Min(x => x.Value));
        }

        private Point GetCoordinateAfterInstruction(char instruction, Point initialCoordinate)
        {            
            switch (instruction)
            {
                case 'R':
                    return new Point(initialCoordinate.X +1 , initialCoordinate.Y);
                case 'L':
                    return new Point(initialCoordinate.X - 1, initialCoordinate.Y);
                case 'U':
                    return new Point(initialCoordinate.X, initialCoordinate.Y + 1);
                case 'D':
                    return new Point(initialCoordinate.X, initialCoordinate.Y - 1);
                default: throw new Exception("something went very wrong");
            }
        }

        private void IntCode(int[] input, int position1Replacement, int position2Replacement)
        {
            input[1] = position1Replacement;
            input[2] = position2Replacement;

            for (int i = 0; i < input.Length; i += 4)
            {
                var instruction = input[i];

                if (instruction == 99)
                {
                    break;
                }

                var firstIndex = input[i + 1];
                var secondIndex = input[i + 2];
                var thirdIndex = input[i + 3];

                if (instruction == 1)
                {
                    input[thirdIndex] = input[firstIndex] + input[secondIndex];
                }
                else if (instruction == 2)
                {
                    input[thirdIndex] = input[firstIndex] * input[secondIndex];
                }
                else
                {
                    throw new Exception("something went horrifically wrong");
                }
            }
        }
    }
}