using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PersonModel
    {
        /// <summary>
        /// The unique identifier for the person
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represents the first name of this tournament participant
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Represents the last name of this tournament participant
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Represents the email address of this tournament participant
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Represents the cell phone number of this tournament participant
        /// </summary>
        public string CellphoneNumber { get; set; }

        public string MemberInfo
        {
            get 
            {
                return $"{FirstName} {LastName} ({EmailAddress})";
            }
        }
        public string FirstAndLast
        {
            get 
            {
                return $"{FirstName} {LastName}";
            }
        }

        public override bool Equals(object obj)
        {
            if(obj.GetType() != typeof(PersonModel))
            {
                return false;
            }

            if(obj == null)
            {
                return false;
            }

            var item = obj as PersonModel;

            return this.Id == item.Id;
        }
    }
}
