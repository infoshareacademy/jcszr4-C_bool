using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Enums;
using Castle.Core.Internal;

namespace C_bool.WebApp.Controllers
{
    public class ValueValidatorController : Controller
    {
        /// <summary>
        /// Validation of "Valid from" form input in game task create view, used primarily in GameTaskEditModel
        /// </summary>
        /// <param name="validFrom"></param>
        /// <param name="validThru"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult IsValid_ValidFromDate(string validFrom, string validThru)
        {
            var validFromMin = DateTime.Now;
            var validFromMax = DateTime.Now.AddYears(1);
            var message = "Wprowadź prawidłową datę";
            try
            {
                DateTime.TryParse(validFrom, out DateTime validFromDate);
                DateTime.TryParse(validThru, out DateTime validThruDate);
                if (validFrom.IsNullOrEmpty())
                {
                    message = "Musisz wprowadzić datę początkową";
                    return Json(message);
                }
                if (validFromDate >= validThruDate && !validThru.IsNullOrEmpty())
                {
                    message = "Data początkowa nie może być większa od końcowej";
                    return Json(message);
                }
                if (validFromDate < validFromMin || validFromDate > validFromMax)
                {
                    message = "Zadanie może mieć datę <b>początkową</b> od dzisiaj do nie dalej jak rok w przód";
                    return Json(message);
                }
                else
                {
                    return Json(true);
                }
            }
            catch (Exception)
            {
                return Json(message);
            }
        }

        /// <summary>
        /// Validation of "Valid thru" form input in game task create view, used primarily in GameTaskEditModel
        /// </summary>
        /// <param name="validFrom"></param>
        /// <param name="validThru"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult IsValid_ValidThruDate(string validFrom, string validThru)
        {
            var message = "Wprowadź prawidłową datę, data nie może być mniejsza niż data początkowa";
            var minimumTimeBetweenFromAndThru = TimeSpan.FromMinutes(30);
            try
            {
                DateTime.TryParse(validFrom, out DateTime validFromDate);
                DateTime.TryParse(validThru, out DateTime validThruDate);
                if (validFrom.IsNullOrEmpty() || validThru.IsNullOrEmpty())
                {
                    message = "Musisz wprowadzić obydwie wartości, zarówno datę początkową jak i końcową";
                    return Json(message);
                }
                if (validFromDate >= validThruDate && !validFrom.IsNullOrEmpty())
                {
                    message = "Data końcowa nie może być mniejsza od początkowej";
                    return Json(message);
                }
                if (validThruDate - validFromDate < minimumTimeBetweenFromAndThru)
                {
                    message = $"Daj graczowi minimum {minimumTimeBetweenFromAndThru.TotalHours} godzin na zaznajomienie się z zadaniem";
                    return Json(message);
                }
                return Json(true);
            }
            catch (Exception)
            {
                return Json(message);
            }
        }

        [HttpPost]
        public JsonResult IsValid_TextCriterion(string type, string textCriterion)
        {
            Enum.TryParse(type, true, out TaskType typeEnum);
            var message = "Ustaw swoje hasło do zaliczenia zadania";
            if (typeEnum == TaskType.TextEntry)
            {
                if (textCriterion.IsNullOrEmpty())
                {
                    message = "Hasło nie może być puste";
                    return Json(message);
                }
                else
                {
                    return Json(true);
                }
            }

            return Json(true);
        }
    }
}
