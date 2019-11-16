using System;
using System.Collections.Generic;
using Notes.DBModels;

namespace Notes.DBProviders
{
    public interface IDBProvider : IDisposable
    {
        IEnumerable<TObject> SelectAll<TObject>(Func<TObject, bool> selectFunc = null) where TObject : class, IDBModel;
        TObject Select<TObject>(Func<TObject, bool> selectFunc) where TObject : class, IDBModel;
        void Update<TObject>(IEnumerable<TObject> objects) where TObject : class, IDBModel;
        void Update<TObject>(TObject obj) where TObject : class, IDBModel;
        void Delete<TObject>(IEnumerable<TObject> obj) where TObject : class, IDBModel;
        void Delete<TObject>(TObject obj) where TObject : class, IDBModel;
        void Add<TObject>(IEnumerable<TObject> obj) where TObject : class, IDBModel;
        void Add<TObject>(TObject obj) where TObject : class, IDBModel;
    }
}
