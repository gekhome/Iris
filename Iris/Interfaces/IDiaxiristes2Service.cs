using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IDiaxiristes2Service
    {
        void Create(DiaxiristisViewModel data);
        void Destroy(DiaxiristisViewModel data);
        IEnumerable<DiaxiristisViewModel> Read();
        DiaxiristisViewModel Refresh(int entityId);
        void Update(DiaxiristisViewModel data);
    }
}