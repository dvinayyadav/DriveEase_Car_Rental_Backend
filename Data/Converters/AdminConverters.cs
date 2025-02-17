using Car_Rental_Backend_Application.Data.Entities;
using Car_Rental_Backend_Application.Data.RequestDto_s;
using Car_Rental_Backend_Application.Data.ResponseDto_s;

namespace Car_Rental_Backend_Application.Data.Converters
{
    public static class AdminConverters
    {
      
        public static Admin AdminRequestDtoToAdmin(AdminRequestDto requestDto)
        {
            return new Admin
            {
                Username = requestDto.Username,
                Email = requestDto.Email,
                Password = requestDto.Password
            };
        }

      
        public static AdminResponseDto AdminToAdminResponseDto(Admin admin)
        {
            return new AdminResponseDto
            {
                Admin_ID = admin.Admin_ID,
                Username = admin.Username,
                Email = admin.Email
            };
        }
    }
}
