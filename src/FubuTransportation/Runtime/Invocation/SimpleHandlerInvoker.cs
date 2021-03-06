﻿using System;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;

namespace FubuTransportation.Runtime.Invocation
{
    public class SimpleHandlerInvoker<TController, TInput> : BasicBehavior where TInput : class
    {
        private readonly Action<TController, TInput> _action;
        private readonly TController _controller;
        private readonly IFubuRequest _request;

        public SimpleHandlerInvoker(IFubuRequest request, TController controller,
                                    Action<TController, TInput> action)
            : base(PartialBehavior.Executes)
        {
            _request = request;
            _controller = controller;
            _action = action;
        }

        protected override DoNext performInvoke()
        {
            var input = _request.Find<TInput>().Single();
            _action(_controller, input);
            return DoNext.Continue;
        }
    }
}