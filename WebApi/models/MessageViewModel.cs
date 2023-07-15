
namespace WebApi.models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActivated { get; set; }
        public string RegisterDate { get; set; }
        public string AltTime { get; set; }
        public string UserId { get; set; }
    }
}
