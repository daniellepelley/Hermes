//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hermes.Data.Integration.Test
{
    using System;
    using System.Collections.Generic;
    
    public partial class TestClass
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }

        public override bool Equals(object obj)
        {
            var other = (TestClass) obj;

            return other.Number == Number &&
                   other.Title == Title;
        }
    }
}
