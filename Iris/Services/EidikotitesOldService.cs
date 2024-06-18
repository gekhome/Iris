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
    public class EidikotitesOldService : IEidikotitesOldService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public EidikotitesOldService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SysEidikotitesOldViewModel> Read()
        {
            var data = (from d in entities.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ
                        orderby d.ΚΛΑΔΟΣ, d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ
                        select new SysEidikotitesOldViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ,
                            ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = d.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
                        }).ToList();

            return data;
        }

        public void Create(SysEidikotitesOldViewModel data)
        {
            ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ entity = new ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ();

            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = data.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ;
            entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;

            entities.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΚΩΔΙΚΟΣ = entity.ΚΩΔΙΚΟΣ;
        }

        public void Update(SysEidikotitesOldViewModel data)
        {
            ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ entity = entities.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ.Find(data.ΚΩΔΙΚΟΣ);

            entity.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = data.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ;
            entity.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = data.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ;
            entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SysEidikotitesOldViewModel data)
        {
            ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ entity = entities.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ.Find(data.ΚΩΔΙΚΟΣ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SysEidikotitesOldViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΕΙΔΙΚΟΤΗΤΕΣ_ΠΑΛΙΕΣ.Select(d => new SysEidikotitesOldViewModel
            {
                ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ = d.ΕΙΔΙΚΟΤΗΤΑ_ΚΩΔΙΚΟΣ,
                ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ = d.ΕΙΔΙΚΟΤΗΤΑ_ΛΕΚΤΙΚΟ,
                ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ
            }).Where(d => d.ΚΩΔΙΚΟΣ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}