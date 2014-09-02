using System;
using Hermes.Validation.Interfaces;

namespace Hermes.Validation.Rules
{
    public abstract class Rule<T> : IRule<T>, IComparable
    {
        #region Parameters

        private Func<T, string> _logic;

        public Func<T, string> Logic
        {
            protected set { _logic = value; }
            get { return _logic; }
        }

        #endregion

        #region Constructors

        public Rule()
        {
            _logic = t => string.Empty;
        }

        public Rule(Func<T, string> logic)
            : this()
        {
            _logic = logic;
        }

        //public Rule(Func<T, string> logic, Func<T, T> enforcer)
        //    : this()
        //{
        //    _logic = logic;
        //}

        #endregion

        #region Methods

        //protected virtual string LogicMethod(T value)
        //{
        //    //Make sure it doesn't call itself
        //    if (_logic == LogicMethod)
        //        return string.Empty;
        //    return _logic(value);
        //}

        #endregion

        public string Check(T value)
        {
            if (_logic != null)
            {
                return _logic(value);
            }
            return string.Empty;
        }

        public bool CheckValid(T value)
        {
            return Check(value) == string.Empty;
        }

        public abstract string Message { get; }

        public virtual int CompareTo(object obj)
        {
            return 0;
        }
    }
}
