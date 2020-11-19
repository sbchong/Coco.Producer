﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coco.Producer
{
  public static  class ServiceExtension
    {
        public static IServiceCollection AddCocoProducer(this IServiceCollection service,string host)
        {
            service.AddScoped<IProducer,Producer>(options=>new Producer(host));
            return service;
        }
    }
}
