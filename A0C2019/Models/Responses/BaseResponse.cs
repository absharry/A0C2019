using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Models.Responses
{
    public class BaseResponse
    {
        public BaseResponse(double answer1, double answer2)
        {
            this.Answer1 = answer1;
            this.Answer2 = answer2;
        }

        public BaseResponse(int answer1, int answer2) 
            : this((double)answer1, (double) answer2)
        {
        }

        public double Answer1 { get; set; }

        public double Answer2 { get; set; }
    }
}
