using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ApiWrapper.Utils
{
    public interface IMarshallable
    {
        object BackingField { get; set; }
    }
}
