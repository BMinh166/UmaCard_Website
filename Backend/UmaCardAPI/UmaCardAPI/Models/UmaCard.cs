namespace UmaCardAPI.Models
{
    public class UmaCard
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string OutfitType { get; set; } = "";
        public string Type { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "";
    }
}
