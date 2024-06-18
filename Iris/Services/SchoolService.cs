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
    public class SchoolService : ISchoolService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public SchoolService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<SchoolsGridViewModel> Read(string dir)
        {
            List<SchoolsGridViewModel> data = new List<SchoolsGridViewModel>();

            if (dir == "D1")
            {
                data = (from d in entities.ΣΥΣ_ΣΧΟΛΕΣ
                        where d.ΔΟΜΗ == 1 || d.ΔΟΜΗ == 4
                        orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ
                        select new SchoolsGridViewModel
                        {
                            ΣΧΟΛΗ_ΚΩΔ = d.ΣΧΟΛΗ_ΚΩΔ,
                            ΕΠΩΝΥΜΙΑ = d.ΕΠΩΝΥΜΙΑ,
                            ΔΟΜΗ = d.ΔΟΜΗ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ
                        }).ToList();
            }
            else if (dir == "D3")
            {
                data = (from d in entities.ΣΥΣ_ΣΧΟΛΕΣ
                        where d.ΔΟΜΗ != 1 && d.ΔΟΜΗ != 4
                        orderby d.ΔΟΜΗ, d.ΕΠΩΝΥΜΙΑ
                        select new SchoolsGridViewModel
                        {
                            ΣΧΟΛΗ_ΚΩΔ = d.ΣΧΟΛΗ_ΚΩΔ,
                            ΕΠΩΝΥΜΙΑ = d.ΕΠΩΝΥΜΙΑ,
                            ΔΟΜΗ = d.ΔΟΜΗ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ
                        }).ToList();
            }
            return data;
        }

        public void Create(SchoolsGridViewModel data)
        {
            ΣΥΣ_ΣΧΟΛΕΣ entity = new ΣΥΣ_ΣΧΟΛΕΣ();

            entity.ΣΧΟΛΗ_ΚΩΔ = data.ΣΧΟΛΗ_ΚΩΔ;
            entity.ΕΠΩΝΥΜΙΑ = data.ΕΠΩΝΥΜΙΑ;
            entity.ΔΟΜΗ = data.ΔΟΜΗ;
            entity.ΠΕΡΙΦΕΡΕΙΑΚΗ = data.ΠΕΡΙΦΕΡΕΙΑΚΗ;

            entities.ΣΥΣ_ΣΧΟΛΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΣΧΟΛΗ_ΚΩΔ = entity.ΣΧΟΛΗ_ΚΩΔ;
        }

        public void Update(SchoolsGridViewModel data)
        {
            ΣΥΣ_ΣΧΟΛΕΣ entity = entities.ΣΥΣ_ΣΧΟΛΕΣ.Find(data.ΣΧΟΛΗ_ΚΩΔ);

            entity.ΣΧΟΛΗ_ΚΩΔ = data.ΣΧΟΛΗ_ΚΩΔ;
            entity.ΕΠΩΝΥΜΙΑ = data.ΕΠΩΝΥΜΙΑ;
            entity.ΔΟΜΗ = data.ΔΟΜΗ;
            entity.ΠΕΡΙΦΕΡΕΙΑΚΗ = data.ΠΕΡΙΦΕΡΕΙΑΚΗ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(SchoolsGridViewModel data)
        {
            ΣΥΣ_ΣΧΟΛΕΣ entity = entities.ΣΥΣ_ΣΧΟΛΕΣ.Find(data.ΣΧΟΛΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΣΥΣ_ΣΧΟΛΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public SchoolsGridViewModel Refresh(int entityId)
        {
            return entities.ΣΥΣ_ΣΧΟΛΕΣ.Select(d => new SchoolsGridViewModel
            {
                ΣΧΟΛΗ_ΚΩΔ = d.ΣΧΟΛΗ_ΚΩΔ,
                ΕΠΩΝΥΜΙΑ = d.ΕΠΩΝΥΜΙΑ,
                ΔΟΜΗ = d.ΔΟΜΗ,
                ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ
            }).Where(d => d.ΣΧΟΛΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}