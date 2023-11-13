using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingStarted
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        // public State Initial { get; private set; } 
        public State Submitted { get; private set; } = null!;
        public State Accepted { get; private set; } = null!;
        // public State Final { get; private set; } 

        public OrderStateMachine()
        {
            //InstanceState(x => x.CurrentState);
            InstanceState(x => x.CurrentState, Submitted, Accepted);
            Initially(
                // Behavior Starts
                When(SubmitOrder)
                // Activities
                .TransitionTo(Accepted) // !! NOT SAVED
                // Activities
                .TransitionTo(Submitted)
            // Behavior completes and state persisted
            );

            Event(() => OrderAccepted, x => x.CorrelateById(context => context.Message.OrderId));

            During(Submitted, When(OrderAccepted).TransitionTo(Accepted));

            During(Accepted, Ignore(SubmitOrder));

            During(Accepted, When(SubmitOrder).Then(x => x.Saga.OrderDate = x.Message.OrderDate));

        }
        public Event<SubmitOrder> SubmitOrder { get; private set; }
        public Event<OrderAccepted> OrderAccepted { get; private set; }
    }
}
