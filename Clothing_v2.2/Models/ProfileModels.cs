using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Clothing_v2._2.Models
{
    public class ProfileModels
    {
        [Required]
        [Display(Name = "Рост: ")]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number")]
        public float Growth { get; set; }

        [Required]
        [Display(Name = "Вес: ")]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number")]
        public float Weight { get; set; }

        [Required]
        [Display(Name = "Обхват груди: ")]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number")]
        public float Bust { get; set; }

        [Required]
        [Display(Name = "Обхват талии: ")]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number")]
        public float Waist { get; set; }

        [Required]
        [Display(Name = "Обхват бедер: ")]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number")]
        public float Hip { get; set; }

        [Required]
        [Display(Name = "Размер обуви: ")]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number")]
        public float Shoes_size { get; set; }

        [Required]
        [Display(Name = "Дата: ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd")]
        public DateTime Data { get; set; }

        [Required]
        [Display(Name = "Тип кожи: ")]
        public string SkinColour { get; set; }

        [Required]
        [Display(Name = "Тип волос: ")]
        public string HairColour { get; set; }

    }
}