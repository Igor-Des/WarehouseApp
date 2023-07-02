using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace WarehouseApp.Models
{
    public class Component
    {
        [Key]
        [Display(Name = "Код")]
        public int ComponentId { get; set; }

        [Display(Name = "Код поставщика")]
        [ForeignKey("Supplier")]
        [Required(ErrorMessage = "Не указан поставщик")]
        public int SupplierId { get; set; }

        [Display(Name = "Код комплектующего")]
        [ForeignKey("TypeComponent")]
        [Required(ErrorMessage = "Не указан тип товара")]
        public int TypeComponentId { get; set; }

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


        public TypeComponent TypeComponent { get; set; }
        public Supplier Supplier { get; set; }

        public override string ToString()
        {
            return ComponentId + " " + SupplierId + " " + TypeComponentId + " " + Price + " " + Amount + " " + Date;
        }

    }
}
