using Car_Rental_Backend_Application.Data.Entities;
using Car_Rental_Backend_Application.Data.RequestDto_s;
using Car_Rental_Backend_Application.Data.ResponseDto_s;

namespace Car_Rental_Backend_Application.Data.Converters
{
    public static class UserConverters
    {
        public static User RequestUserDtoToUser(UserRequestDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            return new User
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber
            };
        }

        public static UserResponseDto UserToResponseUserDto(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return new UserResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Address = user.Address,
                Phone_Number = user.PhoneNumber,
                //BookingIds = user.Bookings?.Select(b => b.BookingId).ToList() ?? new List<int>() 
            };
        }

        public static UpdateUserResponseDto UserToUserUpdateDto(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return new UpdateUserResponseDto
            {
              Email = user.Email,
              PhoneNumber=user.PhoneNumber,
              UserName = user.Username,
              Address = user.Address, 
            };
        }

    }
}
