﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SummerCamp.Web.Models
{
    public class Announcements
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public string Title { get; set; }

        public bool Closed { get; set; }

    }
}