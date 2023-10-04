using Microsoft.AspNetCore.Mvc;

namespace Dojodachi.Controllers
{
    public class Dojodashi : Controller
    {
        [HttpGet]
        [Route("dojodashi")]
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("Fullness", 20);
            HttpContext.Session.SetInt32("Happiness", 20);
            HttpContext.Session.SetInt32("Meals", 3);
            HttpContext.Session.SetInt32("Energy", 50);
            HttpContext.Session.SetString("Message", "Welcome to the Dojodashi");
            return View();
        }

        [HttpGet]
        [Route("")]
        public IActionResult RedirectToDojoDashi()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("operation")]
        public IActionResult Operation(int operationId)
        {
            int? fullness = HttpContext.Session.GetInt32("Fullness");
            int? happiness = HttpContext.Session.GetInt32("Happiness");
            int? meals = HttpContext.Session.GetInt32("Meals");
            int? energy = HttpContext.Session.GetInt32("Energy");
            string? message = HttpContext.Session.GetString("Message");
            if (
                fullness != null
                && happiness != null
                && meals != null
                && energy != null
                && message != null
            )
            {
                Random rand = new Random();
                switch (operationId)
                { //Feed
                    case 1:
                        if (meals != 0)
                        {
                            meals--;
                            int randvalue = rand.Next(5, 11);
                            fullness = fullness + randvalue;
                            message =
                                $"I fed my Dojodachi and it regained fullness +{randvalue}, meals -1";
                        }
                        break;
                    //Play
                    case 2:
                        if (energy != 0 && energy > 5)
                        {
                            energy = energy - 5;
                            int randvalue = rand.Next(5, 11);
                            happiness = happiness + randvalue;
                            message =
                                $"You played with you Dojodachi! Happiness +{randvalue}, Enegy -5";
                        }
                        break;
                    //Work
                    case 3:
                        if (energy != 0 && energy > 0)
                        {
                            energy = energy - 5;
                            int randvalue = rand.Next(1, 4);
                            meals = meals + randvalue;
                            message = $"My Dojodachi worked. Energy -5, Meals +{randvalue}";
                        }
                        break;
                    //Sleep
                    case 4:
                        energy = energy + 15;
                        fullness = fullness - 5;
                        happiness = happiness - 5;
                        message =
                            $"My Dojodachi went to sleep. Energy +15, fullness -5, happiness -5";
                        break;
                    //Restart
                    case 5:
                        fullness = 20;
                        happiness = 20;
                        meals = 3;
                        energy = 50;
                        break;
                }

                if (fullness < 0 || happiness < 0)
                {
                    message = "Your dojodachi has passed away";
                }
                else if (energy > 100 && happiness > 100 && fullness > 100)
                {
                    message = "Congratulation! You Won!";
                }

                HttpContext.Session.SetInt32("Fullness", (int)fullness);
                HttpContext.Session.SetInt32("Happiness", (int)happiness);
                HttpContext.Session.SetInt32("Meals", (int)meals);
                HttpContext.Session.SetInt32("Energy", (int)energy);
                HttpContext.Session.SetString("Message", message);

                return View("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
