using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Domain.Models
{
    public class Instruction
    {
        public Instruction(int[] instruction, int[] fullProgram)
        {
            var fullOpCode = instruction[0];
            var firstParamMode = 0;
            var secondParamMode = 0;
            var thirdParamMode = 0;

            if (fullOpCode > 99 )
            {
                var fullOpCodeString = fullOpCode.ToString();
                this.OpCode = int.Parse(fullOpCodeString.Substring(fullOpCodeString.Length - 2, 2));

                firstParamMode = int.Parse(fullOpCodeString.Substring(fullOpCodeString.Length - 3, 1));

                if(fullOpCodeString.Length > 3)
                {
                    secondParamMode = int.Parse(fullOpCodeString.Substring(fullOpCodeString.Length - 4, 1));
                }

                if(fullOpCodeString.Length > 4)
                {
                    thirdParamMode = int.Parse(fullOpCodeString.Substring(fullOpCodeString.Length - 5, 1));
                }
            }
            else
            {
                this.OpCode = fullOpCode;
            }

            if (instruction.Length == 2)
            {
                this.ToIndex = instruction[1];
            }

            if (instruction.Length == 3)
            {
                this.ToIndex = instruction[1];
                this.FirstValue = instruction[2];
            }

            if (instruction.Length > 3)
            {
                this.FirstValue = firstParamMode == 0 ? fullProgram[instruction[1]] : instruction[1];
                this.SecondValue = secondParamMode == 0 ? fullProgram[instruction[2]] : instruction[2];

                if (thirdParamMode == 1)
                {
                    Console.WriteLine("I'm not sure how this would work");
                }

                this.ToIndex = instruction[3];
            }
        }

        public int OpCode { get; private set; }

        public int FirstValue { get; private set; }

        public int SecondValue { get; private set; }

        public int? ToIndex { get; private set; }

        public int? Run(int[] fullProgram)
        {
            switch(this.OpCode)
            {
                case 1:
                    fullProgram[this.ToIndex.Value] = this.FirstValue + this.SecondValue;
                    break;
                case 2:
                    fullProgram[this.ToIndex.Value] = this.FirstValue * this.SecondValue;
                    break;
                case 3:
                    fullProgram[this.ToIndex.Value] = this.FirstValue;
                    break;
                case 4:
                    return this.ToIndex;
                default:
                    throw new Exception("something went horrifically wrong");
            }

            return null;
        }
    }
}
