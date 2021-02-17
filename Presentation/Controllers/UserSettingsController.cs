using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Presentation.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class UserSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserSettingsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserSettings/SetTimeZone
        public async Task<IActionResult> SetTimeZone([FromBody] UserSettingSetTimeZoneDTO value)
        {
            ApplicationUser curUser = await _userManager.GetUserAsync(HttpContext.User);
            UserSetting currentUserSetting = await _context.UserSettings.Where(x => x.ApplicationUserId.Equals(curUser.Id)).FirstOrDefaultAsync();

            currentUserSetting.CurrentTimeZone = value.value;
            await _context.SaveChangesAsync();
            return Ok();
        }


    }
}
