
namespace WebApi.models
{
    public class MessageViewModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public bool isActivated { get; set; }
        public DateTime registerDate { get; set; }
        public DateTime altTime { get; set; }
        public int userId { get; set; }
    }
}
