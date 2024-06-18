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
    public class KladoiHoursService : IKladoiHoursService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public KladoiHoursService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SysKladosViewModel> Read()
        {
            var data = (from d in entities.ΣΥΣ_ΚΛΑΔΟΙ
                        select new SysKladosViewModel
                        {
                            ΚΛΑΔΟΣ_ΚΩΔ = d.ΚΛΑΔΟΣ_ΚΩΔ,
                            ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ,
                            ΜΙΣΘΟΣ = d.ΜΙΣΘΟΣ,
                            ΩΡΑΡΙΟ = d.ΩΡΑΡΙΟ,
                        }).ToList();

            return data;
        }

        public void Create(SysKladosViewModel data)
        {
            ΣΥΣ_ΚΛΑΔΟΙ entity = new ΣΥΣ_ΚΛΑΔΟΙ();

            entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;
            entity.ΩΡΑΡΙΟ = data.ΩΡΑΡΙΟ;
            entity.ΜΙΣΘΟΣ = data.ΜΙΣΘΟΣ;

            entities.ΣΥΣ_ΚΛΑΔΟΙ.Add(entity);
            entities.SaveChanges();

            data.ΚΛΑΔΟΣ_ΚΩΔ = entity.ΚΛΑΔΟΣ_ΚΩΔ;
        }

        public void Update(SysKladosViewModel data)
        {
            ΣΥΣ_ΚΛΑΔΟΙ entity = entities.ΣΥΣ_ΚΛΑΔΟΙ.Find(data.ΚΛΑΔΟΣ_ΚΩΔ);

            entity.ΚΛΑΔΟΣ = data.ΚΛΑΔΟΣ;
            entity.ΩΡΑΡΙΟ = data.ΩΡΑΡΙΟ;
            entity.ΜΙΣΘΟΣ = data.ΜΙΣΘΟΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SysKladosViewModel data)
        {
            ΣΥΣ_ΚΛΑΔΟΙ entity = entities.ΣΥΣ_ΚΛΑΔΟΙ.Find(data.ΚΛΑΔΟΣ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΚΛΑΔΟΙ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SysKladosViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΚΛΑΔΟΙ.Select(d => new SysKladosViewModel
            {
                ΚΛΑΔΟΣ_ΚΩΔ = d.ΚΛΑΔΟΣ_ΚΩΔ,
                ΚΛΑΔΟΣ = d.ΚΛΑΔΟΣ,
                ΜΙΣΘΟΣ = d.ΜΙΣΘΟΣ,
                ΩΡΑΡΙΟ = d.ΩΡΑΡΙΟ
            }).Where(d => d.ΚΛΑΔΟΣ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}