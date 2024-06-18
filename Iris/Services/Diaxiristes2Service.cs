﻿using System;
using System.Collections.Generic;
using System.Linq;
using Iris.DAL;
using Iris.Models;
using System.Data.Entity;

namespace Iris.Services
{
    public class Diaxiristes2Service : IDiaxiristes2Service, IDisposable
    {
        private readonly IrisDBEntities entities;

        public Diaxiristes2Service(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<DiaxiristisViewModel> Read()
        {
            var data = (from d in entities.Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ
                        orderby d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ
                        select new DiaxiristisViewModel
                        {
                            ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                            ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ,
                            ΤΗΛΕΦΩΝΟ = d.ΤΗΛΕΦΩΝΟ,
                            ΦΑΞ = d.ΦΑΞ,
                            ΦΥΛΟ = d.ΦΥΛΟ
                        }).ToList();
            return data;
        }

        public void Create(DiaxiristisViewModel data)
        {
            Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ entity = new Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ()
            {
                ΟΝΟΜΑΤΕΠΩΝΥΜΟ = data.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ = data.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ,
                ΤΗΛΕΦΩΝΟ = data.ΤΗΛΕΦΩΝΟ,
                ΦΑΞ = data.ΦΑΞ,
                ΦΥΛΟ = data.ΦΥΛΟ
            };
            entities.Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ = entity.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ;
        }

        public void Update(DiaxiristisViewModel data)
        {
            Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ entity = entities.Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ.Find(data.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ);

            entity.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ = data.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ;
            entity.ΟΝΟΜΑΤΕΠΩΝΥΜΟ = data.ΟΝΟΜΑΤΕΠΩΝΥΜΟ;
            entity.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ = data.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ;
            entity.ΤΗΛΕΦΩΝΟ = data.ΤΗΛΕΦΩΝΟ;
            entity.ΦΑΞ = data.ΦΑΞ;
            entity.ΦΥΛΟ = data.ΦΥΛΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(DiaxiristisViewModel data)
        {
            Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ entity = entities.Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ.Find(data.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public DiaxiristisViewModel Refresh(int entityId)
        {
            return entities.Δ2_ΔΙΑΧΕΙΡΙΣΤΕΣ.Select(d => new DiaxiristisViewModel
            {
                ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ,
                ΟΝΟΜΑΤΕΠΩΝΥΜΟ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ,
                ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ = d.ΟΝΟΜΑΤΕΠΩΝΥΜΟ_ΠΛΗΡΕΣ,
                ΤΗΛΕΦΩΝΟ = d.ΤΗΛΕΦΩΝΟ,
                ΦΑΞ = d.ΦΑΞ,
                ΦΥΛΟ = d.ΦΥΛΟ
            }).Where(d => d.ΔΙΑΧΕΙΡΙΣΤΗΣ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}