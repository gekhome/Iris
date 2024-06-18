using System;
using System.Collections.Generic;
using System.Linq;
using Iris.DAL;
using Iris.Models;
using System.Data.Entity;

namespace Iris.Services
{
    public class Directors2Service : IDirectors2Service, IDisposable
    {
        private readonly IrisDBEntities entities;

        public Directors2Service(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<DirectorViewModel> Read()
        {
            var data = (from d in entities.Δ2_ΔΙΕΥΘΥΝΤΕΣ
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        select new DirectorViewModel
                        {
                            ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ = d.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΔΙΕΥΘΥΝΤΗΣ = d.ΔΙΕΥΘΥΝΤΗΣ,
                            ΦΥΛΟ = d.ΦΥΛΟ
                        }).ToList();
            return data;
        }

        public void Create(DirectorViewModel data)
        {
            Δ2_ΔΙΕΥΘΥΝΤΕΣ entity = new Δ2_ΔΙΕΥΘΥΝΤΕΣ()
            {
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΔΙΕΥΘΥΝΤΗΣ = data.ΔΙΕΥΘΥΝΤΗΣ,
                ΦΥΛΟ = data.ΦΥΛΟ
            };
            entities.Δ2_ΔΙΕΥΘΥΝΤΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ = entity.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ;
        }

        public void Update(DirectorViewModel data)
        {
            Δ2_ΔΙΕΥΘΥΝΤΕΣ entity = entities.Δ2_ΔΙΕΥΘΥΝΤΕΣ.Find(data.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ);

            entity.ΔΙΕΥΘΥΝΤΗΣ = data.ΔΙΕΥΘΥΝΤΗΣ;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΦΥΛΟ = data.ΦΥΛΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(DirectorViewModel data)
        {
            Δ2_ΔΙΕΥΘΥΝΤΕΣ entity = entities.Δ2_ΔΙΕΥΘΥΝΤΕΣ.Find(data.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.Δ2_ΔΙΕΥΘΥΝΤΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public DirectorViewModel Refresh(int entityId)
        {
            return entities.Δ2_ΔΙΕΥΘΥΝΤΕΣ.Select(d => new DirectorViewModel
            {
                ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ = d.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΔΙΕΥΘΥΝΤΗΣ = d.ΔΙΕΥΘΥΝΤΗΣ,
                ΦΥΛΟ = d.ΦΥΛΟ
            }).Where(d => d.ΔΙΕΥΘΥΝΤΗΣ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}