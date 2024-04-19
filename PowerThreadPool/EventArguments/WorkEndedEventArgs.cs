﻿using System;
using PowerThreadPool.Results;

namespace PowerThreadPool.EventArguments
{
    public class WorkEndedEventArgs : WorkEventArgsBase
    {
        public WorkEndedEventArgs() { }

        private object result;
        public object Result { get => result; internal set => result = value; }

        private bool succeed;
        public bool Succeed { get => succeed; internal set => succeed = value; }

        private Exception exception;
        public Exception Exception { get => exception; internal set => exception = value; }

        private RetryInfo retryInfo;
        public RetryInfo RetryInfo { get => retryInfo; internal set => retryInfo = value; }
    }
}
