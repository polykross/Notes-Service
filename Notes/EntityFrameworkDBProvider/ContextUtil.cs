using Notes.EntityFrameworkDBProvider;
using System;

namespace Notes.RESTService.Repository
{
    public class ContextUtil
    {
        public ContextUtil()
        {

        }

        public void DoWithContext(Action<NotesDBContext> action)
        {
            using (var context = GetContext())
            {
                action.Invoke(context);
            }
        }

        private NotesDBContext GetContext()
        {
            return new NotesDBContext();
        }
    }
}