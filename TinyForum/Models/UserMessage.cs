using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TinyForum.Models
{
    public class UserMessage
    {
        public int Id { get; set; }
        public string SendUserId { get; set; }
        public User SendUser { get; set; }
        public string ReceiveUserId { get; set; }
        public User ReceiveUser { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public int State { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
