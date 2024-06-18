using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IRegAnathesiUniversalService
    {
        AnatheseisUniversalViewModel GetRecord(int entityId);
        IEnumerable<AnatheseisUniversalViewModel> Read(int schoolyearId = 0);
    }
}