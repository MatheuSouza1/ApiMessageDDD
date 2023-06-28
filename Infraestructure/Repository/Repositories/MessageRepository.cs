using Domain.Interfaces;
using Entities.Entities;
using Infraestructure.Configuration;
using Infraestructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository.Repositories
{
    public class MessageRepository : GenericRepository<Message>, IMessage
    {
        private readonly DbContextOptions<ContextBase> _dbContext;
        public MessageRepository()
        {
            _dbContext = new DbContextOptions<ContextBase>();
        }
    }
}
