namespace WPCRecruitmentTest.Models
{
    public static class ErrorMessages
    {
        public static class Generic
        {
            public static Error SomethingWentWrong = new("genn-err-1", "Something went wrong");
            public static Error NotWithinUK = new("genn-err-2", "Location provided is not within the UK");
            public static Error NoDataYet = new("genn-err-3", "No data uploaded for this time frame");
        }
    }
}
