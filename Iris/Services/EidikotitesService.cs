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
    public class EidikotitesService : IEidikotitesService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public EidikotitesService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SysEidikotitesViewModel> Read()
        {
            var data = (from d in entities.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ
                        orderby d.ΚΛΑΔΟΣ, d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ
                        select new SysEidikotitesViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = d.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ1 = d.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ1,
                            ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ = d.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ,
                            ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
                        }).ToList();

            return data;
        }

        public void Create(SysEidikotitesViewModel data)
        {
            ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ entity = new ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ();

            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = data.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ1 = data.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ1;
            entity.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ = Common.GetKladosEniaiosText((int)data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ);
            entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;

            entities.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΚΩΔΙΚΟΣ = entity.ΚΩΔΙΚΟΣ;
        }

        public void Update(SysEidikotitesViewModel data)
        {
            ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ entity = entities.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Find(data.ΚΩΔΙΚΟΣ);

            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = data.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ1 = data.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ1;
            entity.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ = Common.GetKladosEniaiosText((int)data.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ);
            entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SysEidikotitesViewModel data)
        {
            ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ entity = entities.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Find(data.ΚΩΔΙΚΟΣ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SysEidikotitesViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ.Select(d => new SysEidikotitesViewModel
            {
                ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ,
                ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = d.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ,
                ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ1 = d.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ1,
                ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ = d.ΕΙΔΙΚΟΤΗΤΑ_ΕΝΙΑΙΑ,
                ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ = d.ΚΛΑΔΟΣ_ΕΝΙΑΙΟΣ,
                ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
            }).Where(d => d.ΚΩΔΙΚΟΣ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}