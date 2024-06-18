using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IHourWagesService
    {
        void Create(SysHourWagesViewModel data);
        void Destroy(SysHourWagesViewModel data);
        IEnumerable<SysHourWagesViewModel> Read();
        SysHourWagesViewModel Refresh(int entityId);
        void Update(SysHourWagesViewModel data);
    }
}