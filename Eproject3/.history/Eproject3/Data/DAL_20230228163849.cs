using Eproject3.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Eproject3.Data
{
    public interface IDAL
    {
        public List<Event> GetEvents();
        public Event GetEvent(int id);
        public void CreateEvent(IFormCollection form);
        public void UpdateEvent(IFormCollection form);
        public void DeleteEvent(int id);

        public class DAL : IDAL
        {
            private eProject3Context db = new eProject3Context();

            public List<Event> GetEvents()
            {
                return db.Events.Include(c => c.Labs).ToList();
            }

            public Event GetEvent(int id)
            {
                return db.Events.Include(c => c.Labs).FirstOrDefault(x => x.Id == id);
            }

            public void CreateEvent(IFormCollection form)
            {
                var labname = form["Lab"].ToString();
                var newevent = new GEvent(form, db.Labs.FirstOrDefault(x => x.LabsName == labname));
                db.Events.Add(newevent);
                db.SaveChanges();
            }

            public void UpdateEvent(IFormCollection form)
            {
                var labname = form["Lab"].ToString();
                var eventid = int.Parse(form["Event.Id"]);
                var myevent = db.Events.FirstOrDefault(x => x.Id == eventid);
                var lab = db.Labs.FirstOrDefault(x => x.LabsName == labname);
                myevent.UpdateDate(form, lab);
                db.Entry(myevent).State = EntityState.Modified;
                db.SaveChanges();
            }

            public void DeleteEvent(int id)
            {
                var myevent = db.Events.Find(id);
                db.Events.Remove(myevent);
                db.SaveChanges();
            }
        }
    }
}