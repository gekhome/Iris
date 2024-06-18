using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iris.DAL;
using Iris.Models;
using Iris.BPM;
using System.Data.Entity;

namespace Iris.Services
{
    public class AnathesiSupplementService : IAnathesiSupplementService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public AnathesiSupplementService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<AnathesiSupplementViewModel> Read(int schoolyearId, int schoolId)
        {
            var data = (from d in entities.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ
                        orderby d.ΑΝΑΘΕΣΗ_ΚΩΔ descending
                        where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΣΧΟΛΗ == schoolId
                        select new AnathesiSupplementViewModel
                        {
                            ΑΝΑΘΕΣΗ_ΚΩΔ = d.ΑΝΑΘΕΣΗ_ΚΩΔ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                            ΩΡΕΣ_ΕΒΔ = d.ΩΡΕΣ_ΕΒΔ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΚΛΑΔΟΣ_ΚΩΔ = d.ΚΛΑΔΟΣ_ΚΩΔ,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΣΥΜΒΑΣΗ = d.ΣΥΜΒΑΣΗ,
                            ΦΥΛΟ = d.ΦΥΛΟ,
                            ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = d.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ
                        }).ToList();

            return data;
        }

        public IEnumerable<AnathesiSupplementViewModel> Read(ApofasiParameters ap)
        {
            List<AnathesiSupplementViewModel> data = new List<AnathesiSupplementViewModel>();

            if (ap.apofasiId > 0)
            {
                data = (from d in entities.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId
                        orderby d.ΑΝΑΘΕΣΗ_ΚΩΔ descending
                        select new AnathesiSupplementViewModel
                        {
                            ΑΝΑΘΕΣΗ_ΚΩΔ = d.ΑΝΑΘΕΣΗ_ΚΩΔ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                            ΩΡΕΣ_ΕΒΔ = d.ΩΡΕΣ_ΕΒΔ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΚΛΑΔΟΣ_ΚΩΔ = d.ΚΛΑΔΟΣ_ΚΩΔ,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΣΥΜΒΑΣΗ = d.ΣΥΜΒΑΣΗ,
                            ΦΥΛΟ = d.ΦΥΛΟ,
                            ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = d.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ
                        }).ToList();
            }
            return data;
        }

        public void Create(AnathesiSupplementViewModel data, int schoolyearId, int schoolId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ();

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = schoolyearId;
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΑΦΜ = data.ΑΦΜ.Trim();
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim();
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ.Trim();
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΩΡΕΣ_ΕΒΔ = data.ΩΡΕΣ_ΕΒΔ;
            entity.ΣΥΜΒΑΣΗ = 1;                 // ωρομίσθιος
            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ";
            entity.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;
            entity.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΦΥΛΟ = Common.GetGenderFromName(data.ΟΝΟΜΑ);

            entities.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΑΝΑΘΕΣΗ_ΚΩΔ = entity.ΑΝΑΘΕΣΗ_ΚΩΔ;
        }

        public void Update(AnathesiSupplementViewModel data, int schoolyearId, int schoolId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ entity = entities.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Find(data.ΑΝΑΘΕΣΗ_ΚΩΔ);

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = schoolyearId;
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΑΦΜ = data.ΑΦΜ.Trim();
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim();
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ.Trim();
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΩΡΕΣ_ΕΒΔ = data.ΩΡΕΣ_ΕΒΔ;
            entity.ΣΥΜΒΑΣΗ = 1;                 // ωρομίσθιος
            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ";
            entity.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΦΥΛΟ = Common.GetGenderFromName(data.ΟΝΟΜΑ);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Update(AnathesiSupplementViewModel data, ApofasiParameters ap)
        {
            ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ entity = entities.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Find(data.ΑΝΑΘΕΣΗ_ΚΩΔ);

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = ap.schoolyearId;
            entity.ΣΧΟΛΗ = ap.schoolId;
            entity.ΑΦΜ = data.ΑΦΜ;
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΩΡΕΣ_ΕΒΔ = data.ΩΡΕΣ_ΕΒΔ;
            entity.ΑΠΟΦΑΣΗ_ΚΩΔ = data.ΑΠΟΦΑΣΗ_ΚΩΔ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(AnathesiSupplementViewModel data)
        {
            ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ entity = entities.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Find(data.ΑΝΑΘΕΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public AnathesiSupplementViewModel Refresh(int entityId)
        {
            return entities.ΑΝΑΘΕΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Select(d => new AnathesiSupplementViewModel
            {
                ΑΝΑΘΕΣΗ_ΚΩΔ = d.ΑΝΑΘΕΣΗ_ΚΩΔ,
                ΑΦΜ = d.ΑΦΜ,
                ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                ΩΡΕΣ_ΕΒΔ = d.ΩΡΕΣ_ΕΒΔ,
                ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΚΛΑΔΟΣ_ΚΩΔ = d.ΚΛΑΔΟΣ_ΚΩΔ,
                ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                ΣΥΜΒΑΣΗ = d.ΣΥΜΒΑΣΗ,
                ΦΥΛΟ = d.ΦΥΛΟ,
                ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = d.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ
            }).Where(d => d.ΑΝΑΘΕΣΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}