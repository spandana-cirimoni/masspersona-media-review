using MassPersonaMediaReview.DAL;
using MassPersonaMediaReview.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MassPersonaMediaReview.Pages.Reviews
{
    public class HomeModel : PageModel
    {
        private readonly MyAppDBContext _context;

        public HomeModel(MyAppDBContext context){
            _context = context;
        }
        public List<Review> Reviews{ get; set; }
        public async Task OnGetAsync()
        {
            if(_context.Reviews!=null){
                Reviews = await _context.Reviews.ToListAsync();
            }
        }
    }
}
