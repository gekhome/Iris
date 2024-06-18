using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IProistamenoi2Service
    {
        void Create(ProistamenosViewModel data);
        void Destroy(ProistamenosViewModel data);
        IEnumerable<ProistamenosViewModel> Read();
        ProistamenosViewModel Refresh(int entityId);
        void Update(ProistamenosViewModel data);
    }
}