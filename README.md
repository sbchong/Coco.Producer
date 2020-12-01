# Coco.Producer

[Coco](https://github.com/sbchong/Coco)是一个使用dotnet来实现的简单高效消息队列，本项目是Coco消息队列的生产者插件.

![GitHub Workflow Status](https://img.shields.io/github/workflow/status/sbchong/Coco.Producer/Coco.Producer)
![Nuget](https://img.shields.io/nuget/dt/Coco.Producer)
![GitHub](https://img.shields.io/github/license/sbchong/Coco.Producer)

## 使用方法

+ 引入nuget包

```bash
$  dotnet add package Coco.Producer
```

+ 服务注册，注意传入参数为Coco运行环境地址，缺省为本地回环地址

```C#
public void ConfigureServices(IServiceCollection services)
{
     services.AddControllers();

     services.AddCocoProducer("127.0.0.1"); 
}
```

+ 构造器注入服务，Push方法推送，传入参数推送消息路径，消息内容

```C#
[ApiController]
[Route("[controller]")]
public class ValuesController : ControllerBase
{
    private readonly ILogger<ValuesController> _logger;
    private readonly IProducer _producer;

    public ValuesController(ILogger<ValuesController> logger, IProducer producer)
    {
        _logger = logger;
        _producer = producer;
    }

    [HttpGet]
    public string Get()
    {
        var rng = DateTime.Now.ToString();
        _producer.Push(topic : "message", message : rng);

        _logger.LogInformation(rng);
        return rng;
    }
}
```
