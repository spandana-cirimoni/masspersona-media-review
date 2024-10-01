using MassPersonaMediaReview.DAL;
using MassPersonaMediaReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MassPersona_Media_Review.Pages.Reviews
{
    public class EditModel : PageModel
    {
        private readonly MyAppDBContext _context;

        public EditModel(MyAppDBContext context){
            _context = context;
        }
        [BindProperty]
        public Review Reviews { get; set; }
        public SelectList CategoryList { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        { 
            
            if(id == null || _context.Reviews == null){
                return NotFound();
            }
            var review = await _context.Reviews.FirstOrDefaultAsync(review => review.Id == id);
            if(review == null){
                return NotFound();
            }
            CategoryList = new SelectList(Enum.GetValues(typeof(Category)));
            Reviews = review;
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(){
            CategoryList = new SelectList(Enum.GetValues(typeof(Category)));
            if(!ModelState.IsValid){
                return Page();
            }
                
            Reviews.DateReviewed = DateTime.UtcNow;
            _context.Reviews.Update(Reviews);
            await _context.SaveChangesAsync();
            return RedirectToPage("Home");
        }
    }
}
