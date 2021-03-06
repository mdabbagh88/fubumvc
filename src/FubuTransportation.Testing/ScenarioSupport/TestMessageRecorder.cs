﻿using System;
using System.Collections.Generic;
using System.Linq;
using FubuTestingSupport;

namespace FubuTransportation.Testing.ScenarioSupport
{
    public static class TestMessageRecorder
    {
        private static readonly IList<MessageProcessed> _processed = new List<MessageProcessed>();

        public static MessageProcessed[] AllProcessed
        {
            get { return _processed.ToArray(); }
        }

        public static void Clear()
        {
            _processed.Clear();
        }

        public static void ShouldMatch<THandler>(this MessageProcessed processed, Message message)
        {
            processed.Description.ShouldEqual(typeof (THandler).Name);
            processed.Message.ShouldEqual(message);
        }

        public static bool HasProcessed(Message message)
        {
            return _processed.Any(x => message.Equals(x.Message));
        }

        public static void Processed(string description, Message message, Uri receivedAt = null)
        {
            _processed.Fill(new MessageProcessed
            {
                Description = description,
                Message = message,
                ReceivedAt = receivedAt
            });
        }

        public static IEnumerable<MessageProcessed> ProcessedFor<T>() where T : Message
        {
            return _processed.Where(x => x.Message is T);
        }
    }
}