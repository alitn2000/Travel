using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Entities.TripManagement;
using Travel.Domain.Core.Entities.UserUserManagement;

namespace Travel.Domain.Core.Entities.Userr
{
    public class Userr : BaseEntity, IAggregateRoot
    {
        private Userr()
        {
            
        }

        public Userr(string userName, int id, int userNameType)
        {
            Id = id;
            UserName = userName;
            UserNameType = userNameType;

            // Initialize Profile  
            // call domain event for trip
            
        }

        public int Id { get; private set; }
        public string UserName { get; private set; }
        public int UserNameType { get; private set; }
        public Profile Profile { get; private set; }
        public List<UserTrip> UserTrips { get; set; } = new List<UserTrip>();



        public void AddTrip(int id, Userr user, int userId, Trip trip, int tripId, bool isOwner)
        {

        }
    }

    //public class test
    //{
    //    public void TestMethod()
    //    {
    //        // This is a test method to demonstrate the structure of the code.
    //        // You can add your logic here.

    //        var user = new Userr("test", 1, 2);

    //        user.AddTrip(); // Call the method to add a trip

    //    }
    //}
}
