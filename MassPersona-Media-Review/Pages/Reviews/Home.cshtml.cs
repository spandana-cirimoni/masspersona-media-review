using MassPersonaMediaReview.DAL;
using MassPersonaMediaReview.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MassPersonaMediaReview.Pages.Reviews
{
    public class HomeModel : PageModel
    {
        private readonly MyAppDBContext _context;

        public HomeModel(MyAppDBContext context){
            _context = context;
        }
        public List<Review> Reviews { get; set; } = new List<Review>();

        //Filter Variables
        [BindProperty(SupportsGet = true)]
        public int? FilterRating { get; set; }

        [BindProperty(SupportsGet = true)]
        public Category? FilterCategory { get; set; }
        public SelectList CategoryList { get; set; }

        //Sorting Variable
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        // Pagination properties
        [BindProperty(SupportsGet = true)] // Added this attribute
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 5; // Number of reviews per page

        public async Task OnGetAsync()
        {
            CategoryList = new SelectList(Enum.GetValues(typeof(Category)));

            IQueryable<Review> query = _context.Reviews;

            // Apply Rating filter if selected
            if (FilterRating.HasValue)
            {
                query = query.Where(r => r.Rating == FilterRating.Value);
            }

            // Apply Category filter if selected
            if (FilterCategory.HasValue)
            {
                query = query.Where(r => r.Category == FilterCategory.Value);
            }

            // Sort the order
            switch (SortOrder)
            {
            case "title":
                query = query.OrderBy(r => r.Title.ToLower());
                break;
            case "title_desc":
                query = query.OrderByDescending(r => r.Title.ToLower());
                break;
            case "category":
                query = query.OrderBy(r => r.Category);
                break;
            case "category_desc":
                query = query.OrderByDescending(r => r.Category);
                break;
            case "rating":
                query = query.OrderBy(r => r.Rating);
                break;
            case "rating_desc":
                query = query.OrderByDescending(r => r.Rating);
                break;
            case "date":
                query = query.OrderBy(r => r.DateReviewed);
                break;
            case "date_desc":
                query = query.OrderByDescending(r => r.DateReviewed);
                break;
            default:
                query = query.OrderBy(r => r.Id);
                break;
            }

            // Get total count for pagination
            int totalCount = await query.CountAsync();

            // Calculate total pages
            TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
            TotalPages = TotalPages < 1 ? 1 : TotalPages;

            // Ensure CurrentPage is within valid range
            CurrentPage = CurrentPage < 1 ? 1 : CurrentPage;
            CurrentPage = CurrentPage > TotalPages ? TotalPages : CurrentPage;

            
            if(query!=null){
                Reviews = await query
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
            }
        }
    }
}
