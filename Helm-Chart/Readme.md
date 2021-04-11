
# Create and deploy a helm chart for a .Net5 WebApi project

This is a step by step tutorial to create a .Net5 WebApi project and at the end deploy it to local kubernetes cluster and debug with VisualStudio.

A much simpler but smaller example can be found [here](A simple helm chart for an asp application 
https://github.com/Crokus/aspnet-core-helm-sample/tree/master/chart).

## Create a .Net5 project

1. Create a project and solution

```powershell
dotnet new webapi -n Examples.HelmChart -o .\src
dotnet new sln -n Examples.HelmChart -o .\
dotnet sln Examples.HelmChart.sln add .\src
```

2. Remove Https redirection in startup.cs

```csharp
// app.UseHttpsRedirection();
```

3. (Optional) Add HealthChecks, see [Microsoft Docs](https://docs.microsoft.com/de-de/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-5.0)

4. Run project locally with VS2019 on IIS and make sure application is running


## Pack application into a docker image

1. Add a Dockerfile

```
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Examples.HelmChart.csproj", "./"]

RUN dotnet restore "./Examples.HelmChart.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Examples.HelmChart.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Examples.HelmChart.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Examples.HelmChart.dll"]
```

2. Add docker configuration to launchSettings.json

```json
{
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "profiles": {
    "Local": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "swagger",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "dotnetRunMessages": "true",
      "applicationUrl": "http://localhost:5000"
    },
    "Docker": {
      "commandName": "Docker",
      "launchBrowser": true,
      "launchUrl": "{Scheme}://{ServiceHost}:{ServicePort}/swagger",
      "publishAllPorts": true
    },
    "Bridge to Kubernetes": {
      "commandName": "AzureDevSpacesLocal",
      "launchBrowser": true
    }
  }
}
```

3. Run project locally with VS2019 on Docker and make sure application is running

4. Build docker file

```
docker build -t my-example:latest .\
```

5. Test deploymnet of container

```powershell
docker run -it --rm -e "ASPNETCORE_ENVIRONMENT=Development" --name my-example -p 8080:80 my-example:latest 
```

and open the browser with url http://localhost:8080/swagger


### Additional Links

Reference of docker commands

* https://docs.docker.com/engine/reference/run/

## Pack application into a helm chart

1. Create a helm chart

```
mkdir charts
cd charts
helm create my-example  
```

2. Change the docker repository and tag in values.yaml

```
image:
  repository: my-project
  pullPolicy: IfNotPresent
  # Overrides the image tag whose default is the chart appVersion.
  tag: "latest"
```


3. Set the correct health check pathes in deployment.yaml or remove them

```
          livenessProbe:
            httpGet:
              path: /health/live
              port: http
          readinessProbe:
            httpGet:
              path: /health/ready
              port: http
```

4. Install the chart

```
helm install my-example ./src/charts/my-example
```


5. Create a nodeport service to expose the service port permanently

5.1. Create a new file "service-nodeport.yaml" containing a configuration of a nodeport service 
```
apiVersion: v1
kind: Service
metadata:
  name: my-example-nodeport
spec:
  type: NodePort
  selector:
    app.kubernetes.io/instance: my-example # label of the pod
  ports:
    - name: http
      protocol: TCP
      port: 80
      targetPort: 80
      nodePort: 30031
```

5.2. Publish nodeport service into local kubernetes cluster

```
kubectl apply -f ./service-nodeport.yaml
```

6. Test the application. Open the browser http://localhost:30031/swagger

### Additional Links

Reference of helm commands
* https://helm.sh/docs/helm/helm/

Detailled Explanation of all files and fields of a helm chart
* https://helm.sh/docs/topics/charts/

Naming Convention for Helm Charts
https://helm.sh/docs/chart_best_practices/conventions/

## Debug application with Bridge to Kubernetes


1. Add a new file named azds.yaml

```
kind: helm-release
apiVersion: 1.1
build:
  context: .
  dockerfile: Dockerfile
install:
  chart: charts/my-example
  values:
  - values.dev.yaml?
  - secrets.dev.yaml?
  set:
    # Optionally, specify an array of imagePullSecrets. These secrets must be manually created in the namespace.
    # This will override the imagePullSecrets array in values.yaml file.
    # If the dockerfile specifies any private registry, the imagePullSecret for that registry must be added here.
    # ref: https://kubernetes.io/docs/concepts/containers/images/#specifying-imagepullsecrets-on-a-pod
    #
    # For example, the following uses credentials from secret "myRegistryKeySecretName".
    #
    # imagePullSecrets:
    #   - name: myRegistryKeySecretName
    replicaCount: 1
    image:
      repository: my-example
      tag: $(tag)
      pullPolicy: Never
    ingress:
      annotations:
        kubernetes.io/ingress.class: traefik-azds
      hosts:
      # This expands to form the service's public URL: [space.s.][rootSpace.]my-example.<random suffix>.<region>.azds.io
      # Customize the public URL by changing the 'my-example' text between the $(rootSpacePrefix) and $(hostSuffix) tokens
      # For more information see https://aka.ms/devspaces/routing
      - $(spacePrefix)$(rootSpacePrefix)my-example$(hostSuffix)
configurations:
  develop:
    build:
      dockerfile: Dockerfile.develop
      useGitIgnore: true
      args:
        BUILD_CONFIGURATION: ${BUILD_CONFIGURATION:-Debug}
    container:
      sync:
      - "**/Pages/**"
      - "**/Views/**"
      - "**/wwwroot/**"
      - "!**/*.{sln,csproj}"
      command: [dotnet, run, --no-restore, --no-build, --no-launch-profile, -c, "${BUILD_CONFIGURATION:-Debug}"]
      iterate:
        processesToKill: [dotnet, vsdbg, my-example]
        buildCommands:
        - [dotnet, build, --no-restore, -c, "${BUILD_CONFIGURATION:-Debug}"]

```

2. Add some breakpoints in the controller actions

3. Run Bridge to Kubernetes and enter as apllication url http://localhost:30031/swagger

4. Execute some API actions from within SwaggerUI

### Additional Links

Bridge to Kubernetes
* https://docs.microsoft.com/de-de/visualstudio/containers/overview-bridge-to-kubernetes?view=vs-2019

Configure Bridge to Kubernetes
https://docs.microsoft.com/de-de/azure/dev-spaces/how-dev-spaces-works-up

