using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IRegAnathesiProslipsiAnService
    {
        RegAnathesiProslipsiAnViewModel GetRecord(int entityId);
        IEnumerable<RegAnathesiProslipsiAnViewModel> Read(int schoolyearId = 0);
        IEnumerable<RegAnathesiProslipsiAnViewModel> ReadSchool(int schoolId, int schoolyearId = 0);
    }
}