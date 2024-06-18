using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IRegAnathesiMetaboliAnService
    {
        RegAnathesiMetaboliAnViewModel GetRecord(int entityId);
        IEnumerable<RegAnathesiMetaboliAnViewModel> Read(int schoolyearId = 0);
        IEnumerable<RegAnathesiMetaboliAnViewModel> ReadSchool(int schoolId, int schoolyearId = 0);
    }
}