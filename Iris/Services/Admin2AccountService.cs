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
    public class Admin2AccountService : IAdmin2AccountService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public Admin2AccountService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<UserAdmin2ViewModel> Read()
        {
            var data = (from d in entities.USER_ARIADNE
                        select new UserAdmin2ViewModel
                        {
                            USER_ID = d.USER_ID,
                            USERNAME = d.USERNAME,
                            PASSWORD = d.PASSWORD,
                            FULLNAME = d.FULLNAME,
                            ADMIN_LEVEL = d.ADMIN_LEVEL ?? 0,
                            ISACTIVE = d.ISACTIVE ?? false,
                            CREATEDATE = (DateTime)d.CREATEDATE
                        }).ToList();

            return data;
        }

        public void Create(UserAdmin2ViewModel data)
        {
            USER_ARIADNE entity = new USER_ARIADNE();

            entity.USERNAME = data.USERNAME;
            entity.PASSWORD = data.PASSWORD;
            entity.FULLNAME = data.FULLNAME;
            entity.ADMIN_LEVEL = data.ADMIN_LEVEL;
            entity.ISACTIVE = data.ISACTIVE;
            entity.CREATEDATE = data.CREATEDATE;

            entities.USER_ARIADNE.Add(entity);
            entities.SaveChanges();
        }

        public void Update(UserAdmin2ViewModel data)
        {
            USER_ARIADNE entity = entities.USER_ARIADNE.Find(data.USER_ID);

            entity.USERNAME = data.USERNAME;
            entity.PASSWORD = data.PASSWORD;
            entity.FULLNAME = data.FULLNAME;
            entity.ADMIN_LEVEL = data.ADMIN_LEVEL;
            entity.ISACTIVE = data.ISACTIVE;
            entity.CREATEDATE = data.CREATEDATE;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(UserAdmin2ViewModel data)
        {
            USER_ARIADNE entity = entities.USER_ARIADNE.Find(data.USER_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.USER_ARIADNE.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}