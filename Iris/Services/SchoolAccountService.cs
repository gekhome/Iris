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
    public class SchoolAccountService : ISchoolAccountService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public SchoolAccountService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<UserSchoolViewModel> Read()
        {
            var data = (from a in entities.USER_SCHOOLS
                        orderby a.USERNAME
                        select new UserSchoolViewModel
                        {
                            USER_ID = a.USER_ID,
                            USERNAME = a.USERNAME,
                            PASSWORD = a.PASSWORD,
                            USER_SCHOOLID = a.USER_SCHOOLID ?? 0,
                            ISACTIVE = a.ISACTIVE ?? false
                        }).ToList();
            return data;
        }

        public void Create(UserSchoolViewModel data)
        {
            USER_SCHOOLS entity = new USER_SCHOOLS();

            entity.USERNAME = data.USERNAME;
            entity.PASSWORD = data.PASSWORD;
            entity.USER_SCHOOLID = data.USER_SCHOOLID;
            entity.ISACTIVE = data.ISACTIVE;

            entities.USER_SCHOOLS.Add(entity);
            entities.SaveChanges();

            data.USER_ID = entity.USER_ID;
        }

        public void Update(UserSchoolViewModel data)
        {
            USER_SCHOOLS entity = entities.USER_SCHOOLS.Find(data.USER_ID);

            entity.USER_ID = data.USER_ID;
            entity.USERNAME = data.USERNAME;
            entity.PASSWORD = data.PASSWORD;
            entity.USER_SCHOOLID = data.USER_SCHOOLID;
            entity.ISACTIVE = data.ISACTIVE;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(UserSchoolViewModel data)
        {
            USER_SCHOOLS entity = entities.USER_SCHOOLS.Find(data.USER_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.USER_SCHOOLS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public UserSchoolViewModel Refresh(int entityId)
        {
            return entities.USER_SCHOOLS.Select(d => new UserSchoolViewModel
            {
                USER_ID = d.USER_ID,
                USERNAME = d.USERNAME,
                PASSWORD = d.PASSWORD,
                ISACTIVE = d.ISACTIVE ?? false,
                USER_SCHOOLID = d.USER_SCHOOLID
            }).Where(d => d.USER_ID == entityId).FirstOrDefault();
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}