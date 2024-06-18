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
    public class AnathesiModifyService : IAnathesiModifyService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public AnathesiModifyService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<AnathesiModifyViewModel> Read(int schoolyearId, int schoolId)
        {
            var data = (from d in entities.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ
                        orderby d.ΑΝΑΘΕΣΗ_ΚΩΔ descending
                        where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΣΧΟΛΗ == schoolId
                        select new AnathesiModifyViewModel
                        {
                            ΑΝΑΘΕΣΗ_ΚΩΔ = d.ΑΝΑΘΕΣΗ_ΚΩΔ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                            ΩΡΕΣ_ΕΒΔ_ΑΠΟ = d.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                            ΩΡΕΣ_ΕΒΔ_ΣΕ = d.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΣΥΜΒΑΣΗ = d.ΣΥΜΒΑΣΗ,
                            ΚΛΑΔΟΣ_ΚΩΔ = d.ΚΛΑΔΟΣ_ΚΩΔ,
                            ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = d.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                            ΦΥΛΟ = d.ΦΥΛΟ
                        }).ToList();

            return data;
        }

        public IEnumerable<AnathesiModifyViewModel> Read(ApofasiParameters ap)
        {
            List<AnathesiModifyViewModel> data = new List<AnathesiModifyViewModel>();

            if (ap.apofasiId > 0)
            {
                data = (from d in entities.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId
                        orderby d.ΑΝΑΘΕΣΗ_ΚΩΔ descending
                        select new AnathesiModifyViewModel
                        {
                            ΑΝΑΘΕΣΗ_ΚΩΔ = d.ΑΝΑΘΕΣΗ_ΚΩΔ,
                            ΑΦΜ = d.ΑΦΜ,
                            ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                            ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                            ΩΡΕΣ_ΕΒΔ_ΑΠΟ = d.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                            ΩΡΕΣ_ΕΒΔ_ΣΕ = d.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΣΥΜΒΑΣΗ = d.ΣΥΜΒΑΣΗ,
                        }).ToList();
            }
            return data;
        }

        public void Create(AnathesiModifyViewModel data, int schoolyearId, int schoolId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ();

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = schoolyearId;
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΑΦΜ = data.ΑΦΜ.Trim();
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim();
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ.Trim();
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΩΡΕΣ_ΕΒΔ_ΑΠΟ = data.ΩΡΕΣ_ΕΒΔ_ΑΠΟ;
            entity.ΩΡΕΣ_ΕΒΔ_ΣΕ = data.ΩΡΕΣ_ΕΒΔ_ΣΕ;
            entity.ΣΥΜΒΑΣΗ = 1;                 // ωρομίσθιος
            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ";
            entity.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;
            entity.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΦΥΛΟ = Common.GetGenderFromName(data.ΟΝΟΜΑ);

            entities.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΑΝΑΘΕΣΗ_ΚΩΔ = entity.ΑΝΑΘΕΣΗ_ΚΩΔ;
        }

        public void Update(AnathesiModifyViewModel data, int schoolyearId, int schoolId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ entity = entities.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Find(data.ΑΝΑΘΕΣΗ_ΚΩΔ);

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = schoolyearId;
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΑΦΜ = data.ΑΦΜ.Trim();
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim();
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ.Trim();
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΩΡΕΣ_ΕΒΔ_ΑΠΟ = data.ΩΡΕΣ_ΕΒΔ_ΑΠΟ;
            entity.ΩΡΕΣ_ΕΒΔ_ΣΕ = data.ΩΡΕΣ_ΕΒΔ_ΣΕ;
            entity.ΣΥΜΒΑΣΗ = 1;                 // ωρομίσθιος
            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ";
            entity.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΦΥΛΟ = Common.GetGenderFromName(data.ΟΝΟΜΑ);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Update(AnathesiModifyViewModel data, ApofasiParameters ap)
        {
            ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ entity = entities.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Find(data.ΑΝΑΘΕΣΗ_ΚΩΔ);

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = ap.schoolyearId;
            entity.ΣΧΟΛΗ = ap.schoolId;
            entity.ΑΦΜ = data.ΑΦΜ;
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΩΡΕΣ_ΕΒΔ_ΑΠΟ = data.ΩΡΕΣ_ΕΒΔ_ΑΠΟ;
            entity.ΩΡΕΣ_ΕΒΔ_ΣΕ = data.ΩΡΕΣ_ΕΒΔ_ΣΕ;
            entity.ΑΠΟΦΑΣΗ_ΚΩΔ = data.ΑΠΟΦΑΣΗ_ΚΩΔ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(AnathesiModifyViewModel data)
        {
            ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ entity = entities.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Find(data.ΑΝΑΘΕΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public AnathesiModifyViewModel Refresh(int entityId)
        {
            return entities.ΑΝΑΘΕΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ.Select(d => new AnathesiModifyViewModel
            {
                ΑΝΑΘΕΣΗ_ΚΩΔ = d.ΑΝΑΘΕΣΗ_ΚΩΔ,
                ΑΦΜ = d.ΑΦΜ,
                ΕΠΩΝΥΜΟ = d.ΕΠΩΝΥΜΟ,
                ΟΝΟΜΑ = d.ΟΝΟΜΑ,
                ΠΑΤΡΩΝΥΜΟ = d.ΠΑΤΡΩΝΥΜΟ,
                ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                ΩΡΕΣ_ΕΒΔ_ΑΠΟ = d.ΩΡΕΣ_ΕΒΔ_ΑΠΟ,
                ΩΡΕΣ_ΕΒΔ_ΣΕ = d.ΩΡΕΣ_ΕΒΔ_ΣΕ,
                ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                ΣΥΜΒΑΣΗ = d.ΣΥΜΒΑΣΗ,
                ΚΛΑΔΟΣ_ΚΩΔ = d.ΚΛΑΔΟΣ_ΚΩΔ,
                ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = d.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                ΦΥΛΟ = d.ΦΥΛΟ
            }).Where(d => d.ΑΝΑΘΕΣΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}