namespace GameLibrary.Server.Models
{
    public class Game
    {
        public string OfficialTitle { get; set; }
        public string CommonTitle { get; set; }
        public string SerialNumber { get; set; }
        public string Region { get; set; }
        public string GenreStyle { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string DateReleased { get; set; }
        public string Barcode { get; set; }
        public string Language { get; set; }
        public string GameDescription { get; set; }
        public string NumberOfPlayers { get; set; }
        public string NumberOfMemoryCardBlocks { get; set; }
        public string Vibration { get; set; }
        public string MultiTapFunction { get; set; }
        public string LinkCableFunction { get; set; }
    }
}
