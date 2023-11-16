using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Album.Models;
using Album.MyContext;

namespace Product_Razor_2__EF_IF_.Pages_Blog
{
    public class IndexModel : PageModel
    {
        private readonly Album.MyContext.MyBlogContext _context;

        public IndexModel(Album.MyContext.MyBlogContext context)
        {
            _context = context;
        }


        public IList<Article> Article { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString {set;get;}

        [BindProperty(SupportsGet = true)]
        public int CurrentPage {set;get;}
        public int CountPage {set;get;}

        public int Number_Item_Per_Page = 10;
        public async Task OnGetAsync()
        {

            var articles =  await (from a in _context.Article select a).ToListAsync();
            int totalPage = articles.Count;
            
            CountPage = (int)Math.Ceiling((double)totalPage/Number_Item_Per_Page);
            
            if(CurrentPage < 1) {
                CurrentPage = 1;
            }
            if(CurrentPage > CountPage) {
                CurrentPage = CountPage;
            }
            
            if (_context.Article != null)
            {
                if(!string.IsNullOrEmpty(SearchString)){
                    Article = articles.Where( a => a.Title.Contains(SearchString)).ToList();
                }  else {
                    Article = articles.Skip(Number_Item_Per_Page*(CurrentPage -1)).Take(Number_Item_Per_Page).ToList();
                }
                
            }
        }
    }
}
