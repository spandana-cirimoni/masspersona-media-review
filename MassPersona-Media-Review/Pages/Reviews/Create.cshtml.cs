using MassPersonaMediaReview.DAL;
using MassPersonaMediaReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MassPersona_Media_Review.Pages.Reviews
{
    public class CreateModel : PageModel
    {
        private readonly MyAppDBContext _context;
        public CreateModel(MyAppDBContext context){
            _context = context;
        }
        public void OnGet(){
            CategoryList = new SelectList(Enum.GetValues(typeof(Category)));
        }
        [BindProperty]
        public Review Reviews{ get; set; } = new Review();
        public SelectList? CategoryList { get; set; }

        public async Task<IActionResult> OnPostAsync(){
            CategoryList = new SelectList(Enum.GetValues(typeof(Category)));
            if(!ModelState.IsValid || _context.Reviews == null || Reviews == null){
                return Page();
            }
            Reviews.DateReviewed = DateTime.UtcNow;
            _context.Reviews.Add(Reviews);
            await _context.SaveChangesAsync();
            return RedirectToPage("Home");
        }
    }
}
