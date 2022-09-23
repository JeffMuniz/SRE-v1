## Exposição via service:

- Dev
Service Ip - 10.186.28.101
Port range - 9***
availabilityapi - 9000
productenrichmentmacnaimaapi - 9001
dashboard - 9002
appX - 9003
appY - 9004

---
QA
Service Ip - 10.186.28.102
Port range - 8***
availabilityapi - 8000
productenrichmentmacnaimaapi - 8001
dashboard - 8002
appX - 8003
appY - 8004

## Subindo a aplicação

Criando NS

```kubectl create ns catalog-integration-dev```


Subindo a aplicação

```bash
kubectl apply $(ls */*-configmap.yaml | awk ' { print " -f " $1 } ') -n catalog-integration-dev
kubectl apply $(ls */*-deployment.yaml | awk ' { print " -f " $1 } ') -n catalog-integration-dev
kubectl apply $(ls */*-service.yaml | awk ' { print " -f " $1 } ') -n catalog-integration-dev
```


# Sobre a Solução
Tem a finalidade de importar os produtos dos diversos fornecedores de maneira que o produto seja identificado, 
independente do fornecedor, normalizado e enriquecido através de serviço disponibilizado pela Macnaima.
Utiliza-se dos conceitos de clean arquitecture, micro-services e containers.

## Onboarding Para Desenvolvedores
Abaixo estarão incluídos os passo a passos para possuir o necessário para trabalhar na solução

