using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IRegAnathesiProslipsiService
    {
        RegAnathesiProslipsiViewModel GetRecord(int entityId);
        IEnumerable<RegAnathesiProslipsiViewModel> ReadEpas(int schoolyearId = 0);
        IEnumerable<RegAnathesiProslipsiViewModel> ReadIek(int schoolyearId = 0);
        IEnumerable<RegAnathesiProslipsiViewModel> ReadSchool(int schoolId, int schoolyearId = 0);
    }
}