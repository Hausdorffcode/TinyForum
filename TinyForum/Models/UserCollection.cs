using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyForum.Models
{
    public class UserCollection
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
        public int State { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
