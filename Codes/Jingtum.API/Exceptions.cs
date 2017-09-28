using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jingtum.API
{
    public class JingtumException:Exception
    {
        private const long SerialVersionUID = 1L;
        protected string m_Message;

        public override string Message
        {
            get
            {
                return m_Message;
            }
        }
    }

    public class InvalidParameterException:JingtumException 
    {        
        private string m_Param;
	
        public string Param
        {
            get
            {
                return m_Param;
            }
        }

        public InvalidParameterException(string message, string param)
        {
            m_Message = message;
            this.m_Param = param;
        }
    }

    public class EncodingFormatException : JingtumException
    {
        public EncodingFormatException(string message)
        {
            m_Message = message;
        }
    }

    public class APIException : JingtumException 
    {
        private string m_ErrorType;
        private string m_Error;

        public APIException(string errorType, string error, string message)
        {
            m_ErrorType = errorType;
            m_Error = error;
            m_Message = message;
        }

        public string ErrorType
        {
            get
            {
                return m_ErrorType;
            }
        }

        public string Error
        {
            get
            {
                return m_Error;
            }
        }
    }

    //enum APIExceptionErrorType
    //{
    //    Others,
    //    ClientError,
    //    NetworkError,
    //    TransactionError,
    //    ServerError,
    //}
	
}
