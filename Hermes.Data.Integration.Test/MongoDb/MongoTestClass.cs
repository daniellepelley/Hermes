using System;

namespace Hermes.Data.Integration.Test.MongoDb
{
    public class MongoTestClass
    {
        public string _id { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }

        public MongoTestClass()
        {
            _id = Guid.NewGuid().ToString();
        }

        public override bool Equals(object obj)
        {
            var other = (MongoTestClass)obj;

            return other.Number == Number &&
                   other.Title == Title;
        }
    }
}
