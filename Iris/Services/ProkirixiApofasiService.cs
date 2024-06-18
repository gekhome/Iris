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
    public class ProkirixiApofasiService : IProkirixiApofasiService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public ProkirixiApofasiService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ProkirixiApofasiGridViewModel> Read()
        {
            var data = (from d in entities.ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ
                        select new ProkirixiApofasiGridViewModel
                        {
                            ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ = d.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ,
                            ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ = d.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ,
                        }).ToList();

            return data;
        }

        public void Create(ProkirixiApofasiGridViewModel data)
        {
            ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ entity = new ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ();

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ = data.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ;
            entity.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ = data.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ;

            entities.ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ.Add(entity);
            entities.SaveChanges();

            data.ΚΩΔΙΚΟΣ = entity.ΚΩΔΙΚΟΣ;
        }

        public void Update(ProkirixiApofasiGridViewModel data)
        {
            ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ entity = entities.ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ.Find(data.ΚΩΔΙΚΟΣ);

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ = data.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ;
            entity.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ = data.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ProkirixiApofasiGridViewModel data)
        {
            ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ entity = entities.ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ.Find(data.ΚΩΔΙΚΟΣ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public ProkirixiApofasiGridViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ.Select(d => new ProkirixiApofasiGridViewModel
            {
                ΚΩΔΙΚΟΣ = d.ΚΩΔΙΚΟΣ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ = d.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΕΠΑΣ,
                ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ = d.ΠΡΟΚΗΡΥΞΗ_ΑΠΟΦΑΣΗ_ΙΕΚ,
            }).Where(d => d.ΚΩΔΙΚΟΣ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}