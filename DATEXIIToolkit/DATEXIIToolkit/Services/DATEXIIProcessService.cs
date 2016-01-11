using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DATEXIIToolkit.Services
{
    public abstract class DATEXIIProcessService
    {
        public abstract void processMessage(D2LogicalModel d2LogicalModel);
    }

}