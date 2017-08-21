using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Clothing_v2._2.Models
{
    public class QuestionsModels
    {
        [Required]
        [Display(Name = "Какой повод: я хочу с выбором ответа сделать. можно?")]
        public string Select_action { get; set; }

        public IEnumerable<SelectListItem> Actions
        {
            get
            {
                return new[]
                {
                    new SelectListItem { Text = "Свадьба", Value = "1" },
                    new SelectListItem { Text = "Банкет", Value = "2" },
                    new SelectListItem { Text = "Менее официальное мероприятие", Value = "3" },
                    new SelectListItem { Text = "Торжественный случай", Value = "4" },
                    new SelectListItem { Text = "Ежедневный", Value = "5" },
                    new SelectListItem { Text = "Деловой", Value = "6" },
                    new SelectListItem { Text = "Свидание", Value = "7" }
                };
            }
        }
    }
}