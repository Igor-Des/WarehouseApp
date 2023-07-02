using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;

namespace WarehouseApp.Models
{
    public class TypeComponent
    {
        [Key]
        [Display(Name = "Код")]
        public int TypeComponentId { get; set; }

        [Display(Name = "Тип товара")]
        [Required(ErrorMessage = "Введите корректное название")]
        public string Name { get; set; }

        public ICollection<Component> Components { get; set; }

        public TypeComponent()
        {
            Components = new List<Component>();
        }

        public override string ToString()
        {
            return TypeComponentId + " " + Name;
        }
    }
}
