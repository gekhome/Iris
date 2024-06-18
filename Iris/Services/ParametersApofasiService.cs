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
    public class ParametersApofasiService : IParametersApofasiService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public ParametersApofasiService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ApofasiParametersGridViewModel> Read()
        {
            var data = (from d in entities.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ
                        select new ApofasiParametersGridViewModel
                        {
                            ID = d.ID,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = d.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ,
                            ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ = d.ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ,
                            ΠΥΣ_ΑΡΘΡΟ = d.ΠΥΣ_ΑΡΘΡΟ
                        }).ToList();

            return data;
        }

        public void Create(ApofasiParametersGridViewModel data)
        {
            ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ entity = new ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ();

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
            entity.ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ = data.ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ;
            entity.ΠΥΣ_ΑΡΘΡΟ = data.ΠΥΣ_ΑΡΘΡΟ;

            entities.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ.Add(entity);
            entities.SaveChanges();

            data.ID = entity.ID;
        }

        public void Update(ApofasiParametersGridViewModel data)
        {
            ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ entity = entities.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ.Find(data.ID);

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
            entity.ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ = data.ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ;
            entity.ΠΥΣ_ΑΡΘΡΟ = data.ΠΥΣ_ΑΡΘΡΟ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ApofasiParametersGridViewModel data)
        {
            ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ entity = entities.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ.Find(data.ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public ApofasiParametersGridViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΠΑΡΑΜΕΤΡΟΙ_ΑΠΟΦΑΣΕΙΣ.Select(d => new ApofasiParametersGridViewModel
            {
                ID = d.ID,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = d.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ,
                ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ = d.ΑΠΟΦΑΣΗ_ΕΓΚΡΙΤΙΚΗ,
                ΠΥΣ_ΑΡΘΡΟ = d.ΠΥΣ_ΑΡΘΡΟ
            }).Where(d => d.ID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}