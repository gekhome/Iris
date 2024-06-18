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
    public class PeriferiakesService : IPeriferiakesService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public PeriferiakesService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SysPeriferiakiViewModel> Read()
        {
            var data = (from d in entities.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ
                        orderby d.ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ
                        select new SysPeriferiakiViewModel
                        {
                            ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ = d.ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ,
                            ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ = d.ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ,
                            ΤΑΧ_ΔΙΕΥΘΥΝΣΗ = d.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ,
                            ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                            FAX = d.FAX,
                            EMAIL = d.EMAIL
                        }).ToList();

            return data;
        }

        public void Create(SysPeriferiakiViewModel data)
        {
            ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ entity = new ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ();

            entity.ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ = data.ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ;
            entity.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ = data.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ;
            entity.ΤΗΛΕΦΩΝΑ = data.ΤΗΛΕΦΩΝΑ;
            entity.FAX = data.FAX;
            entity.EMAIL = data.EMAIL;

            entities.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ = entity.ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ;
        }

        public void Update(SysPeriferiakiViewModel data)
        {
            ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ entity = entities.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ.Find(data.ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ);

            entity.ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ = data.ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ;
            entity.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ = data.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ;
            entity.ΤΗΛΕΦΩΝΑ = data.ΤΗΛΕΦΩΝΑ;
            entity.FAX = data.FAX;
            entity.EMAIL = data.EMAIL;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SysPeriferiakiViewModel data)
        {
            ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ entity = entities.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ.Find(data.ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SysPeriferiakiViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΠΕΡΙΦΕΡΕΙΑΚΕΣ.Select(d => new SysPeriferiakiViewModel
            {
                ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ = d.ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ,
                ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ = d.ΕΠΩΝΥΜΙΑ_ΠΕΡΙΦΕΡΕΙΑ,
                ΤΑΧ_ΔΙΕΥΘΥΝΣΗ = d.ΤΑΧ_ΔΙΕΥΘΥΝΣΗ,
                ΤΗΛΕΦΩΝΑ = d.ΤΗΛΕΦΩΝΑ,
                FAX = d.FAX,
                EMAIL = d.EMAIL
            }).Where(d => d.ΚΩΔΙΚΟΣ_ΠΕΡΙΦΕΡΕΙΑ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}