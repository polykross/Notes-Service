using System;

namespace NotesService.DBModels
{
    interface IDBModel
    {
        Guid Guid { get; }
    }
}
