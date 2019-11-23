using Notes.DBModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Notes.EntityFrameworkDBProvider
{
    public class DBProvider : Notes.DBProviders.IDBProvider
    {
        private readonly NotesDBContext _context;

        public DBProvider()
        {
            _context = new NotesDBContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IEnumerable<TObject> SelectAll<TObject>(Func<TObject, bool> selectFunc = null) where TObject : class, IDBModel
        {
            var result = _context.Set<TObject>();
            return selectFunc == null ? result : result.Where(selectFunc);
        }

        public TObject Select<TObject>(Func<TObject, bool> selectFunc) where TObject : class, IDBModel
        {
            return SelectAll(selectFunc).FirstOrDefault();
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
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
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
            _context.Entry(obj).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public void Add<TObject>(IEnumerable<TObject> obj) where TObject : class, IDBModel
        {
            foreach (var dbModel in obj)
            {
                Add(dbModel);
            }
        }

        public void Add<TObject>(TObject obj) where TObject : class, IDBModel
        {
            _context.Entry(obj).State = EntityState.Added;
            _context.SaveChanges();
        }
    }
}
