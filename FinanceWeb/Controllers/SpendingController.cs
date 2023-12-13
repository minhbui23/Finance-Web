using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Finance.DataAccess.DBContext;
using Finance.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Finance_Web.Controllers
{
    public class SpendingController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager; 
        public SpendingController(ApplicationDBContext db, UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager){
            _db=db;
            _userManager = userManager;
            _signManager = signInManager;
        }
        
        public async Task<IActionResult> Index()
        {
            if (_signManager.IsSignedIn(User))
            {
                // Step 1: Retrieve the logged-in user
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    // Step 2: Retrieve the user with active wallet and related spendings
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                    var userWithActiveWallet = _userManager.Users
                        .Include(u => u.Wallets)
                        .ThenInclude(w => w.Spendings)
                        .SingleOrDefault(u => u.Id == user.Id && u.ActiveWalletId != null);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

                    if (userWithActiveWallet != null)
                    {
                        // Step 3: Retrieve spendings related to the active wallet
                        var spendingsForActiveWallet = userWithActiveWallet.ActiveWallet.Spendings.ToList();

                        return View(spendingsForActiveWallet);
                    }
                }
            }

            
            return View();
        }

        //when click Create in Index page
        public IActionResult Create(){
            return View();
        }

        //When Click Create in Create Page, a Spending object will be included in the Post method 
        [HttpPost]
        public async Task<IActionResult> Create(Spending spending)
        {
            if (ModelState.IsValid)
            {
                // Step 1: Retrieve the logged-in user's ID
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    // Step 2: Fetch the user's ActiveWalletID based on UserID
                    var userWithActiveWallet = _userManager.Users
                        .Include(u => u.Wallets)
                        .SingleOrDefault(u => u.Id == user.Id);

                    if (userWithActiveWallet != null && userWithActiveWallet.ActiveWalletId.HasValue)
                    {
                        // Step 3: Set the IdWallet property of the Spending object to the retrieved ActiveWalletID
                        var spendingtoDb = new Spending
                        {
                            // Assuming you have properties like Time, Description, Amount, etc.
                            Time = spending.Time,
                            Description = spending.Description,
                            Amount = spending.Amount,
                            IdWallet = userWithActiveWallet.ActiveWalletId.Value,
                        };
                        
                        userWithActiveWallet.ActiveWallet.Balance -= spending.Amount;

                        // Step 4: Persist the Spending record to the database
                        _db.Spendings.Add(spendingtoDb);
                        _db.SaveChanges();

                        // Redirect to the Index action or any other appropriate action
                        return RedirectToAction("Index");
                    }
                }
            }

            // Handle invalid model state or other errors
            return View(spending);
        }


        public IActionResult Edit(int? id){
            if(id == null || id ==0){
                return NotFound();
            }
            Spending? spendingFromDb = _db.Spendings.Find(id);
            if(spendingFromDb == null){
                return NotFound();
            }
            return View(spendingFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Spending obj){
            if(ModelState.IsValid){
                _db.Spendings.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id){
            if(id == null || id == -0) {
                return NotFound();
            }
            Spending? spendingFromDb = _db.Spendings.Find(id);
            if(spendingFromDb == null) {
                return NotFound();
            }
            return View(spendingFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id){
            Spending? obj = _db.Spendings.Find(id);
            if(obj == null){
                return NotFound();
            }
            _db.Spendings.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}