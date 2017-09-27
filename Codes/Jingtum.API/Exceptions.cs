using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jingtum.API
{
    public class JingtumException:Exception
    {
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
        private const long SerialVersionUID = 1L;
        private string m_Param;
	
        public string Param
        {
            get
            {
                return m_Param;
            }
        }

        public InvalidParameterException(String message, String param)
        {
            m_Message = message;
            this.m_Param = param;
        }
    }

    public class EncodingFormatException : JingtumException
    {
        private const long SerialVersionUID = 1L;

        public EncodingFormatException(String message)
        {
            m_Message = message;
        }
    }
}
