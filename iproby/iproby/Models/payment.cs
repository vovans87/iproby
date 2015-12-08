using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iproby.Models
{
    public class payment
    {
        private string _mrchlogin = "iproru";
        public string mrchlogin { get { return _mrchlogin; } set { _mrchlogin = value; } }
        private float _outsum = 1;
        public float outsum { get { return _outsum; } set { _outsum = value; } }
        private static int _invid = 1;
        public int invid { get { return _invid; } set { _invid = value; } }
        string _desc = "Оплата поднятия объявления";
        public string desc { get { return _desc; } set { _desc = value; } }
        public string signaturevalue { get; set; }
        //private string _password1 = test "mESNR1J5NUSMe16W8cIg"; real wOKw20G8EmR3X6iJyDMM 12345678q
        private string _password1 = "qjw46trTDWOQZ3dkY47c";
        public string password1 { get { return _password1; } set { _password1 = value; } }
        //private string _password2 = "T8p49zrpAN8IjClQdK5Y"; PXISDNIs50NHUy0lgP08 87654321q
        private string _password2 = "INRJTSY45Eh318LGCROj";
        public string password2 { get { return _password2; } set { _password2 = value; } }
        public string status_text { get; set; }
        private int _istest = 0;
        public int istest { get{return _istest;} set{_istest=value;} }
    }
}