using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookings.Domain.BookingContext.Commands
{
    public class Command : ICommand
    {
        public Guid Id { get; private set; }

        public Command(Guid id)
        {
            this.Id = id;
        }
    }
}
