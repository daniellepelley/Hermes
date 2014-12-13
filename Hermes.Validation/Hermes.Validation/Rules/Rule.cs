using System;
using Hermes.Validation.Interfaces;

namespace Hermes.Validation.Rules
{
    public abstract class Rule<T> : IRule<T>, IComparable
    {
        private Func<T, string> _logic;
        private readonly string _message;

        public Func<T, string> Logic
        {
            protected set { _logic = value; }
            get { return _logic; }
        }

        protected Rule()
        {
            _logic = t => string.Empty;
        }

        protected Rule(string message, Func<T, string> logic)
        {
            _message = message;
            _logic = logic;
        }

        public string Check(T value)
        {
            if (_logic != null)
            {
                return _logic(value);
            }
            return string.Empty;
        }

        public virtual string Message
        {
            get { return _message; }
        }

        public virtual int CompareTo(object obj)
        {
            return 0;
        }
    }

    public static class Extensions
    {
        public static bool CheckValid<T>(this Rule<T> rule, T value)
        {
            return rule.Check(value) == string.Empty;
        }
   
    }
}
