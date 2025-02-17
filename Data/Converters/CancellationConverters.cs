using Car_Rental_Backend_Application.Data.Entities;
using Car_Rental_Backend_Application.Data.DTOs;

namespace Car_Rental_Backend_Application.Data.Converters
{
    public static class CancellationConverters
    {
        public static Cancellation CancellationRequestDtoToCancellation(CancellationRequestDto dto)
        {
            return new Cancellation
            {
                Booking_ID = dto.Booking_ID,
                Cancellation_Date = dto.Cancellation_Date,
                Reason = dto.Reason
            };
        }

        public static CancellationResponseDto CancellationToCancellationResponseDto(Cancellation cancellation)
        {
            return new CancellationResponseDto
            {
                Cancellation_ID = cancellation.Cancellation_ID,
                Booking_ID = cancellation.Booking_ID,
                Cancellation_Date = cancellation.Cancellation_Date,
                Reason = cancellation.Reason
            };
        }
    }
}
