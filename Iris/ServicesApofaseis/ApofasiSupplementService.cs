using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iris.DAL;
using Iris.Models;
using Iris.BPM;
using System.Data.Entity;

namespace Iris.ServicesApofaseis
{
    public class ApofasiSupplementService : IApofasiSupplementService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public ApofasiSupplementService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ApofasiSupplementGridViewModel> Read(int schoolyearId = 0, int adminId = 0)
        {
            List<ApofasiSupplementGridViewModel> data = new List<ApofasiSupplementGridViewModel>();

            if (schoolyearId > 0 && adminId > 0)
            {
                data = (from d in entities.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ
                        where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΔΙΑΧΕΙΡΙΣΤΗΣ == adminId && (d.ΣΧΟΛΗ_ΤΥΠΟΣ == 1 || d.ΣΧΟΛΗ_ΤΥΠΟΣ == 4)
                        orderby d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ descending, d.ΣΧΟΛΗ
                        select new ApofasiSupplementGridViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ
                        }).ToList();
            }
            else
            {
                data = (from d in entities.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ
                        where d.ΣΧΟΛΗ_ΤΥΠΟΣ == 1 || d.ΣΧΟΛΗ_ΤΥΠΟΣ == 4
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ descending, d.ΣΧΟΛΗ
                        select new ApofasiSupplementGridViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ
                        }).ToList();
            }
            return (data);
        }

        public IEnumerable<ApofasiSupplementGridViewModel> Read2(int schoolyearId = 0, int adminId = 0)
        {
            List<ApofasiSupplementGridViewModel> data = new List<ApofasiSupplementGridViewModel>();

            if (schoolyearId > 0 && adminId > 0)
            {
                data = (from d in entities.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ
                        where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΔΙΑΧΕΙΡΙΣΤΗΣ == adminId && (d.ΣΧΟΛΗ_ΤΥΠΟΣ != 1 && d.ΣΧΟΛΗ_ΤΥΠΟΣ != 4)
                        orderby d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ descending, d.ΣΧΟΛΗ
                        select new ApofasiSupplementGridViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ
                        }).ToList();
            }
            else
            {
                data = (from d in entities.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ
                        where d.ΣΧΟΛΗ_ΤΥΠΟΣ != 1 && d.ΣΧΟΛΗ_ΤΥΠΟΣ != 4
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ descending, d.ΣΧΟΛΗ
                        select new ApofasiSupplementGridViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ
                        }).ToList();
            }
            return (data);
        }

        public void Create(ApofasiSupplementGridViewModel data)
        {
            ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ entity = new ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ();

            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ";
            entity.ΔΙΑΧΕΙΡΙΣΤΗΣ = data.ΔΙΑΧΕΙΡΙΣΤΗΣ;
            entity.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = data.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;
            entity.ΣΧΟΛΗ = data.ΣΧΟΛΗ;
            entity.ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType((int)data.ΣΧΟΛΗ);
            entity.ΠΕΡΙΦΕΡΕΙΑΚΗ = Common.GetSchoolPeriferiaki((int)data.ΣΧΟΛΗ);
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = data.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ;

            entities.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Add(entity);
            entities.SaveChanges();

            data.ΑΠΟΦΑΣΗ_ΚΩΔ = entity.ΑΠΟΦΑΣΗ_ΚΩΔ;
        }

        public void Update(ApofasiSupplementGridViewModel data)
        {
            ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ entity = entities.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Find(data.ΑΠΟΦΑΣΗ_ΚΩΔ);

            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ";
            entity.ΔΙΑΧΕΙΡΙΣΤΗΣ = data.ΔΙΑΧΕΙΡΙΣΤΗΣ;
            entity.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = data.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;
            entity.ΣΧΟΛΗ = data.ΣΧΟΛΗ;
            entity.ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType((int)data.ΣΧΟΛΗ);
            entity.ΠΕΡΙΦΕΡΕΙΑΚΗ = Common.GetSchoolPeriferiaki((int)data.ΣΧΟΛΗ);
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = data.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(ApofasiSupplementGridViewModel data)
        {
            ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ entity = entities.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Find(data.ΑΠΟΦΑΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public ApofasiSupplementGridViewModel Refresh(int entityId)
        {
            return entities.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Select(d => new ApofasiSupplementGridViewModel
            {
                ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ,
                ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ
            }).Where(d => d.ΑΠΟΦΑΣΗ_ΚΩΔ == entityId).FirstOrDefault();
        }

        public ApofasiSupplementViewModel GetRecord(int apofasiId)
        {
            var data = (from d in entities.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiSupplementViewModel
                        {
                            ΑΠΟΦΑΣΗ_ΚΩΔ = d.ΑΠΟΦΑΣΗ_ΚΩΔ,
                            ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = d.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ,
                            ΑΔΑ = d.ΑΔΑ,
                            ΑΝΤΙΠΡΟΕΔΡΟΣ = d.ΑΝΤΙΠΡΟΕΔΡΟΣ,
                            ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = d.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ,
                            ΓΕΝΙΚΟΣ = d.ΓΕΝΙΚΟΣ,
                            ΔΙΑΧΕΙΡΙΣΤΗΣ = d.ΔΙΑΧΕΙΡΙΣΤΗΣ,
                            ΔΙΕΥΘΥΝΤΗΣ = d.ΔΙΕΥΘΥΝΤΗΣ,
                            ΔΙΟΙΚΗΤΗΣ = d.ΔΙΟΙΚΗΤΗΣ,
                            ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = d.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ,
                            ΗΜΕΡΟΜΗΝΙΑ = d.ΗΜΕΡΟΜΗΝΙΑ,
                            ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ = d.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ,
                            ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ,
                            ΠΡΟΙΣΤΑΜΕΝΟΣ = d.ΠΡΟΙΣΤΑΜΕΝΟΣ,
                            ΠΡΩΤΟΚΟΛΛΟ = d.ΠΡΩΤΟΚΟΛΛΟ,
                            ΣΤΟ_ΟΡΘΟ = d.ΣΤΟ_ΟΡΘΟ,
                            ΣΧΟΛΗ = d.ΣΧΟΛΗ,
                            ΠΕΡΙΦΕΡΕΙΑΚΗ = d.ΠΕΡΙΦΕΡΕΙΑΚΗ,
                            ΣΧΟΛΗ_ΕΓΓΡΑΦΟ = d.ΣΧΟΛΗ_ΕΓΓΡΑΦΟ,
                            ΣΧΟΛΗ_ΤΥΠΟΣ = d.ΣΧΟΛΗ_ΤΥΠΟΣ,
                            ΣΧΟΛΙΚΟ_ΕΤΟΣ = d.ΣΧΟΛΙΚΟ_ΕΤΟΣ,
                            ΥΠΟΓΡΑΦΩΝ = d.ΥΠΟΓΡΑΦΩΝ,
                            ΚΕΙΜΕΝΟ_ΥΠΟΨΗ = d.ΚΕΙΜΕΝΟ_ΥΠΟΨΗ
                        }).FirstOrDefault();
            return data;
        }

        public void UpdateRecord(ApofasiSupplementViewModel data, int apofasiId, int departement)
        {
            ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ entity = entities.ΑΠΟΦΑΣΕΙΣ_ΣΥΜΠΛΗΡΩΜΑΤΙΚΕΣ.Find(apofasiId);

            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΣΥΜΠΛΗΡΩΜΑΤΙΚΗ";
            entity.ΑΔΑ = data.ΑΔΑ;
            entity.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΔΙΑΧΕΙΡΙΣΤΗΣ = data.ΔΙΑΧΕΙΡΙΣΤΗΣ;
            entity.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = data.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ;
            entity.ΗΜΕΡΟΜΗΝΙΑ = data.ΗΜΕΡΟΜΗΝΙΑ;
            entity.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ = data.ΗΜΕΡΟΜΗΝΙΑ_ΟΡΘΟ;
            entity.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = data.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;
            entity.ΠΡΩΤΟΚΟΛΛΟ = data.ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΣΤΟ_ΟΡΘΟ = data.ΣΤΟ_ΟΡΘΟ;
            entity.ΣΧΟΛΗ = data.ΣΧΟΛΗ;
            entity.ΣΧΟΛΗ_ΕΓΓΡΑΦΟ = data.ΣΧΟΛΗ_ΕΓΓΡΑΦΟ;
            entity.ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType((int)data.ΣΧΟΛΗ);
            entity.ΠΕΡΙΦΕΡΕΙΑΚΗ = Common.GetSchoolPeriferiaki((int)data.ΣΧΟΛΗ);
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΥΠΟΓΡΑΦΩΝ = data.ΥΠΟΓΡΑΦΩΝ;
            entity.ΚΕΙΜΕΝΟ_ΥΠΟΨΗ = data.ΚΕΙΜΕΝΟ_ΥΠΟΨΗ;
            entity.ΓΕΝΙΚΟΣ = data.ΓΕΝΙΚΟΣ;
            entity.ΔΙΕΥΘΥΝΤΗΣ = data.ΔΙΕΥΘΥΝΤΗΣ;
            entity.ΠΡΟΙΣΤΑΜΕΝΟΣ = data.ΠΡΟΙΣΤΑΜΕΝΟΣ;
            entity.ΔΙΟΙΚΗΤΗΣ = data.ΔΙΟΙΚΗΤΗΣ;
            entity.ΑΝΤΙΠΡΟΕΔΡΟΣ = data.ΑΝΤΙΠΡΟΕΔΡΟΣ;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}