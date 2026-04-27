using KurumsalWeb.Models.Model;
using System.Collections.Generic;

namespace KurumsalWeb.Models.ViewModel
{
    public class HomePageViewModel
    {
        public Identity Identity { get; set; }
        public AboutUs AboutUs { get; set; }
        public Contact Contact { get; set; }
        public IList<Slider> Sliders { get; set; }
        public IList<Service> Services { get; set; }
        public IList<Blog> LatestBlogs { get; set; }
    }
}
