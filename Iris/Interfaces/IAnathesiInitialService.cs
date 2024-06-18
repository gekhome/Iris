using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiInitialService
    {
        void Create(AnathesiInitialViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiInitialViewModel data);
        IEnumerable<AnathesiInitialViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiInitialViewModel> Read(int schoolyearId, int schoolId);
        AnathesiInitialViewModel Refresh(int entityId);
        void Update(AnathesiInitialViewModel data, ApofasiParameters ap);
        void Update(AnathesiInitialViewModel data, int schoolyearId, int schoolId);
    }
}