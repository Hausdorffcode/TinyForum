using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TinyForum.Helpers;
using TinyForum.Models;

namespace TinyForum.Interfaces
{
    public interface ITopicRepository: IRepository<Topic>
    {
        Page<Topic> PageList(int pagesize, int pageindex);
        Page<Topic> PageList(Expression<Func<Topic, bool>> predicate, int pagesize, int pageindex);
    }
}
