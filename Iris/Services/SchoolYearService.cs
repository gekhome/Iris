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
    public class SchoolYearService : ISchoolYearService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public SchoolYearService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SysSchoolYearViewModel> Read()
        {
            var data = (from d in entities.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ
                        select new SysSchoolYearViewModel
                        {
                            SCHOOLYEAR_ID = d.SCHOOLYEAR_ID,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΗΜΝΙΑ_ΕΝΑΡΞΗ = d.ΗΜΝΙΑ_ΕΝΑΡΞΗ,
                            ΗΜΝΙΑ_ΛΗΞΗ = d.ΗΜΝΙΑ_ΛΗΞΗ,
                        }).ToList();

            return data;
        }

        public void Create(SysSchoolYearViewModel data)
        {
            ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ entity = new ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ();

            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΗΜΝΙΑ_ΕΝΑΡΞΗ = data.ΗΜΝΙΑ_ΕΝΑΡΞΗ;
            entity.ΗΜΝΙΑ_ΛΗΞΗ = data.ΗΜΝΙΑ_ΛΗΞΗ;

            entities.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Add(entity);
            entities.SaveChanges();

            data.SCHOOLYEAR_ID = entity.SCHOOLYEAR_ID;
        }

        public void Update(SysSchoolYearViewModel data)
        {
            ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ entity = entities.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Find(data.SCHOOLYEAR_ID);

            entity.SCHOOLYEAR_ID = data.SCHOOLYEAR_ID;
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΗΜΝΙΑ_ΕΝΑΡΞΗ = data.ΗΜΝΙΑ_ΕΝΑΡΞΗ;
            entity.ΗΜΝΙΑ_ΛΗΞΗ = data.ΗΜΝΙΑ_ΛΗΞΗ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SysSchoolYearViewModel data)
        {
            ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ entity = entities.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Find(data.SCHOOLYEAR_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SysSchoolYearViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΣΧΟΛΙΚΑ_ΕΤΗ.Select(d => new SysSchoolYearViewModel
            {
                SCHOOLYEAR_ID = d.SCHOOLYEAR_ID,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΗΜΝΙΑ_ΕΝΑΡΞΗ = d.ΗΜΝΙΑ_ΕΝΑΡΞΗ,
                ΗΜΝΙΑ_ΛΗΞΗ = d.ΗΜΝΙΑ_ΛΗΞΗ
            }).Where(d => d.SCHOOLYEAR_ID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}