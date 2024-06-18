using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IRegAnathesiMetaboliService
    {
        RegAnathesiMetaboliViewModel GetRecord(int entityId);
        IEnumerable<RegAnathesiMetaboliViewModel> ReadEpas(int schoolyearId = 0);
        IEnumerable<RegAnathesiMetaboliViewModel> ReadIek(int schoolyearId = 0);
        IEnumerable<RegAnathesiMetaboliViewModel> ReadSchool(int schoolId, int schoolyearId = 0);
    }
}