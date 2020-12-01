using Microsoft.Extensions.DependencyInjection;
using System;

namespace Coco.Producer
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddCocoProducer(this IServiceCollection service, Func<IProducer, Producer> action)
        {
            service.AddScoped<IProducer, Producer>(options => action(new Producer()));
            return service;
        }

        public static IServiceCollection AddCocoProducer(this IServiceCollection service)
        {
            service.AddScoped<IProducer, Producer>();
            return service;
        }
    }
}
