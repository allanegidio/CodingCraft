using Estamparia.MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace Estamparia.MVC.ViewModels
{
    public class LayoutViewModel
    {
        [Display(Name = "Layout")]
        public Layout LayoutName { get; set; }
    }
}