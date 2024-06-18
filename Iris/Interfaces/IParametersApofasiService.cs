using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IParametersApofasiService
    {
        void Create(ApofasiParametersGridViewModel data);
        void Destroy(ApofasiParametersGridViewModel data);
        IEnumerable<ApofasiParametersGridViewModel> Read();
        ApofasiParametersGridViewModel Refresh(int entityId);
        void Update(ApofasiParametersGridViewModel data);
    }
}