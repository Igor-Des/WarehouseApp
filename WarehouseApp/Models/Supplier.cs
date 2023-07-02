using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


namespace WarehouseApp.Models
{
    public class Supplier
    {
        [Key]
        [Display(Name = "Код")]
        public int SupplierId { get; set; }

        [Display(Name = "Поставщик")]
        [Required(ErrorMessage = "Не указан поставщик")]
        public string Name { get; set; }

        public ICollection<Component> Components { get; set; }

        public Supplier()
        {
            Components = new List<Component>();
        }

        public override string ToString()
        {
            return SupplierId + " " + Name;
        }

    }
}
