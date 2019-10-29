using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TeacherModel
    {
        public int TeacherID { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public string TeacherPassword { get; set; }

        public TeacherModel()
        { }

        public TeacherModel(int tID, string gName, string sName, string pass)
        {
            TeacherID = tID;
            GivenName = gName;
            Surname = sName;
            TeacherPassword = pass;
        }

    }
}