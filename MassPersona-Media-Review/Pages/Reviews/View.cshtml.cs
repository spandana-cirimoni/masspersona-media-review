using MassPersonaMediaReview.DAL;
using MassPersonaMediaReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MassPersona_Media_Review.Pages.Reviews
{
    public class ViewModel : PageModel
    {
        public readonly MyAppDBContext _context;

        public ViewModel(MyAppDBContext context){
            _context = context;
        }
        public Review Reviews{ get; set;} 
        public async Task<IActionResult> OnGetAsync(int? id){
            if(id == null || _context.Reviews == null){
                return RedirectToPage("/NotFound");
            }
            var review = await _context.Reviews.FirstOrDefaultAsync(review => review.Id == id);
            if(review == null){
                return RedirectToPage("/NotFound");
            }
            Reviews = review;
            return Page();
        }
    }
}
