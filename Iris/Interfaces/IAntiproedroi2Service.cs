using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAntiproedroi2Service
    {
        void Create(AntiproedrosViewModel data);
        void Destroy(AntiproedrosViewModel data);
        IEnumerable<AntiproedrosViewModel> Read();
        AntiproedrosViewModel Refresh(int entityId);
        void Update(AntiproedrosViewModel data);
    }
}