using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingStarted
{
    public interface SubmitOrder
    {
        Guid OrderId { get; }
        DateTime OrderDate { get; }
    }

    public interface OrderAccepted
    {
        Guid OrderId { get; }
    }

    public class OrderState :
        SagaStateMachineInstance,
        ISagaVersion
    {
        public Guid CorrelationId { get; set; }
        //public string CurrentState { get; set; }
        public int CurrentState { get; set; } = 0;
        public DateTime? OrderDate { get; set; }

        public int Version { get; set; }
    }
}
