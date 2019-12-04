using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Models.Responses._2019
{
    public class Day4Response : BaseResponse
    {
        public Day4Response(double answer1, double answer2)
        {
            this.Answer1 = answer1.ToString();
            this.Answer2 = answer2.ToString();
        }
    }
}
