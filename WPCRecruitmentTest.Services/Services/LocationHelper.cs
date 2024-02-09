namespace WPCRecruitmentTest.Services.Services
{
    public class LocationHelper
    {
        private const double UK_MIN_LATITUDE = 49.861144; // Southernmost latitude
        private const double UK_MAX_LATITUDE = 61.048661; // Northernmost latitude
        private const double UK_MIN_LONGITUDE = -8.621927; // Westernmost longitude
        private const double UK_MAX_LONGITUDE = 1.681530; // Easternmost longitude

        public bool IsWithinUk(float latitude, float longitude) =>
            latitude >= UK_MIN_LATITUDE &&
            latitude <= UK_MAX_LATITUDE &&
            longitude >= UK_MIN_LONGITUDE &&
            longitude <= UK_MAX_LONGITUDE;
    }
}
