using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceMessage : IServiceMessage
    {
        private readonly IMessage _IMessage;

        public ServiceMessage(IMessage IMessage)
        {
            _IMessage = IMessage;
        }

        public async Task Add(Message message)
        {
            var validateTitle = message.stringValidation(message.Title, "Title");
            if (validateTitle)
            {
                message.RegisterDate = DateTime.Now;
                message.AltTime = DateTime.Now;
                message.IsActivated = true;
                await _IMessage.Add(message);
            }
        }

        public async Task<List<Message>> ListActivesMessages()
        {
            return await _IMessage.ListMessage(n => n.IsActivated);
        }

        public async Task Update(Message message)
        {
            message.AltTime = DateTime.Now;
            await _IMessage.Update(message);
        }
    }
}
