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
    public class EpitropesApofasiService : IEpitropesApofasiService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public EpitropesApofasiService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SysEpitropesViewModel> Read(int schoolyearId, int periferiakiId)
        {
            var data = (from d in entities.ΣΥΣ_ΕΠΙΤΡΟΠΕΣ_ΑΒ
                        where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΠΕΡΙΦΕΡΕΙΑΚΗ == periferiakiId
                        select new SysEpitropesViewModel
                        {
                            ΕΠΙΤΡΟΠΗ_ΚΩΔ = d.ΕΠΙΤΡΟΠΗ_ΚΩΔ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ,
                            ΠΡΩΤΟΒΑΘΜΙΑ = d.ΠΡΩΤΟΒΑΘΜΙΑ,
                            ΔΕΥΤΕΡΟΒΑΘΜΙΑ = d.ΔΕΥΤΕΡΟΒΑΘΜΙΑ
                        }).ToList();

            return data;
        }

        public void Create(SysEpitropesViewModel data, int schoolyearId, int periferiakiId)
        {
            ΣΥΣ_ΕΠΙΤΡΟΠΕΣ_ΑΒ entity = new ΣΥΣ_ΕΠΙΤΡΟΠΕΣ_ΑΒ();

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = schoolyearId;
            entity.ΠΕΡΙΦΕΡΕΙΑΚΗ = periferiakiId;
            entity.ΠΡΩΤΟΒΑΘΜΙΑ = data.ΠΡΩΤΟΒΑΘΜΙΑ;
            entity.ΔΕΥΤΕΡΟΒΑΘΜΙΑ = data.ΔΕΥΤΕΡΟΒΑΘΜΙΑ;

            entities.ΣΥΣ_ΕΠΙΤΡΟΠΕΣ_ΑΒ.Add(entity);
            entities.SaveChanges();

            data.ΕΠΙΤΡΟΠΗ_ΚΩΔ = entity.ΕΠΙΤΡΟΠΗ_ΚΩΔ;
        }

        public void Update(SysEpitropesViewModel data, int schoolyearId, int periferiakiId)
        {
            ΣΥΣ_ΕΠΙΤΡΟΠΕΣ_ΑΒ entity = entities.ΣΥΣ_ΕΠΙΤΡΟΠΕΣ_ΑΒ.Find(data.ΕΠΙΤΡΟΠΗ_ΚΩΔ);

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = schoolyearId;
            entity.ΠΕΡΙΦΕΡΕΙΑΚΗ = periferiakiId;
            entity.ΠΡΩΤΟΒΑΘΜΙΑ = data.ΠΡΩΤΟΒΑΘΜΙΑ;
            entity.ΔΕΥΤΕΡΟΒΑΘΜΙΑ = data.ΔΕΥΤΕΡΟΒΑΘΜΙΑ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SysEpitropesViewModel data)
        {
            ΣΥΣ_ΕΠΙΤΡΟΠΕΣ_ΑΒ entity = entities.ΣΥΣ_ΕΠΙΤΡΟΠΕΣ_ΑΒ.Find(data.ΕΠΙΤΡΟΠΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΕΠΙΤΡΟΠΕΣ_ΑΒ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SysEpitropesViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΕΠΙΤΡΟΠΕΣ_ΑΒ.Select(d => new SysEpitropesViewModel
            {
                ΕΠΙΤΡΟΠΗ_ΚΩΔ = d.ΕΠΙΤΡΟΠΗ_ΚΩΔ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ,
                ΠΡΩΤΟΒΑΘΜΙΑ = d.ΠΡΩΤΟΒΑΘΜΙΑ,
                ΔΕΥΤΕΡΟΒΑΘΜΙΑ = d.ΔΕΥΤΕΡΟΒΑΘΜΙΑ
            }).Where(d => d.ΕΠΙΤΡΟΠΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}