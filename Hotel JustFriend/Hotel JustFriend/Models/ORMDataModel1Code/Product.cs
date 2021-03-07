using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace Hotel_JustFriend.Models.Hotel_JustFriend
{

    public partial class Product
    {
        public Product(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
