using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
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
        private readonly IServiceMessage _serviceMessage;

        public MessageController(IMapper mapper, IMessage message, IServiceMessage serviceMessage)
        {
            _mapper = mapper;
            _message = message;
            _serviceMessage = serviceMessage;
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
        [Produces("application/json")]
        [HttpGet("/api/GetAll")]
        public async Task<List<MessageViewModel>> GetAll()
        {
            var message = await _message.List();
            var messageMap = _mapper.Map<List<MessageViewModel>>(message);
            return messageMap;
        }

        [Authorize]
        [Produces("application/json")]
        [HttpGet("/api/GetActives")]
        public async Task<List<MessageViewModel>> GetActives()
        {
            var message = await _serviceMessage.ListActivesMessages();
            var messageMap = _mapper.Map<List<MessageViewModel>>(message);
            return messageMap;
        }

        [Authorize]
        [HttpGet("/api/GetById/{id:int}")]
        public async Task<ActionResult<MessageViewModel>> GetById(int id)
        {
            var message = await _message.GetEntityById(message => message.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            else
            {
                var messageMap = _mapper.Map<MessageViewModel>(message);
                return Ok(messageMap);
            }
        }

        [Authorize]
        [HttpPost("/api/AddMessage")]
        public async Task<List<Notifies>> AddMessage(MessageViewModel messageViewModel)
        {
            messageViewModel.UserId = GetIdUser();
            var messageMap = _mapper.Map<Message>(messageViewModel);
            await _serviceMessage.Add(messageMap);
            return messageMap.Notify;
        }

        [Authorize]
        [HttpPut("/api/UpdateMessage")]
        public async Task<ActionResult<MessageViewModel>> UpdateMessage([FromBody] MessageViewModel messageView)
        {
            var messageMap = _mapper.Map<Message>(messageView);
            await _serviceMessage.Update(messageMap);
            return Ok(messageMap);
        }

        [Authorize]
        [HttpDelete("/api/DeleteMessage/{id:int}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            var message = await _message.GetEntityById(message => message.Id == id);
            if (message == null)
            {
                return NotFound("Mensagem não encontrada");
            }
            await _message.Delete(message);
            return Ok("Mensagem apagada");
        }
    }
}
