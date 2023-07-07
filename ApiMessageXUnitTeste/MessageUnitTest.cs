using AutoMapper;
using Domain.Interfaces;
using Entities.Entities;
using Infraestructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.models;
using Xunit;

namespace ApiMessageXUnitTeste
{
    public class MessageUnitTest
    {
        private IMapper _mapper;
        private IMessage _message;

        public static DbContextOptions<ContextBase> dbOptions { get; }

        public static string conString = "Data Source=DESKTOP-MHHGNGB;Initial Catalog=messagesDb;Integrated Security=True;TrustServerCertificate=True";

        static MessageUnitTest()
        {
            dbOptions = new DbContextOptionsBuilder<ContextBase>().UseSqlServer(conString).Options;
        }

        public MessageUnitTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MessageViewModel, Message>();
                cfg.CreateMap<Message, MessageViewModel>();
            });
            _mapper = config.CreateMapper();
        }

        //====UNIT TESTES====

        //GET TEST:
        [Fact]
        public void GetMessage_Return_OkResult()
        {
            var controller = new MessageController(_mapper, _message);

            var data = controller.GetAll();

            Assert.IsType<Task<List<MessageViewModel>>>(data);
;        }
    }
}
