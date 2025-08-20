using System.ComponentModel.DataAnnotations;

namespace WebShopApi.Models
{
    public class BaseViewModel
    {
        public List<Category> Categories { get; set; }

        public BaseViewModel()
        {
            Categories = DAO.GetTypesInfo().Result;
        }
    }

    public class Category
    {
        [Key]
        public ProductType ProductType { get; set; }
    }
}
