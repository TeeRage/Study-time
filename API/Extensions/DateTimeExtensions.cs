//Calculates age uf the user base on the birhday and current DateTime
namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateTime dob) {
            var today = DateTime.Today;
            var age  = today.Year - dob.Year;
            if(dob.Date > today.AddYears(-age)) age--; //If birthday is later rhis year, take one year away
            return age;
        }
    }
}