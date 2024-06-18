using System;
using System.Collections.Generic;
using System.Linq;
using Iris.DAL;
using Iris.Models;
using System.Data.Entity;

namespace Iris.Services
{
    public class Antiproedroi2Service : IAntiproedroi2Service, IDisposable
    {
        private readonly IrisDBEntities entities;

        public Antiproedroi2Service(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<AntiproedrosViewModel> Read()
        {
            var data = (from d in entities.Δ2_ΑΝΤΙΠΡΟΕΔΡΟΙ
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        select new AntiproedrosViewModel
                        {
                            ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ = d.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΑΝΤΙΠΡΟΕΔΡΟΣ = d.ΑΝΤΙΠΡΟΕΔΡΟΣ,
                            ΒΑΘΜΟΣ = d.ΒΑΘΜΟΣ,
                            ΦΥΛΟ = d.ΦΥΛΟ
                        }).ToList();
            return data;
        }

        public void Create(AntiproedrosViewModel data)
        {
            Δ2_ΑΝΤΙΠΡΟΕΔΡΟΙ entity = new Δ2_ΑΝΤΙΠΡΟΕΔΡΟΙ()
            {
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΑΝΤΙΠΡΟΕΔΡΟΣ = data.ΑΝΤΙΠΡΟΕΔΡΟΣ,
                ΒΑΘΜΟΣ = data.ΒΑΘΜΟΣ,
                ΦΥΛΟ = data.ΦΥΛΟ
            };
            entities.Δ2_ΑΝΤΙΠΡΟΕΔΡΟΙ.Add(entity);
            entities.SaveChanges();

            data.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ = entity.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ;
        }

        public void Update(AntiproedrosViewModel data)
        {
            Δ2_ΑΝΤΙΠΡΟΕΔΡΟΙ entity = entities.Δ2_ΑΝΤΙΠΡΟΕΔΡΟΙ.Find(data.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ);

            entity.ΑΝΤΙΠΡΟΕΔΡΟΣ = data.ΑΝΤΙΠΡΟΕΔΡΟΣ;
            entity.ΒΑΘΜΟΣ = data.ΒΑΘΜΟΣ;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΦΥΛΟ = data.ΦΥΛΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(AntiproedrosViewModel data)
        {
            Δ2_ΑΝΤΙΠΡΟΕΔΡΟΙ entity = entities.Δ2_ΑΝΤΙΠΡΟΕΔΡΟΙ.Find(data.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.Δ2_ΑΝΤΙΠΡΟΕΔΡΟΙ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public AntiproedrosViewModel Refresh(int entityId)
        {
            return entities.Δ2_ΑΝΤΙΠΡΟΕΔΡΟΙ.Select(d => new AntiproedrosViewModel
            {
                ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ = d.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΑΝΤΙΠΡΟΕΔΡΟΣ = d.ΑΝΤΙΠΡΟΕΔΡΟΣ,
                ΒΑΘΜΟΣ = d.ΒΑΘΜΟΣ,
                ΦΥΛΟ = d.ΦΥΛΟ
            }).Where(d => d.ΑΝΤΙΠΡΟΕΔΡΟΣ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}