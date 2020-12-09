using System;
using System.Collections.Generic;
using System.Text;

namespace SAttributes.Exceptions
{
    public class SAttributesException : Exception
    {
        public SAttributesException(String message) : base(message) { }
    }
}
