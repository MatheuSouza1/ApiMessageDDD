using Domain.Interfaces.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entitites;
using Entities.Entities;

namespace Domain.Interfaces
{
    public interface IMessage : IGeneric<Message>
    {
    }
}
