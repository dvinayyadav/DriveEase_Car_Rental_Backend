using Car_Rental_Backend_Application.Data;
using Car_Rental_Backend_Application.Data.RequestDto_s;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_Backend_Application.Services
{
    public class UserService : IUserService
    {
        private readonly CarRentalContext _context;

        public UserService(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<UserProfileDto> GetUserProfileByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return null;

            return new UserProfileDto
            {
                Username = user.Username,
                Email = user.Email,
                Address = user.Address,
                Phone = user.PhoneNumber
            };
        }


        // ✅ Update User Profile
        public async Task<bool> UpdateUserProfileAsync(int userId, UserProfileDto userProfile)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return false;

            user.Username = userProfile.Username;
            user.Address = userProfile.Address;
            user.PhoneNumber = userProfile.Phone;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }




    }
}
