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
    public class AnathesiCancelService : IAnathesiCancelService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public AnathesiCancelService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<AnathesiCancelViewModel> Read(int schoolyearId, int schoolId)
        {
            var data = (from d in entities.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ
                        orderby d.ΑΝΑΘΕΣΗ_ΚΩΔ descending
                        where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΣΧΟΛΗ == schoolId
                        select new AnathesiCancelViewModel
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

        public IEnumerable<AnathesiCancelViewModel> Read(ApofasiParameters ap)
        {
            List<AnathesiCancelViewModel> data = new List<AnathesiCancelViewModel>();

            if (ap.apofasiId > 0)
            {
                data = (from d in entities.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId
                        orderby d.ΑΝΑΘΕΣΗ_ΚΩΔ descending
                        select new AnathesiCancelViewModel
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

        public void Create(AnathesiCancelViewModel data, int schoolyearId, int schoolId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ();

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = schoolyearId;
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΑΦΜ = data.ΑΦΜ.Trim();
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim();
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ.Trim();
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΩΡΕΣ_ΕΒΔ_ΑΠΟ = data.ΩΡΕΣ_ΕΒΔ_ΑΠΟ;
            entity.ΩΡΕΣ_ΕΒΔ_ΣΕ = 0;
            entity.ΣΥΜΒΑΣΗ = 1;                 // ωρομίσθιος
            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΚΥΡΩΤΙΚΗ";
            entity.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;
            entity.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΦΥΛΟ = Common.GetGenderFromName(data.ΟΝΟΜΑ);

            entities.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΑΝΑΘΕΣΗ_ΚΩΔ = entity.ΑΝΑΘΕΣΗ_ΚΩΔ;
        }

        public void Update(AnathesiCancelViewModel data, int schoolyearId, int schoolId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ entity = entities.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Find(data.ΑΝΑΘΕΣΗ_ΚΩΔ);

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = schoolyearId;
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΑΦΜ = data.ΑΦΜ.Trim();
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim();
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ.Trim();
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΩΡΕΣ_ΕΒΔ_ΑΠΟ = data.ΩΡΕΣ_ΕΒΔ_ΑΠΟ;
            entity.ΩΡΕΣ_ΕΒΔ_ΣΕ = 0;
            entity.ΣΥΜΒΑΣΗ = 1;                 // ωρομίσθιος
            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΚΥΡΩΤΙΚΗ";
            entity.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΦΥΛΟ = Common.GetGenderFromName(data.ΟΝΟΜΑ);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Update(AnathesiCancelViewModel data, ApofasiParameters ap)
        {
            ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ entity = entities.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Find(data.ΑΝΑΘΕΣΗ_ΚΩΔ);

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = ap.schoolyearId;
            entity.ΣΧΟΛΗ = ap.schoolId;
            entity.ΑΦΜ = data.ΑΦΜ;
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ;
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ;
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΩΡΕΣ_ΕΒΔ_ΑΠΟ = data.ΩΡΕΣ_ΕΒΔ_ΑΠΟ;
            entity.ΩΡΕΣ_ΕΒΔ_ΣΕ = 0;
            entity.ΑΠΟΦΑΣΗ_ΚΩΔ = data.ΑΠΟΦΑΣΗ_ΚΩΔ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(AnathesiCancelViewModel data)
        {
            ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ entity = entities.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Find(data.ΑΝΑΘΕΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public AnathesiCancelViewModel Refresh(int entityId)
        {
            return entities.ΑΝΑΘΕΣΕΙΣ_ΑΚΥΡΩΤΙΚΕΣ.Select(d => new AnathesiCancelViewModel
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