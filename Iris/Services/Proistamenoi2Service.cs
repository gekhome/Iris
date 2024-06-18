using System;
using System.Collections.Generic;
using System.Linq;
using Iris.DAL;
using Iris.Models;
using System.Data.Entity;

namespace Iris.Services
{
    public class Proistamenoi2Service : IProistamenoi2Service, IDisposable
    {
        private readonly IrisDBEntities entities;

        public Proistamenoi2Service(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ProistamenosViewModel> Read()
        {
            var data = (from d in entities.Δ2_ΠΡΟΙΣΤΑΜΕΝΟΙ
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        select new ProistamenosViewModel
                        {
                            ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ = d.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΠΡΟΙΣΤΑΜΕΝΟΣ = d.ΠΡΟΙΣΤΑΜΕΝΟΣ,
                            ΦΥΛΟ = d.ΦΥΛΟ
                        }).ToList();
            return data;
        }

        public void Create(ProistamenosViewModel data)
        {
            Δ2_ΠΡΟΙΣΤΑΜΕΝΟΙ entity = new Δ2_ΠΡΟΙΣΤΑΜΕΝΟΙ()
            {
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΠΡΟΙΣΤΑΜΕΝΟΣ = data.ΠΡΟΙΣΤΑΜΕΝΟΣ,
                ΦΥΛΟ = data.ΦΥΛΟ
            };
            entities.Δ2_ΠΡΟΙΣΤΑΜΕΝΟΙ.Add(entity);
            entities.SaveChanges();

            data.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ = entity.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ;
        }

        public void Update(ProistamenosViewModel data)
        {
            Δ2_ΠΡΟΙΣΤΑΜΕΝΟΙ entity = entities.Δ2_ΠΡΟΙΣΤΑΜΕΝΟΙ.Find(data.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ);

            entity.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ = data.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ;
            entity.ΠΡΟΙΣΤΑΜΕΝΟΣ = data.ΠΡΟΙΣΤΑΜΕΝΟΣ;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΦΥΛΟ = data.ΦΥΛΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ProistamenosViewModel data)
        {
            Δ2_ΠΡΟΙΣΤΑΜΕΝΟΙ entity = entities.Δ2_ΠΡΟΙΣΤΑΜΕΝΟΙ.Find(data.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.Δ2_ΠΡΟΙΣΤΑΜΕΝΟΙ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public ProistamenosViewModel Refresh(int entityId)
        {
            return entities.Δ2_ΠΡΟΙΣΤΑΜΕΝΟΙ.Select(d => new ProistamenosViewModel
            {
                ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ = d.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΠΡΟΙΣΤΑΜΕΝΟΣ = d.ΠΡΟΙΣΤΑΜΕΝΟΣ,
                ΦΥΛΟ = d.ΦΥΛΟ
            }).Where(d => d.ΠΡΟΙΣΤΑΜΕΝΟΣ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}