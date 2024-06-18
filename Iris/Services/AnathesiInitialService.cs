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
    public class AnathesiInitialService : IAnathesiInitialService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public AnathesiInitialService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<AnathesiInitialViewModel> Read(int schoolyearId, int schoolId)
        {
            var data = (from d in entities.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ
                        orderby d.ΑΝΑΘΕΣΗ_ΚΩΔ descending
                        where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΣΧΟΛΗ == schoolId
                        select new AnathesiInitialViewModel
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

        public IEnumerable<AnathesiInitialViewModel> Read(ApofasiParameters ap)
        {
            List<AnathesiInitialViewModel> data = new List<AnathesiInitialViewModel>();

            if (ap.apofasiId > 0)
            {
                data = (from d in entities.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == ap.apofasiId
                        orderby d.ΑΝΑΘΕΣΗ_ΚΩΔ descending
                        select new AnathesiInitialViewModel
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

        public void Create(AnathesiInitialViewModel data, int schoolyearId, int schoolId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ entity = new ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ();

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = schoolyearId;
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΑΦΜ = data.ΑΦΜ.Trim();
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim();
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ.Trim();
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΩΡΕΣ_ΕΒΔ = data.ΩΡΕΣ_ΕΒΔ;
            entity.ΣΥΜΒΑΣΗ = 1;                 // ωρομίσθιος
            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΡΧΙΚΗ";
            entity.ΑΠΟΦΑΣΗ_ΚΩΔ = 0;
            entity.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΦΥΛΟ = Common.GetGenderFromName(data.ΟΝΟΜΑ);

            entities.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΑΝΑΘΕΣΗ_ΚΩΔ = entity.ΑΝΑΘΕΣΗ_ΚΩΔ;
        }

        public void Update(AnathesiInitialViewModel data, int schoolyearId, int schoolId)
        {
            ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ entity = entities.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Find(data.ΑΝΑΘΕΣΗ_ΚΩΔ);

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = schoolyearId;
            entity.ΣΧΟΛΗ = schoolId;
            entity.ΑΦΜ = data.ΑΦΜ.Trim();
            entity.ΕΠΩΝΥΜΟ = data.ΕΠΩΝΥΜΟ.Trim();
            entity.ΟΝΟΜΑ = data.ΟΝΟΜΑ.Trim();
            entity.ΠΑΤΡΩΝΥΜΟ = data.ΠΑΤΡΩΝΥΜΟ.Trim();
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ;
            entity.ΩΡΕΣ_ΕΒΔ = data.ΩΡΕΣ_ΕΒΔ;
            entity.ΣΥΜΒΑΣΗ = 1;                 // ωρομίσθιος
            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΑΡΧΙΚΗ";
            entity.ΚΛΑΔΟΣ_ΚΩΔ = Common.GetKladosFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = Common.GetWagesCodeFromEidikotita((int)data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ);
            entity.ΦΥΛΟ = Common.GetGenderFromName(data.ΟΝΟΜΑ);

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Update(AnathesiInitialViewModel data, ApofasiParameters ap)
        {
            ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ entity = entities.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Find(data.ΑΝΑΘΕΣΗ_ΚΩΔ);

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

        public void Destroy(AnathesiInitialViewModel data)
        {
            ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ entity = entities.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Find(data.ΑΝΑΘΕΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public AnathesiInitialViewModel Refresh(int entityId)
        {
            return entities.ΑΝΑΘΕΣΕΙΣ_ΑΡΧΙΚΕΣ.Select(item => new AnathesiInitialViewModel
            {
                ΑΝΑΘΕΣΗ_ΚΩΔ = item.ΑΝΑΘΕΣΗ_ΚΩΔ,
                ΑΦΜ = item.ΑΦΜ,
                ΕΠΩΝΥΜΟ = item.ΕΠΩΝΥΜΟ,
                ΟΝΟΜΑ = item.ΟΝΟΜΑ,
                ΠΑΤΡΩΝΥΜΟ = item.ΠΑΤΡΩΝΥΜΟ,
                ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ = item.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔ,
                ΩΡΕΣ_ΕΒΔ = item.ΩΡΕΣ_ΕΒΔ,
                ΣΧΟΛΗ = item.ΣΧΟΛΗ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = item.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΚΛΑΔΟΣ_ΚΩΔ = item.ΚΛΑΔΟΣ_ΚΩΔ,
                ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = item.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                ΑΠΟΦΑΣΗ_ΚΩΔ = item.ΑΠΟΦΑΣΗ_ΚΩΔ,
                ΣΥΜΒΑΣΗ = item.ΣΥΜΒΑΣΗ,
                ΦΥΛΟ = item.ΦΥΛΟ,
                ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = item.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ
            }).Where(d => d.ΑΝΑΘΕΣΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}