### Sugestões de Ferramentas
- **Gestão dos containers** - [Portainer](https://www.portainer.io/) é uma imagem que auxilia na gestão dos containers;
- **Mongo DB** - [NoSQL Booster](https://nosqlbooster.com/) auxilia à gerenciar as informações armazenadas no Mongo DB;
- **Azure Service Bus** - Service Bus Explorer ([download](https://aka.ms/servicebusexplorer)) facilita na visualização e gestão das mensagens no **Azure Service Bus**, é possível acessar [versão web do explorer](https://azure.microsoft.com/pt-br/updates/sesrvice-bus-explorer/) no Portal Azure;
- **Redis** - RedisInsight ([Redis Doc](https://docs.redis.com/latest/ri/installing/install-redis-desktop/));

### Utilizando o docker
#### Pré-requisitos
  - WSL2 Instalado no Windows, artigos úteis [Instalar o WSL](https://docs.microsoft.com/pt-br/windows/wsl/install) e [Etapas de instalação manual para versões mais antigas do WSL](https://docs.microsoft.com/pt-br/windows/wsl/install-manual);
  - [Docker Desktop Instalado](https://docs.docker.com/desktop/windows/install/);

Após as instalações acima, tenha certeza de que o docker esteja em execução e que seja utilizado o WSL 2.

![running-wsl2-docker.png](assets/running-wsl2-docker.png)

Caso não tenha visto a notificação do Windows, é possível observar se está ativado o uso do WSL 2.

![wsl2-in-use.png](assets/wsl2-in-use.png)

Geralmente demora algum tempo para iniciar todos os recursos, ao iniciar o  **Docker Desktop** verifique se ele está em execução, o canto inferior direito estará indicando a cor verde.

![docker-desktop.png](assets/docker-desktop.png)


#### Como executar a solução utilizando docker
Na toolbar *Standard* do Visual Studio altere a opção **(1)** *Startup Projects* para **docker-compose** e certifique-se de que o **(2)** dropdown ao lado esteja selecionada a opção:

![standard-toolbar.png](assets/standard-toolbar.png)

![run-solution-with-docker.png](assets/run-solution-with-docker.png)

Opções que são apresentadas ao optar pelo docker:
- **Docker All Services** - Se deseja criar a imagem, container e executar todos os componentes.
- **Manage Docker Compose Launch Settings** - Configura a execução dos containers, semelhante à opção de rodar múltiplos projetos e utilizada em ocnjunto com *Docker Custom Services*;

![manage-docker-launch.png](assets/manage-docker-launch.png)

- **Docker Custom Services** - Se deseja rodar específicamente determinados componentes da solução;

Para visualizar a *containers window*:

![view-containers.png](assets/view-containers.png)

![containers-window.png](assets/containers-window.png)

# Tecnologias
A solução utiliza das seguintes tecnologias, frameworks e recursos:

| Tecnologia | Descrição |
|--|--|
| [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0) | Linguagem de desenvolvimento |
| [SQL Server](https://www.microsoft.com/pt-br/sql-server/) | Banco de dados relacional e principal |
| [Mongo DB](https://www.mongodb.com/) | Banco de dados não relacional |
| [Redis](https://redis.io/) | Cache de dados |
| [Solr](https://solr.apache.org/) | Banco de dados para pesquisa multifacetada |
| [Consul](https://www.consul.io/) | Serviço utilizado para armazenagem de key-value das configurações das aplicações |
| [Polly](http://www.thepollyproject.org/) | Framework de resiliência e tolerância à falha para comunicações HTTP |
| [Hangfire](https://www.hangfire.io/) | Framework que gerencia e processa os background jobs |
| [Masstransit](https://masstransit-project.com/) | Framework utilizado para o envio de mensagens para o bus |
| [Azure Service Bus](https://azure.microsoft.com/en-us/services/service-bus/) | Recurso utilizado para envio de mensagens |
| [NLog](https://nlog-project.org/) | Utilizado para registrar os logs |
| [Graylog](https://www.graylog.org/) | Recurso utilizado para armazenamento e gerenciamento dos logs |
| [Open Telemetry](https://opentelemetry.io/) | Observabilidade e integração com Datadog para monitoramento |
| [Docker](https://www.docker.com/) | Tecnologia para criação e publicação de aplicações em container |
| [Kubernetes](https://kubernetes.io/pt-br/) | Orquestrador de containers |

# Recursos da solução
Os caminhos para e ambientes dos recursos da solução.

## Azure Service Bus
- DEV
```
Endpoint=sb://   configmap.yaml: should be well protected
```
- QA
```
```
- UAT
```
```
- PP
```
```
- PROD
```
```
## SQL Server
- DEV
```
Data Source=macsqllivehml.eastus.cloudapp.azure.com,1434;Initial Catalog=PRD_DB_AZU_Live_Catalog;Persist Security Info=True;User ID=aplic.QAmrktplac;Password=HnQo5MVdekUk8k;MultipleActiveResultSets=True
```
- QA
```
```
- UAT
```
```
- PP
```
```
- PROD
```
```

## Mongo DB
- DEV
```
mongodb://Applica-que-vai-integrar:auser@fqdn:27017/CatalogIntegration?authSource=some-entropy&readPreference=primary
```
- QA
```
mongodb://aplic.mkp:  configmap.yaml: should be well protected@mongodb.qa.machinacorp.com.br:27017/CatalogIntegration?authSource=admin
```
- UAT
```
```
- PP
```
```
- PROD
```
```

## Redis
- DEV
```
t0-shard-0
MAcMkpRedisQA1.redis.cache.windows.net,password=pqlCx03yDrfMZB+2T55yWfBakaPDZP1S87J6A7EFIwM=,ssl=False,abortConnect=False

t0-shard-1
cloudloyaltyproductredisdev.redis.cache.windows.net,password=gkfP7HKDLzkIiue9ZMwPH5CQ9UxCmu9tNsrYLB4P1ZA=,ssl=False,abortConnect=False
```
- QA
```
```
- UAT
```
```
- PP
```
```
- PROD
```
```

## Solr
- DEV
```
http://solr.qa.machinacorp.com.br:8983/solr/#/
```
- QA
```
```
- UAT
```
```
- PP
```
```
- PROD
```
```

## Consul
- DEV
```
http://az-us-dev-consul-01.dev.lab.macna.com:8500
token: NoEntropy'sHere
datacenter: dc1
```
- QA
```
```
- UAT
```
```
- PP
```
```
- PROD
```
```
# Mocks
## Macnaima
```
```
## Partner Hub
```
base: https://partnerhub-saga.getsandbox.com
GET /v1/api/availability?partner_code=:partner_code&sku_id=:sku_id&contract=:contract
GET /v1/api/get-client-configuration?clientName=:clientName
```
## Availability API
```
https://availability-mac.getsandbox.com
GET /availability/partner/{supplier}/{supplierSku}/{contract}
GET /availability/{supplier}/{supplierSku}/{contract}
```
> O recurso ainda não possui publicação

# Configuração da Solution
## Configuração padrão do `.csprojects`
Em linhas gerais os projetos possuem as seguintes configurações padrão:
```
  <PropertyGroup>
    <TargetFramework>net5.0</TargetFramework>
    <LangVersion>Latest</LangVersion>
    <Configurations>Debug;DEV;QA;UAT;PP;PROD</Configurations>
    <AssemblyName>Catalog.Integration.Availability.Cache.Worker</AssemblyName>
    <RootNamespace>Availability.Cache.Worker</RootNamespace>
  </PropertyGroup>

  <PropertyGroup>
    <Optimize Condition="'$(Configuration)'=='Debug'">false</Optimize>
    <Optimize Condition="'$(Configuration)'!='Debug'">true</Optimize>
    <DefineConstants Condition="'$(Configuration)'=='DEV'">DEV</DefineConstants>
    <DefineConstants Condition="'$(Configuration)'=='QA'">QA</DefineConstants>
    <DefineConstants Condition="'$(Configuration)'=='UAT'">UAT</DefineConstants>
    <DefineConstants Condition="'$(Configuration)'=='PP'">PP</DefineConstants>
    <DefineConstants Condition="'$(Configuration)'=='PROD'">PROD</DefineConstants>
  </PropertyGroup>
```
As configurações acima definem os ambientes, comportamentos e também padroniza a aplicação. **AssemblyName** é padronizado
com o prefixo *Catalog.Integration.*, seguido do nome do projeto. **RootNamespace** se mantém no padrão que o Visual Studio 
atribue.

### Sobre as Configurations
De modo geral as configurações auxiliam os projetos à inicializarem corretamente, dentro dos `.csproj` é possível identificar `Conditions`
Por exemplo diferenciar o carregamento de dependências por *Package Reference* ou *Project Reference*, para evitar a necessidade de publicar NuGet. 

| Configuration | Environment |
|--|--|
| Debug | Desenvolvimento (DEV) |
| DEV | Desenvolvimento (DEV) |
| QA | Publicação para validação |
| UAT | User Acceptance Testing (UAT) |
| PP | Pré-produção |
| PROD | Produção (PROD) |

# Conhecimentos Sobre o Masstransit

- O Masstransit utiliza uma implementação simples de `IHostedService` para iniciar o *Bus*, portanto a Dependency Injection da 
Aplicação não é a mesma do Bus, somente os serviços registrados na *DI* do Bus do Masstransit iram ser encontradas no `IServiceCollection`;

- Mensagens podem ser enviadas (Send) ou publicadas (Publish), 
onde *Send* é considerado um #Command# - mensagem é enviada para um endpoint específico; e
o *Publish* é um #Event# - a mensagem é enviada por broadcast para todos os consumers interessados;

## Mensagens
1. Para omitir o *code smell* sobre padrão para declaração de interfaces, gerado pelo padrão utilizado e sugerido no Masstransit, os projetos de contendo mensagens devem possuir um arquivo de supressão contendo algo parecido com:
```
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Declaração padrão para mensagens utilizado pelo Masstransit", Scope = "module")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Declaração padrão para mensagens utilizado pelo Masstransit", Scope = "module")]
```

# NuGet
Necessidade de criar uma biblioteca compartilhada ou um Package NuGet para distribuição das mensagens. 
Masstransit abstrai a criação e alteração da infraestrutura no Azure e com isso utiliza a mensagem declarada no código para criar as subscrições e tópicos.
**Por exemplo:** A produção e o consumo distribuído da hipotética mensagem *BoasNovas*, obrigará a utilização da referência do assembly em que a mensagem *BoasNovas* foi declarada para produzi-la ou  consumi-la com sucesso.

Possíveis formatos válidos para utilização da biblioteca compartilhada:
- **Biblioteca Compartilhada:** *Project Reference* Todas as aplicações declaradas em mesma Solution utilizando referência via projeto;
- **NuGet Package:** *Package Reference* Publicação de NuGet nos Artifacts do Azure para sua distribuição;

# Referências
- https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html
- https://enterprisecraftsmanship.com/posts/functional-c-immutability/
- https://enterprisecraftsmanship.com/posts/functional-c-primitive-obsession/
- https://enterprisecraftsmanship.com/posts/functional-c-non-nullable-reference-types/
- https://enterprisecraftsmanship.com/posts/functional-c-handling-failures-input-errors/
- https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/net-core-microservice-domain-model
