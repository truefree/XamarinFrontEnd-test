using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XamarinFrontEnd
{
    public class UserModel
    {
        public Guid internalID { get; set; }

        /// <summary>
        /// Email!
        /// </summary>
        public string loginID { get; set; }
        public string profileID { get; set; }
        public bool IsEnrolled { get; set; }
        public bool IsEnrollCompleted { get; set; }
    }
}
