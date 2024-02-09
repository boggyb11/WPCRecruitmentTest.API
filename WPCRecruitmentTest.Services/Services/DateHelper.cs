using WPCRecruitmentTest.Models.Enums;

namespace WPCRecruitmentTest.Services.Services
{
    public class DateHelper
    {
        public string GetLastYearMonth(Month requestedMonth)
        {
            DateTime currentDate = DateTime.Now;

            // If the current month has passed the requested month, use the current year; otherwise, use the current year minus one
            int lastYear = (currentDate.Month >= (int)requestedMonth) ? currentDate.Year : currentDate.Year - 1;

            DateTime lastYearDate = new(lastYear, (int)requestedMonth, 1);

            string formattedDate = lastYearDate.ToString("yyyy-MM");

            return formattedDate;
        }
    }
}
