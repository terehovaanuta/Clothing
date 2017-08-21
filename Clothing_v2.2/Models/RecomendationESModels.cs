using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Clothing_v2._2.Models
{
    public class RecomendationESModels
    {
        [Required]
        [Display(Name = "Событие: ")]
        public string Action { get; set; }
      

        [Required]
        [Display(Name = "Размер рубашки и пиджака: ")]
        public string Size_up { get; set; }
        
        [Required]
        [Display(Name = "Размер брюк: ")]
        public string Size_down { get; set; }

        [Required]
        [Display(Name = "Модель костюма: ")]
        public string Model_suit { get; set; }

        [Required]
        [Display(Name = "Цвет костюма: ")]
        public string ColorSuit { get; set; }

        [Required]
        [Display(Name = "Цвет гастука: ")]
        public string ColourTie { get; set; }

        [Required]
        [Display(Name = "Цвет_рубашки: ")]
        public string ColourShirt { get; set; }

    }
}