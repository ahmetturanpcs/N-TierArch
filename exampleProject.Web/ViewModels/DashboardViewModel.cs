using exampleProject.Core.Entities;
namespace exampleProject.Web.ViewModels
{
    public class DashboardViewModel
    {
        // En üstte listelenecek kategoriler
        public List<Category> Categories { get; set; }
        public List<Store> Stores { get; set; }

        // AdminLTE kutucuklarında (Small Boxes) gösterebileceğimiz istatistikler
        public int TotalCategoryCount { get; set; }
        public int ActiveCategoryCount { get; set; }
        public int TotalStoreCount { get; set; }
        public int ActiveStoreCount { get; set; }
    }
}
