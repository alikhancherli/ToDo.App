using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.App.Shared.Domain;

namespace ToDo.App.Domain.Events;

public record TodoHasCreatedEvent : IDomainEventFlag;