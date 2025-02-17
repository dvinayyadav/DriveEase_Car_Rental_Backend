using Car_Rental_Backend_Application.Data.RequestDto_s;

namespace Car_Rental_Backend_Application.Services
{
    public interface IUserService
    {
        // ✅ Get user profile by Email
        Task<UserProfileDto> GetUserProfileByEmailAsync(string email);

        // ✅ Update user profile
        Task<bool> UpdateUserProfileAsync(int userId, UserProfileDto userProfile);
    }
}
