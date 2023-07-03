using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace WarehouseApp.ViewModels
{
    public class ComponentViewModel
    {
        public int ComponentId { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Не указано название")]
        public string Name { get; set; }

        [Display(Name = "Поставщик")]
        [Required(ErrorMessage = "Не указан поставщик")]
        public string SupplierName { get; set; }

        [Display(Name = "Тип товара")]
        [Required(ErrorMessage = "Не указан тип товара")]
        public string TypeComponentName { get; set; }

        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Не указана цена")]
        [Range(1, 10000, ErrorMessage = "Некорректное значение цены товара")]
        public int Price { get; set; }

        [Display(Name = "Количество")]
        [Required(ErrorMessage = "Не указано количество товара")]
        [Range(1, 10000, ErrorMessage = "Некорректное значение товара")]
        public int Amount { get; set; }

        [Display(Name = "Дата поступления")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Не указана дата поступления")]
        public DateTime Date { get; set; }
    }
}
