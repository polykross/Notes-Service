using Notes.DBModels;
using Notes.RESTService.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Notes.EntityFrameworkDBProvider
{
    public class DBProviderDisconnected : Notes.DBProviders.IDBProvider
    {
        private ContextUtil _contextUtil;

        public DBProviderDisconnected()
        {
            _contextUtil = new ContextUtil();
        }

        public void Dispose()
        {
            _contextUtil = null;
        }

        public IEnumerable<TObject> SelectAll<TObject>(Func<TObject, bool> selectFunc = null) where TObject : class, IDBModel
        {
            List<TObject> result = new List<TObject>();
            _contextUtil.DoWithContext(ctx => result.AddRange(ctx.Set<TObject>()));
            return selectFunc == null ? result : result.Where(selectFunc.Invoke);
        }

        public TObject Select<TObject>(Func<TObject, bool> selectFunc) where TObject : class, IDBModel
        {
            return SelectAll(selectFunc).First();
        }

        public void Update<TObject>(IEnumerable<TObject> objects) where TObject : class, IDBModel
        {
            foreach (var dbModel in objects)
            {
                Update(dbModel);
            }
        }

        public void Update<TObject>(TObject obj) where TObject : class, IDBModel
        {
            _contextUtil.DoWithContext(ctx =>
                {
                    ctx.Entry(obj).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            );
        }

        public void Delete<TObject>(IEnumerable<TObject> obj) where TObject : class, IDBModel
        {
            foreach (var dbModel in obj)
            {
                Delete(dbModel);
            }
        }

        public void Delete<TObject>(TObject obj) where TObject : class, IDBModel
        {
            _contextUtil.DoWithContext(ctx =>
                {
                    ctx.Entry(obj).State = EntityState.Deleted;
                    ctx.SaveChanges();
                }
            );
        }

        public void Add<TObject>(IEnumerable<TObject> obj) where TObject : class, IDBModel
        {
            foreach (var dbModel in obj)
            {
                Add(dbModel);
            }
        }

        public bool Add<TObject>(TObject obj) where TObject : class, IDBModel
        {
            var result = true;
            _contextUtil.DoWithContext(ctx =>
                {
                    try
                    {
                        ctx.Set<TObject>().Add(obj);
                        ctx.SaveChanges();
                    }
                    catch
                    {
                        result = false;
                        ctx.Dispose();
                    }
                }
            );
            return result;
        }
    }
}
