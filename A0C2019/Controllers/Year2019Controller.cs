using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Domain.Models;
using AdventOfCode.Models.Requests;
using AdventOfCode.Models.Requests._2019;
using AdventOfCode.Models.Responses;
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
        public BaseResponse Day1([FromBody] Day1Request input)
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

            return new BaseResponse(sum1, sum2);
        }

        [HttpPost]
        [Route("2")]
        public BaseResponse Day2([FromBody] Day2Request input)
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

            return new BaseResponse(value1[0], 100 * noun + verb);
        }

        [HttpPost]
        [Route("3")]
        public BaseResponse Day3([FromBody] Day3Request input)
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

            return new BaseResponse(distances.Min(x => x.Value), steps.Min(x => x.Value));
        }

        [HttpPost]
        [Route("4")]
        public BaseResponse Day4([FromBody] Day4Request input)
        {
            var valid = new List<double>();
            var valid2 = new List<double>();

            for (double i = input.Start; i < input.End + 1; i++)
            {
                var numberAsString = i.ToString();
                if (this.CountOfDigitsInNumber(i, '1') > 1 ||
                    this.CountOfDigitsInNumber(i, '2') > 1 ||
                    this.CountOfDigitsInNumber(i, '3') > 1 ||
                    this.CountOfDigitsInNumber(i, '4') > 1 ||
                    this.CountOfDigitsInNumber(i, '5') > 1 ||
                    this.CountOfDigitsInNumber(i, '6') > 1 ||
                    this.CountOfDigitsInNumber(i, '7') > 1 ||
                    this.CountOfDigitsInNumber(i, '8') > 1 ||
                    this.CountOfDigitsInNumber(i, '9') > 1 ||
                    this.CountOfDigitsInNumber(i, '0') > 1)
                {
                    if (int.Parse(numberAsString[0].ToString()) <= int.Parse(numberAsString[1].ToString()) &&
                        int.Parse(numberAsString[1].ToString()) <= int.Parse(numberAsString[2].ToString()) &&
                        int.Parse(numberAsString[2].ToString()) <= int.Parse(numberAsString[3].ToString()) &&
                        int.Parse(numberAsString[3].ToString()) <= int.Parse(numberAsString[4].ToString()) &&
                        int.Parse(numberAsString[4].ToString()) <= int.Parse(numberAsString[5].ToString())) 
                    {
                        valid.Add(i);
                    }
                }
            }

            foreach (var number in valid)
            { 
                if (this.CountOfDigitsInNumber(number, '1') == 2 ||
                    this.CountOfDigitsInNumber(number, '2') == 2 ||
                    this.CountOfDigitsInNumber(number, '3') == 2 ||
                    this.CountOfDigitsInNumber(number, '4') == 2 ||
                    this.CountOfDigitsInNumber(number, '5') == 2 ||
                    this.CountOfDigitsInNumber(number, '6') == 2 ||
                    this.CountOfDigitsInNumber(number, '7') == 2 ||
                    this.CountOfDigitsInNumber(number, '8') == 2 ||
                    this.CountOfDigitsInNumber(number, '9') == 2 ||
                    this.CountOfDigitsInNumber(number, '0') == 2)
                {
                    valid2.Add(number);
                }
            }

            return new BaseResponse(valid.Count(), valid2.Count());
        }

        [HttpPost]
        [Route("5")]
        public BaseResponse Day5([FromBody] Day5Request input)
        {

            return new BaseResponse(0, 0);
        }

        private int CountOfDigitsInNumber(double number, char character)
        {
            var numberAsString = number.ToString();

            return numberAsString.Count(x => x == character);
        }

        private Point GetCoordinateAfterInstruction(char instruction, Point initialCoordinate)
        {
            return instruction switch
            {
                'R' => new Point(initialCoordinate.X + 1, initialCoordinate.Y),
                'L' => new Point(initialCoordinate.X - 1, initialCoordinate.Y),
                'U' => new Point(initialCoordinate.X, initialCoordinate.Y + 1),
                'D' => new Point(initialCoordinate.X, initialCoordinate.Y - 1),
                _ => throw new Exception("something went very wrong"),
            };
        }

        private void IntCode(int[] input, int position1Replacement, int position2Replacement)
        {
            input[1] = position1Replacement;
            input[2] = position2Replacement;


        }
    }
}