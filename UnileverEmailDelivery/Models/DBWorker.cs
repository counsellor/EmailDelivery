using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnileverEmailDelivery.Models
{
    public class DBWorker
    {
        private User _user;

        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        private CRUD _crud;

        public CRUD Crud
        {
            get { return _crud; }
            set { _crud = value; }
        }

        private Log _log;

        public Log Log
        {
            get { return _log; }
            set { _log = value; }
        }

        public DBWorker() { }

        public DBWorker(string login)
        {
            User = new User(login);
            Crud = new CRUD();
            Log = new Log();
        }

        internal void Create(Delivery delivery)
        {
            Crud.Create(delivery);
            Log.Insert("create", User.Login, delivery);
        }

        internal List<Delivery> Read()
        {
            return Crud.ReadAll();
        }

        internal void Update(Delivery delivery)
        {
            Crud.Update(delivery);
            Log.Insert("update", User.Login, delivery.Id);
        }

        internal void Delete(int id)
        {
            Log.Insert("delete", User.Login, id);
            Crud.Delete(id);
        }
    }
}