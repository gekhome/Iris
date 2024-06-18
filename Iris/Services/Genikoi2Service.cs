using System;
using System.Collections.Generic;
using System.Linq;
using Iris.DAL;
using Iris.Models;
using System.Data.Entity;

namespace Iris.Services
{
    public class Genikoi2Service : IGenikoi2Service, IDisposable
    {
        private readonly IrisDBEntities entities;

        public Genikoi2Service(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<DirectorGeneralViewModel> Read()
        {
            var data = (from d in entities.Δ2_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        select new DirectorGeneralViewModel
                        {
                            ΓΕΝΙΚΟΣ_ΚΩΔ = d.ΓΕΝΙΚΟΣ_ΚΩΔ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΓΕΝΙΚΟΣ = d.ΓΕΝΙΚΟΣ,
                            ΦΥΛΟ = d.ΦΥΛΟ
                        }).ToList();
            return data;
        }

        public void Create(DirectorGeneralViewModel data)
        {
            Δ2_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ entity = new Δ2_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ()
            {
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΓΕΝΙΚΟΣ = data.ΓΕΝΙΚΟΣ,
                ΦΥΛΟ = data.ΦΥΛΟ
            };
            entities.Δ2_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ.Add(entity);
            entities.SaveChanges();

            data.ΓΕΝΙΚΟΣ_ΚΩΔ = entity.ΓΕΝΙΚΟΣ_ΚΩΔ;
        }

        public void Update(DirectorGeneralViewModel data)
        {
            Δ2_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ entity = entities.Δ2_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ.Find(data.ΓΕΝΙΚΟΣ_ΚΩΔ);

            entity.ΓΕΝΙΚΟΣ = data.ΓΕΝΙΚΟΣ;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΦΥΛΟ = data.ΦΥΛΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(DirectorGeneralViewModel data)
        {
            Δ2_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ entity = entities.Δ2_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ.Find(data.ΓΕΝΙΚΟΣ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.Δ2_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public DirectorGeneralViewModel Refresh(int entityId)
        {
            return entities.Δ2_ΔΙΕΥΘΥΝΤΕΣ_ΓΕΝΙΚΟΙ.Select(d => new DirectorGeneralViewModel
            {
                ΓΕΝΙΚΟΣ_ΚΩΔ = d.ΓΕΝΙΚΟΣ_ΚΩΔ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΓΕΝΙΚΟΣ = d.ΓΕΝΙΚΟΣ,
                ΦΥΛΟ = d.ΦΥΛΟ
            }).Where(d => d.ΓΕΝΙΚΟΣ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}