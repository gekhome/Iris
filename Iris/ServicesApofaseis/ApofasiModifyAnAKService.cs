﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iris.DAL;
using Iris.Models;
using Iris.BPM;
using System.Data.Entity;

namespace Iris.ServicesApofaseis
{
    public class ApofasiModifyAnAKService : IApofasiModifyAnAKService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public ApofasiModifyAnAKService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<ApofasiModifyAnaplirotesAKGridViewModel> Read(int schoolyearId = 0, int adminId = 0)
        {
            List<ApofasiModifyAnaplirotesAKGridViewModel> data = new List<ApofasiModifyAnaplirotesAKGridViewModel>();

            if (schoolyearId > 0 && adminId > 0)
            {
                data = (from d in entities.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ
                        where d.ΣΧΟΛΙΚΟ_ΕΤΟΣ == schoolyearId && d.ΔΙΑΧΕΙΡΙΣΤΗΣ == adminId && (d.ΣΧΟΛΗ_ΤΥΠΟΣ == 1 || d.ΣΧΟΛΗ_ΤΥΠΟΣ == 4)
                        orderby d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ descending, d.ΣΧΟΛΗ
                        select new ApofasiModifyAnaplirotesAKGridViewModel
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
                data = (from d in entities.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ
                        where d.ΣΧΟΛΗ_ΤΥΠΟΣ == 1 || d.ΣΧΟΛΗ_ΤΥΠΟΣ == 4
                        orderby d.ΣΧΟΛΙΚΟ_ΕΤΟΣ descending, d.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ descending, d.ΣΧΟΛΗ
                        select new ApofasiModifyAnaplirotesAKGridViewModel
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

        public void Create(ApofasiModifyAnaplirotesAKGridViewModel data)
        {
            ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ entity = new ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ();

            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ-ΑΝ-ΑΚ";
            entity.ΔΙΑΧΕΙΡΙΣΤΗΣ = data.ΔΙΑΧΕΙΡΙΣΤΗΣ;
            entity.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ = data.ΗΜΝΙΑ_ΣΥΝΤΑΞΗ;
            entity.ΣΧΟΛΗ = data.ΣΧΟΛΗ;
            entity.ΣΧΟΛΗ_ΤΥΠΟΣ = Common.GetSchoolType((int)data.ΣΧΟΛΗ);
            entity.ΠΕΡΙΦΕΡΕΙΑΚΗ = Common.GetSchoolPeriferiaki((int)data.ΣΧΟΛΗ);
            entity.ΣΧΟΛΙΚΟ_ΕΤΟΣ = data.ΣΧΟΛΙΚΟ_ΕΤΟΣ;
            entity.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = data.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ;

            entities.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ.Add(entity);
            entities.SaveChanges();

            data.ΑΠΟΦΑΣΗ_ΚΩΔ = entity.ΑΠΟΦΑΣΗ_ΚΩΔ;
        }

        public void Update(ApofasiModifyAnaplirotesAKGridViewModel data)
        {
            ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ entity = entities.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ.Find(data.ΑΠΟΦΑΣΗ_ΚΩΔ);

            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ-ΑΝ-ΑΚ";
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

        public void Destroy(ApofasiModifyAnaplirotesAKGridViewModel data)
        {
            ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ entity = entities.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ.Find(data.ΑΠΟΦΑΣΗ_ΚΩΔ);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ.Remove(entity);
                entities.SaveChanges();
            }
        }

        public ApofasiModifyAnaplirotesAKGridViewModel Refresh(int entityId)
        {
            return entities.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ.Select(d => new ApofasiModifyAnaplirotesAKGridViewModel
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

        public ApofasiModifyAnaplirotesAKViewModel GetRecord(int apofasiId)
        {
            var data = (from d in entities.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ
                        where d.ΑΠΟΦΑΣΗ_ΚΩΔ == apofasiId
                        select new ApofasiModifyAnaplirotesAKViewModel
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
                            ΚΕΙΜΕΝΟ_ΑΚ = d.ΚΕΙΜΕΝΟ_ΑΚ,
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
                            ΚΕΙΜΕΝΟ_ΥΠΟΨΗ = d.ΚΕΙΜΕΝΟ_ΥΠΟΨΗ,
                            ΑΠΟΦΑΣΗ_ΜΕΤΑΒΟΛΗΣ = d.ΑΠΟΦΑΣΗ_ΜΕΤΑΒΟΛΗΣ,
                            ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = d.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ,
                            ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = d.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ
                        }).FirstOrDefault();
            return data;
        }

        public void UpdateRecord(ApofasiModifyAnaplirotesAKViewModel data, int apofasiId, int departement)
        {
            ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ entity = entities.ΑΠΟΦΑΣΕΙΣ_ΤΡΟΠΟΠΟΙΗΤΙΚΕΣ_ΑΝ_ΑΚ.Find(apofasiId);

            entity.ΑΠΟΦΑΣΗ_ΕΙΔΟΣ = "ΤΡΟΠΟΠΟΙΗΤΙΚΗ-ΑΝ-ΑΚ";
            entity.ΑΔΑ = data.ΑΔΑ;
            entity.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ = data.ΑΡΧΙΚΗ_ΠΡΩΤΟΚΟΛΛΟ;
            entity.ΔΙΑΧΕΙΡΙΣΤΗΣ = data.ΔΙΑΧΕΙΡΙΣΤΗΣ;
            entity.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ = data.ΕΓΓΡΑΦΟ_ΕΙΔΟΣ;
            entity.ΚΕΙΜΕΝΟ_ΑΚ = data.ΚΕΙΜΕΝΟ_ΑΚ;
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
            entity.ΑΠΟΦΑΣΗ_ΜΕΤΑΒΟΛΗΣ = data.ΑΠΟΦΑΣΗ_ΜΕΤΑΒΟΛΗΣ;
            entity.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ = data.ΑΠΟΦΑΣΗ_ΔΙΟΙΚΗΤΗΣ;
            entity.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ = data.ΑΠΟΦΑΣΗ_ΥΠΟΥΡΓΟΣ;
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