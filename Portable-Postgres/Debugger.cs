/*
 * Creative Commons Attribution-ShareAlike 3.0 unported
 * ***************************************************************
 * Author:  limpygnome
 * E-mail:  limpygnome@gmail.com
 * Site:    ubermeat.co.uk
 * ***************************************************************
 * Credit to:
 * -- none
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Portable_Postgres
{
    public class Debugger
    {
        #region "Delegates & Events"
        public delegate void MessageEvent(DebugMessage msg);
        public event MessageEvent NewMessage;
        #endregion
        #region "Variables"
        private List<DebugMessage> messages = new List<DebugMessage>();
        #endregion
        #region "Methods - Properties"
        public DebugMessage[] Messages
        {
            get
            {
                return messages.ToArray();
            }
        }
        #endregion
        #region "Methods"
        /// <summary>
        /// Adds a debug message, which includes a stack-trace to this very invocation.
        /// </summary>
        /// <param name="message"></param>
        public void write(string message)
        {
            StackTrace st = new StackTrace();
            DebugMessage msg = new DebugMessage(message, st);
            messages.Add(msg);
            if (NewMessage != null)
                NewMessage(msg);
        }
        /// <summary>
        /// Adds a single debug message, without a stack-trace.
        /// </summary>
        /// <param name="message"></param>
        public void writeNoTrace(string message)
        {
            DebugMessage msg = new DebugMessage(message);
            messages.Add(msg);
            if (NewMessage != null)
                NewMessage(msg);
        }
        public int count()
        {
            return messages.Count;
        }
        public DebugMessage get(int index)
        {
            if (index >= messages.Count)
                throw new IndexOutOfRangeException("Specified index is out of range!");
            return messages[index];
        }
        public void clear()
        {
            messages.Clear();
        }
        public override string ToString()
        {
            StringBuilder data = new StringBuilder();
            foreach (DebugMessage msg in messages)
                data.Append(msg).Append("\r\n\r\n");
            return data.ToString();
        }
        #endregion
    }
    public struct DebugMessage
    {
        public string message;
        public StackTrace stacktrace;
        public DebugMessage(string message, StackTrace stacktrace)
        {
            this.message = message;
            this.stacktrace = stacktrace;
        }
        public DebugMessage(string message)
        {
            this.message = message;
            this.stacktrace = null;
        }
        public override string ToString()
        {
            if (stacktrace == null)
                return message + "\r\n\r\n";
            else
            {
                StringBuilder msg = new StringBuilder();
                System.Reflection.MethodBase m;
                foreach (StackFrame f in stacktrace.GetFrames())
                {
                    m = f.GetMethod();
                    msg.Append(m.Module).Append(", ln ").Append(f.GetFileLineNumber()).Append(": ").Append(m.ToString()).Append("\r\n");
                }
                msg.Append("Stack-frames: ").Append(stacktrace.FrameCount).Append(" - ").Append(message);
                return msg.ToString();
            }
        }
    }
}