using ToDo.App.Domain.Exceptions;
using ToDo.App.Shared.Domain;

namespace ToDo.App.Domain.ValueObjects
{
    public class PriorityLevel : BaseValueObject
    {
        public byte Value { get; private set; }

        public PriorityLevel(byte value)
        {
            if (value < 1)
                throw new ValueIsOutOfRange(value);

            Value = value;
        }
    }
}
