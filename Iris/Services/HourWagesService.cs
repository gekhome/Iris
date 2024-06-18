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
    public class HourWagesService : IHourWagesService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public HourWagesService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SysHourWagesViewModel> Read()
        {
            var data = (from d in entities.ΣΥΣ_ΩΡΟΜΙΣΘΙΑ
                        select new SysHourWagesViewModel
                        {
                            ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = d.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ,
                            ΩΡΟΜΙΣΘΙΟ = d.ΩΡΟΜΙΣΘΙΟ,
                        }).ToList();

            return data;
        }

        public void Create(SysHourWagesViewModel data)
        {
            ΣΥΣ_ΩΡΟΜΙΣΘΙΑ entity = new ΣΥΣ_ΩΡΟΜΙΣΘΙΑ();

            entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;
            entity.ΩΡΟΜΙΣΘΙΟ = data.ΩΡΟΜΙΣΘΙΟ;

            entities.ΣΥΣ_ΩΡΟΜΙΣΘΙΑ.Add(entity);
            entities.SaveChanges();

            data.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = entity.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ;
        }

        public void Update(SysHourWagesViewModel data)
        {
            ΣΥΣ_ΩΡΟΜΙΣΘΙΑ entity = entities.ΣΥΣ_ΩΡΟΜΙΣΘΙΑ.Find(data.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ);

            entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;
            entity.ΩΡΟΜΙΣΘΙΟ = data.ΩΡΟΜΙΣΘΙΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SysHourWagesViewModel data)
        {
            ΣΥΣ_ΩΡΟΜΙΣΘΙΑ entity = entities.ΣΥΣ_ΩΡΟΜΙΣΘΙΑ.Find(data.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΩΡΟΜΙΣΘΙΑ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SysHourWagesViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΩΡΟΜΙΣΘΙΑ.Select(d => new SysHourWagesViewModel
            {
                ΩΡΟΜΙΣΘΙΟ_ΚΩΔ = d.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ,
                ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ,
                ΩΡΟΜΙΣΘΙΟ = d.ΩΡΟΜΙΣΘΙΟ
            }).Where(d => d.ΩΡΟΜΙΣΘΙΟ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}