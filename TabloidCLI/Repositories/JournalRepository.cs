using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString) { }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Journal Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Journal> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Journal entry)
        {
            throw new NotImplementedException();
        }

        public void Update(Journal entry)
        {
            throw new NotImplementedException();
        }
    }
}
