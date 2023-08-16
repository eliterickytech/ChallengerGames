using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLambda3.WorldCup.Application.Shared
{
    public abstract class BaseContract<Entity> : Notifiable<Notification> where Entity : class
    {
        protected abstract void Validate(Entity entity);
    }
}
