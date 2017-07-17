using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieWebApi.UnitOfWork
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWorkResult
    {
        /// <summary>
        /// Response model
        /// </summary>
        public UnitOfWorkResult()
        {

        }

        /// <summary>
        /// Result object
        /// </summary>
        public object Result { get; private set; }

        /// <summary>
        /// Error flag
        /// </summary>
        public bool HasError => Status != UnitOfWorkStatus.Ok;

        /// <summary>
        /// Status code
        /// </summary>
        public UnitOfWorkStatus Status { get; }

        /// <summary>
        /// If exception happens this will be filled
        /// </summary>
        public Exception Exception { get; private set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="status"></param>
        public UnitOfWorkResult(object result, UnitOfWorkStatus status)
        {
            Result = result;
            Status = status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="status"></param>
        public UnitOfWorkResult(Exception e, UnitOfWorkStatus status)
        {
            Exception = e;
            Status = status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="status"></param>
        /// <param name="exception"></param>
        public UnitOfWorkResult(object result, UnitOfWorkStatus status, Exception exception)
            : this(result, status)
        {
            Exception = exception;
        }
    }
}