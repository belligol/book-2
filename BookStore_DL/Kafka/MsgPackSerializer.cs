﻿using Confluent.Kafka;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_DL.Kafka
{
    public class MsgPackSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            
            return MessagePackSerializer.Serialize(data);
        }

    }
}
