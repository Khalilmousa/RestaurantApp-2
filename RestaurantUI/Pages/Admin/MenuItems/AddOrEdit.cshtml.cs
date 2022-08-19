using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using Restaurant.DAL;
using Restaurant.DAL.Interfaces;
using Restaurant.Models;
using Restaurant.Models.ViewModels;

namespace RestaurantUI.Pages.Admin.MenuItems
{
    public class AddOrEditModel : PageModel
    {
        [BindProperty]
        public MenuItemViewModel MenuItem { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToastNotification _notify;
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
        public AddOrEditModel(IUnitOfWork unitOfWork, IToastNotification notify)
        {
            _unitOfWork = unitOfWork;
            _notify = notify;
            MenuItem = new MenuItemViewModel();
        }
        // get handler 
        public async Task OnGet()
        {
            CategoryList = (await _unitOfWork.CategoryRepo.GetAll()).Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            FoodTypeList = (await _unitOfWork.FoodTypeRepo.GetAll()).Select(c => new SelectListItem()
            {
                Text = c.FoodTypeName,
                Value = c.FoodTypeId.ToString()
            });
        }

        public async Task<IActionResult> OnPost()
        {
            // Create operation
            if (MenuItem.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    MenuItem mItem = new MenuItem()
                    {
                        Name = MenuItem.Name,
                        Description = MenuItem.Description,
                        ImageUrl = MenuItem.ImageUrl,
                        Price = MenuItem.Price,
                        FoodTypeId = MenuItem.FoodTypeId,
                        CategoryId = MenuItem.CategoryId,
                    };
                    await _unitOfWork.MenuItemRepo.Add(mItem);
                    bool res = await _unitOfWork.Save();
                    if (res)
                    {
                        _notify.AddSuccessToastMessage("Menu Item Created suuccufully ");
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        _notify.AddErrorToastMessage("Menu Item Not Created suuccufully !!!!");
                    }
                }
                else
                {
                    return Page();
                }

            }
            return Page();
        }

    }
}
