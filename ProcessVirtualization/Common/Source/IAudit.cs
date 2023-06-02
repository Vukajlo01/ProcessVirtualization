using System.Collections.Generic;
using System.ServiceModel;

namespace Common
{
    
    [ServiceContract]
    public interface IAudit
    {
        [OperationContract]
        Audit GetLastAudit();
    }
}
