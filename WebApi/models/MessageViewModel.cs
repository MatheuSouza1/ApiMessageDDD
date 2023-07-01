
namespace WebApi.models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActivated { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime AltTime { get; set; }
        public string userId { get; set; }
    }
}
