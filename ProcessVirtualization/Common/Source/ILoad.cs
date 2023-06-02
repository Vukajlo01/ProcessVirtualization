using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common
{
   
    [ServiceContract]
    public interface ILoad
    {
        [OperationContract]
        List<Load> ListLoads(DateTime dt);
    }
}
