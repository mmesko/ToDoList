using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common
{
    /// <summary>
    /// Class that provides filtering options
    /// </summary>
    public class GenericPaging
    {
        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pageNumber">Page number</param>
        /// <param name="pageSize">Page size</param>
        public GenericPaging(int pageNumber, int pageSize)
        {
            defaultPageSize = 20;
            this.pageSize = pageSize;
            this.pageNumber = pageNumber;
        }

        #region Fields

        protected int pageNumber;
        protected int pageSize;
        protected int defaultPageSize;

        #endregion

        #region Fields

        /// <summary>
        /// Gets page number
        /// </summary>
        public virtual int PageNumber
        {
            get
            {
                if (pageNumber <= 0)
                    pageNumber = 1;

                return pageNumber;
            }
        }

        /// <summary>
        /// Gets page size
        /// </summary>
        public virtual int PageSize
        {
            get
            {
                if (pageSize <= 0 || pageSize > defaultPageSize)
                    pageSize = defaultPageSize;
                return pageSize;
            }
        }

        #endregion

     

        #endregion
    }
}
