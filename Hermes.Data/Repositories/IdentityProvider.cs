using System;
using System.Collections.Generic;
using System.Linq;

namespace Hermes.Data.Repositories
{
    public class IdentityProvider<T>
    {
        private Func<T, int> _getIdFunc;

        public Func<T, int> GetIdFunc
        {
            get { return _getIdFunc; }
            set { _getIdFunc = value; }
        }

        private Action<T, int> _setIdFunc;

        public Action<T, int> SetIdFunc
        {
            get { return _setIdFunc; }
            set { _setIdFunc = value; }
        }

        public int GetNextIdentity(IEnumerable<T> entities)
        {
            if (_getIdFunc == null)
                return -1;

            return entities.Select(e => _getIdFunc(e)).Max() + 1;
        }

        public void SetIdentity(T entity)
        {

        }
    }

    public interface IIdentitySetter<T>
    {
        void SetId(T entity);
    }

    public class IdentitySetter<T> : IIdentitySetter<T>
    {
        private int _seed;

        public int Seed
        {
            get { return _seed; }
            set { _seed = value; }
        }

        private Action<T, int> _setIdFunc;

        public Action<T, int> SetIdFunc
        {
            get { return _setIdFunc; }
            set { _setIdFunc = value; }
        }

        public IdentitySetter(int seed, Action<T, int> setIdFunc)
        {
            _seed = seed;
            _setIdFunc = setIdFunc;
        }

        public void SetId(T entity)
        {
            _seed++;
            _setIdFunc(entity, _seed);
        }
    }
}
