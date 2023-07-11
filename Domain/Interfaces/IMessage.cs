using Domain.Interfaces.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IMessage : IGeneric<Message>
    {
        Task<List<Message>> ListMessage(Expression<Func<Message, bool>> expression);
    }
}
