using AutoMapper;
using Domain.Interfaces;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMessage _message;

        public MessageController(IMapper mapper, IMessage message)
        {
            _mapper = mapper;
            _message = message;
        }

        private string GetIdUser()
        {
            if (User != null)
            {
                var userId = User.FindFirst("userId");
                return userId.Value;
            }
            else { return string.Empty; }
        }

        [Authorize]
        [HttpGet("/api/Post")]
        public async Task<List<Notifies>> Post(MessageViewModel messageViewModel)
        {
            messageViewModel.userId = GetIdUser();
            var messageMap = _mapper.Map<Message>(messageViewModel);
            await _message.Add(messageMap);
            return messageMap.Notify;
        }
    }
}
