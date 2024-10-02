using MassPersonaMediaReview.DAL;
using MassPersonaMediaReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MassPersona_Media_Review.Pages.Reviews
{
    public class DeleteModel : PageModel
    {
        private readonly MyAppDBContext _context;
        public DeleteModel(MyAppDBContext context){
            _context = context;
        }
        [BindProperty]
        public Review Reviews { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null){
                return NotFound();
            }
            var review = await _context.Reviews.FirstOrDefaultAsync(review => review.Id == id);
            if(review == null){
                return NotFound();
            }
            Reviews = review;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id){
            
            if(id == null){
                return RedirectToPage("/NotFound");
            }
            var review = await _context.Reviews.FindAsync(id);

            if(review == null){
                return RedirectToPage("/NotFound");
            }
                
            Reviews = review;
            _context.Remove(Reviews);
            await _context.SaveChangesAsync();
            return RedirectToPage("Home");
        }
    }
}
