using KurumsalWeb.Models.Model;
using System.Collections.Generic;

namespace KurumsalWeb.Models.ViewModel
{
    public class AdminDashboardViewModel
    {
        public int CategoryCount { get; set; }
        public int BlogCount { get; set; }
        public int ServiceCount { get; set; }
        public int SliderCount { get; set; }
        public int ContactCount { get; set; }
        public int AdminCount { get; set; }
        public IList<Blog> LatestBlogs { get; set; }
        public IList<Service> LatestServices { get; set; }
    }
}
