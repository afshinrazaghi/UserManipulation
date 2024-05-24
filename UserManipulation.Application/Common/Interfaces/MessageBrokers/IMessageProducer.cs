﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManipulation.Application.Common.Interfaces.MessageBrokers
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}
