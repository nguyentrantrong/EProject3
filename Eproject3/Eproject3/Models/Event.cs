﻿using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eproject3.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int LabsId { get; set; }

        //Relational data
        public virtual Lab Labs { get; set; }

        public Event(IFormCollection form, Lab lab)
        {
            Name = form["Event.Name"].ToString();
            Description = form["Event.Description"].ToString();
            StartTime = DateTime.Parse(form["Event.StartTime"].ToString());
            EndTime = DateTime.Parse(form["Event.EndTime"].ToString());
            Labs = lab;
        }
        public void UpdateEvent(IFormCollection form, Lab lab)
        {
            Name = form["Event.Name"].ToString();
            Description = form["Event.Description"].ToString();
            StartTime = DateTime.Parse(form["Event.StartTime"].ToString());
            EndTime = DateTime.Parse(form["Event.EndTime"].ToString());
            Labs = lab;
        }
        public Event()
        {

        }
    }
}
        
        
