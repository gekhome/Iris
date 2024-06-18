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
    public class AdminAccountService : IAdminAccountService, IDisposable
    {
        private readonly IrisDBEntities entities;

        public AdminAccountService(IrisDBEntities entities)
        {
            this.entities = entities;
        }

        public IEnumerable<UserAdminViewModel> Read()
        {
            var data = (from d in entities.USER_ADMINS
                        select new UserAdminViewModel
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

        public void Create(UserAdminViewModel data)
        {
            USER_ADMINS entity = new USER_ADMINS();

            entity.USERNAME = data.USERNAME;
            entity.PASSWORD = data.PASSWORD;
            entity.FULLNAME = data.FULLNAME;
            entity.ADMIN_LEVEL = data.ADMIN_LEVEL;
            entity.ISACTIVE = data.ISACTIVE;
            entity.CREATEDATE = data.CREATEDATE;

            entities.USER_ADMINS.Add(entity);
            entities.SaveChanges();
        }

        public void Update(UserAdminViewModel data)
        {
            USER_ADMINS entity = entities.USER_ADMINS.Find(data.USER_ID);

            entity.USERNAME = data.USERNAME;
            entity.PASSWORD = data.PASSWORD;
            entity.FULLNAME = data.FULLNAME;
            entity.ADMIN_LEVEL = data.ADMIN_LEVEL;
            entity.ISACTIVE = data.ISACTIVE;
            entity.CREATEDATE = data.CREATEDATE;

            entities.Entry(entity).State = EntityState.Modified;
            entities.SaveChanges();
        }

        public void Destroy(UserAdminViewModel data)
        {
            USER_ADMINS entity = entities.USER_ADMINS.Find(data.USER_ID);

            if (entity != null)
            {
                entities.Entry(entity).State = EntityState.Deleted;
                entities.USER_ADMINS.Remove(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }
    }
}