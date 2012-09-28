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
        private delegate void MessageEvent(DebugMessage msg);
        private event MessageEvent NewMessage;
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
        public void write(string message)
        {
            StackTrace st = new StackTrace();
            DebugMessage msg = new DebugMessage(message, st);
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
        #endregion
    }
    public struct DebugMessage
    {
        string message;
        StackTrace stacktrace;
        public DebugMessage(string message, StackTrace stacktrace)
        {
            this.message = message;
            this.stacktrace = stacktrace;
        }
        public string toString()
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