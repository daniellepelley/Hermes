using System.Collections.Generic;

namespace Hermes.Data.Operation
{
    public class DataOperator
    {
        private readonly List<Filter> _filters;
        private readonly List<OrderBy> _orderBys;
        private readonly Pager _pager;

        public List<Filter> Filters
        {
            get { return _filters; }
        }

        public List<OrderBy> OrderBys
        {
            get { return _orderBys; }
        }

        public Pager Pager
        {
            get { return _pager; }
        }

        public DataOperator()
        {
            _filters = new List<Filter>();
            _orderBys = new List<OrderBy>();
            _pager = new Pager();
        }
    }
}
