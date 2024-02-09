using Newtonsoft.Json;

namespace WPCRecruitmentTest.ViewModels.Responses
{
    //TODO: Move these into a folder
    public class Street
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Location
    {
        [JsonProperty("latitude")]
        public string Latitude { get; set; }
        [JsonProperty("street")]
        public Street Street { get; set; }
        [JsonProperty("longitude")]
        public string Longitude { get; set; }
    }

    public class CrimeData
    {
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("location_type")]
        public string Location_Type { get; set; }
        [JsonProperty("location")]
        public Location Location { get; set; }
        [JsonProperty("context")]
        public string Context { get; set; }
        [JsonProperty("outcome_status")]
        public object Outcome_Status { get; set; }
        [JsonProperty("persistent_id")]
        public string Persistent_Id { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("location_subtype")]
        public string Location_Subtype { get; set; }
        [JsonProperty("month")]
        public string Month { get; set; }
    }

}
