using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IDioikites2Service
    {
        void Create(DioikitisViewModel data);
        void Destroy(DioikitisViewModel data);
        IEnumerable<DioikitisViewModel> Read();
        DioikitisViewModel Refresh(int entityId);
        void Update(DioikitisViewModel data);
    }
}