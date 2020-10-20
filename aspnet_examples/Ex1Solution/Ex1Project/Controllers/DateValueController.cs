using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ex1Project.Models;

namespace Ex1Project.Controllers
{
    public class DateValueController : Controller
    {
        [HttpGet]
        public IActionResult Example()
        {
            return View();
        }

        [HttpPost]
        //public IActionResult Example(string name, DateTime? start_date, DateTime? end_date, int? count)
        public IActionResult Example(Account account)
        {
            if(ModelState.IsValid)
            {
                Debug.WriteLine(account);
                Debug.WriteLine("Model is OK!");
                //Debug.WriteLine($"name={name}, start_date={start_date}, end_date={end_date}, count={count}");

                List<int> list = new List<int>();
                for(int i = 0; i < account.Count; i++)
                {
                    Debug.WriteLine("append i = " + i);
                    list.Add(i);
                }
                account.CountList = list;
                Debug.WriteLine(account.CountList.Count());

                return View("Example",account);
            }
            else
            {
                Debug.WriteLine("Model is INVALID");
                return View("Example",account);
            }
        }
    }

}