using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IKladoiHoursService
    {
        void Create(SysKladosViewModel data);
        void Destroy(SysKladosViewModel data);
        IEnumerable<SysKladosViewModel> Read();
        SysKladosViewModel Refresh(int entityId);
        void Update(SysKladosViewModel data);
    }
}