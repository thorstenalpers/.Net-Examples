# Tutorial - RabbitMQ with .Net 5

RabbitMQ is a great messagbroker. To use it directly is hard, especially in cases of error handling. So there are some popular libraries which I will quick try out.
There is one solution which includes all examples under a separate folder. The script folder contains some scripts to deploy RabbitMQ and executes the port forwarding.

## Requirements
* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [.Net 5](https://dotnet.microsoft.com/download)
* [Docker](https://www.docker.com/products/docker-desktop)
* [Windows Terminal to script port forwarding](https://github.com/microsoft/terminal)
* [Docker fuer Windows](https://docs.docker.com/docker-for-windows/install/)
* [Helm](https://helm.sh/docs/intro/install/)


## Install and Start RabbitMQ
 
 Execute the following batch script file to download and install RabbitMQ
 ```console
 Tutorials\RabbitMQ\Scripts\1-install-rabbit-mq.bat
 
```

* Access the RabbitMQ UI in the browser with the url localhost:15672

## Masstransit

1) Start Docker
2) Make Kubernetes ports accessible from local host, Tutorials\RabbitMQ\Scripts\2-automate-port-forwarding.cmd
3) Add a breakpoint at the EventController where an event gets published
4) Add a breakpoint to the SomeEventReceivedHandler
5) Start the Masstransit Producer and Consumer with a Debug configuration
6) Also open a new browser to see the RabbitMQ queues and exchanges
7) Post an Event over the SwaggerUI


### Run multiple projects with VisualStudio

* Right click on the solution and open the properties
* Select multiple startup projects
* Select Masstransit Producer and Consumer
* Press Ok, go back and run them together

![Start multiple projects](..\Docs\assets\RabbitMQ\Masstransit_Multiple_StartUps.png)


### Live Demo


![Live Demo](..\Docs\assets\RabbitMQ\RabbitMQ_Masstransit_LiveDemo.gif)


### Links

* https://github.com/MassTransit/MassTransit
* https://masstransit-project.com/


## Rebus

Coming Soon

https://github.com/rebus-org/Rebus

