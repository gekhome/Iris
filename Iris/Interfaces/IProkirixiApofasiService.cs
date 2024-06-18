using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IProkirixiApofasiService
    {
        void Create(ProkirixiApofasiGridViewModel data);
        void Destroy(ProkirixiApofasiGridViewModel data);
        IEnumerable<ProkirixiApofasiGridViewModel> Read();
        ProkirixiApofasiGridViewModel Refresh(int entityId);
        void Update(ProkirixiApofasiGridViewModel data);
    }
}