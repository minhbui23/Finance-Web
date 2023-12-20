using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Finance.DataAccess.DBContext;
using Finance.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace FinanceWeb.Controllers
{
    [Authorize]
    public class WalletController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager; 
        public WalletController(ApplicationDBContext db, UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            if (_signManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var userWithWallets = _userManager.Users.Include(u => u.Wallets).SingleOrDefault(u => u.Id == user.Id);

                    return View(userWithWallets.Wallets);
                }
            }
            return View();
        }



        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Wallet obj)
        {
            if (ModelState.IsValid)
            {
                if (_signManager.IsSignedIn(User))
                {
                    // Get the current user
                    var user = await _userManager.GetUserAsync(User);

                    // Check if the user is not null
                    if (user != null)
                    {
                        // Set the UserId for the wallet
                        //obj.UserId = user.Id;

                        // Add the wallet to the database
                        _db.Wallets.Add(obj);
                        _db.SaveChanges();

                        TempData["success"] = "Wallet created successfully";
                        return RedirectToAction("Index");
                    }
                }
            }

            //check error
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }


            return View(obj);
        }   
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Wallet walletFromDb = _db.Wallets.Find(id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (walletFromDb == null)
            {
                return NotFound();
            }

            return View(walletFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Wallet updatedWallet)
        {
            if (ModelState.IsValid)
            {
                _db.Wallets.Update(updatedWallet);
                _db.SaveChanges();

                TempData["success"] = "Wallet updated successfully";
                return RedirectToAction("Index");
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return View(updatedWallet);
        }

        public IActionResult Delete(int? id){   
            if(id == null || id == -0) {
                return NotFound();
            }
            Wallet? walletFromDb = _db.Wallets.Find(id);
            if(walletFromDb == null) {
                return NotFound();
            }
            return View(walletFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id){
            Wallet? obj = _db.Wallets.Find(id);
            if(obj == null){
                return NotFound();
            }
            _db.Wallets.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Wallet deleted successfully";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SwitchWallet(int walletId)
        {
            if (_signManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var userWithWallets = _userManager.Users.Include(u => u.Wallets).SingleOrDefault(u => u.Id == user.Id);
                    var selectedWallet = userWithWallets.Wallets.Any(w => w.Id == walletId);
                    if (selectedWallet != null)
                    {
                        // Update the user's active wallet
                        user.ActiveWalletId = walletId;
                        await _userManager.UpdateAsync(user);
                    }
                }

            }

            return RedirectToAction("Index");
        }


    }
}