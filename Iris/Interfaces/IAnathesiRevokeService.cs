using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiRevokeService
    {
        void Create(AnathesiRevokeViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiRevokeViewModel data);
        IEnumerable<AnathesiRevokeViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiRevokeViewModel> Read(int schoolyearId, int schoolId);
        AnathesiRevokeViewModel Refresh(int entityId);
        void Update(AnathesiRevokeViewModel data, ApofasiParameters ap);
        void Update(AnathesiRevokeViewModel data, int schoolyearId, int schoolId);
    }
}