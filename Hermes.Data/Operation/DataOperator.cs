using System.Collections.Generic;

namespace Hermes.Data.Operation
{
    public class DataOperator
    {
        private Filter[] _filters;
        private List<OrderBy> _orderBys;
        private Pager _pager;

        public Filter[] Filters
        {
            set { _filters = value;  }
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
            _filters = new Filter[0];
            _orderBys = new List<OrderBy>();
            _pager = new Pager();
        }
    }
}
