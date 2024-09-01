using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace web_api_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PS : ControllerBase
    {

        [HttpGet("math")]
        public IActionResult Math(string input)
        {
            var x = input.Split(' ');

            var num1 = Convert.ToDouble(x[0]);
            var op = x[1];
            var num2 = Convert.ToDouble(x[2]);

            double result = 0;

            switch (op)
            {
                case ("+"):
                    result = num1 + num2;
                    break;
                case ("-"):
                    result = num1 - num2;
                    break;
                case ("*"):
                    result = num1 * num2;
                    break;
                case ("/"):
                    if (num2 == 0)
                    {
                        return BadRequest("can't devide on zero");
                    }
                    else
                    {
                        result = num1 / num2;
                        break;
                    }
            }
            return Ok(result);
        }


        [HttpGet("{num1}-{num2}")]
        public IActionResult isIt30(int num1, int num2)
        {
            bool checkOne = (num1 == 30 || num2 == 30);
            bool checkSum = (num1 + num2 == 30);

            bool result = (checkOne || checkSum);

            return Ok(result);
        }

        [HttpGet("{num}")]
        public IActionResult isIt3or7(int num)
        {
            if (num < 0)
            {
                return BadRequest("please enter a positive number");
            }
            else
            {
                bool result = (num % 3 == 0 || num % 7 == 0);
                return Ok(result);
            }

        }






    }
}
