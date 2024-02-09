using WPCRecruitmentTest.Models.Enums;
using WPCRecruitmentTest.Services.Services;

namespace WPCRecruitmentTest.Test
{
    public class DateHelperTests
    {
        [Theory]
        [InlineData(Month.January, "2024-01")] // Current month hasn't passed, expect this year
        [InlineData(Month.December, "2023-12")] // Current month hasn't passed, expect last year
        public void GetLastYearMonth_Returns_CorrectDate(Month requestedMonth, string expectedDate)
        {

            // Arrange
            var dateHelper = new DateHelper();
            Pose.Shim dateTimeShim = Pose.Shim.Replace(() => DateTime.Now).With(() => new DateTime(2024, 2, 1));

            // Act
            var result = dateHelper.GetLastYearMonth(requestedMonth);

            // Must return date within the last 12 months
            // Assert 
            Assert.Equal(expectedDate, result);
        }
    }
}
