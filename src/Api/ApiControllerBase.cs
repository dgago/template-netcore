using Application.Commands;

using Microsoft.AspNetCore.Mvc;

namespace Api
{
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IEventPublisher EventPublisher;
        protected readonly Presenter Presenter;

        public ApiControllerBase(IEventPublisher eventPublisher, Presenter presenter)
        {
            this.EventPublisher = eventPublisher;
            this.Presenter = presenter;
        }
    }
}