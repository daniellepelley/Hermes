using Hermes.Data.Operation;

namespace Hermes.Data.Test
{
    public class DataOperatorBuilder
    {
        private readonly DataOperator _dataOperator;

        public DataOperatorBuilder()
        {
            _dataOperator = new DataOperator();
        }

        public DataOperator Build()
        {
            return _dataOperator;
        }

        public DataOperatorBuilder AddOrderBy(string property, bool descending = false)
        {
            var dataSorter = new OrderBy
            {
                SortProperty = property
            };

            if (descending)
            {
                dataSorter.SortDirection = "DESC";
            } 

            _dataOperator.OrderBys.Add(dataSorter);

            return this;
        }

        public DataOperatorBuilder AddFilter(string property, string filterOperator, object filterValue)
        {
            var dataFilter = new Filter
            {
                FilterProperty = property,
                FilterOperator = filterOperator,
                FilterValue = filterValue
            };

            _dataOperator.Filters.Add(dataFilter);
            
            return this;
        }

        public DataOperatorBuilder Paging(int numberPerPage, int pageNumber)
        {
            _dataOperator.Pager.NumberPerPage = numberPerPage;
            _dataOperator.Pager.PageNumber = pageNumber;
            return this;
        }

    }
}