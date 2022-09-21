# 

Projeto para envio dos cadastros determinados como carrinhos abandonados e que serão utilizados pelo time de marketing

## Fluxo do Worker

## Componentes utilizados

## Squad macnai
Estabelecimentos Credenciados

## Branch
qa












$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
# flx-api-conta project

API que expõe os dados da conta bancária para as demais APIs macnai.

## Workflow

![](fluxoConta.png)

## Como rodar esta aplicação srpingboot

1° - No terminal, rode os seguintes comandos (de preferência na mesma ordem):  
```
docker network create redelocal --driver=bridge  

docker run --name macnai-conta-postgres -e POSTGRES_PASSWORD=admin -e POSTGRES_USER=admin -e POSTGRES_DB=db_conta --network redelocal -p 5432:5432 -d postgres  
```

2° - Insira essas variáveis de ambiente na aplicação, basta copiar e colar:  
```
mac_KAFKA_CLOUD_PASSWORD=0vH8QJ/Yke/cigyW5gB97dJffYqXQHInO3F5GxtHkFNn1I0ldoC0ZVH0HGmRXVDB
mac_KAFKA_CLOUD_SCHEMA-REGISTRY_KEY=2PVMOKF4ZCTYYWJ5
mac_KAFKA_CLOUD_SCHEMA-REGISTRY_SECRET=amko+rrm3bURPEzO7/lkbCxlhOdizu5VMuqxkCaDj86zNiRNLoKO069dwO2lIFuw
mac_KAFKA_CLOUD_USERNAME=TUB3FCEORLNXMDA4

SPRING_KAFKA_BOOTSTRAP_SERVERS=http://pkc-4nym6.us-east-1.aws.confluent.cloud:9092
SPRING_KAFKA_PROPERTIES_SCHEMA_REGISTRY_URL=https://psrc-4r3xk.us-east-2.aws.confluent.cloud

LOGGING_LEVEL_ROOT=INFO
LOGGING_BR_COM_mac=INFO
SERVER_PORT=8592
SPRING_DATA_SOURCE_URL=jdbc:postgresql://localhost:5432/db_conta
SPRING_DATA_SOURCE_USERNAME=admin
SPRING_DATA_SOURCE_PASSWORD=admin
```

3° - Rodar o comando abaixo acrescentando os argumentos caso queira rodar local ou importando pela sua IDE preferida 
utilizando gradle. Lembrando que é necessário estar logado na vpn para baixar as dependências necessárias para o 
projeto rodar.

> **_NOTA:_**  Ao startar a aplicação, para acessar a api basta acessar a seguinte url a depender da porta escolhida 
> na variável SERVER_PORT. Ex: http://localhost:8592/swagger-ui.html#/

## Guia do macnai

- Solução do mac Flex ([Guia](https://projetoblue1.atlassian.net/wiki/spaces/TEC/pages/1519091713/Solu%2Bo%2Bmac%2BFlex)): Encontre a documentação sobre o negócio e os diversos fluxos da solução mac Flex.
- Kotlin ([guide](https://projetoblue1.atlassian.net/wiki/spaces/TEC/pages/1983021078/Guia%2Bde%2Bboas%2Bpr%2Bticas%2B-%2BStyleguide)): Guia para boas práticas de programação para a liguagem kotlin
- Guia de boas práticas da mac ([guide](https://projetoblue1.atlassian.net/wiki/spaces/TEC/pages/181665847/Guias+de+boas+pr+ticas)): Guia de boas práticas criado pelo time de arquitetura. \o/

## Deploy Template

> Publicar: flx-api-conta
>
> Versao: v1.0.0
>
> Status: Alterar [Alterar|Criar|Excluir]
>
> Task Relacionadas: mac-03, mac-04
>
> Para publicar deve:
>
> - [desligar] - N/A

## Componentes da mac Utilizados

- [rest-adapter](https://bitbucket.org/macvisacard/rest-adapter/src/master/)
- [mac-infrastructure](https://bitbucket.org/macvisacard/mac-infrastructure/src/master/)

## Squad
mac Flex

## Repositório

- [flx-api-conta](https://bitbucket.org/macvisacard/flx-api-conta/src/master/)

## Url's swagger

- [flx-api-conta-qa](https://conta-api-qa.itmacvisacard.com/swagger-ui.html)
- [flx-api-conta-stg](https://conta-api-stg.itmacvisacard.com/swagger-ui.html)

## Commits Guideline

Nós possuimos regras e padrões sobre como as nossas mensagens de commit devem ser formatadas. Isso nós oferece uma 
melhor experiência na hora de acompaharmos o history do projeto.

Utilizamos o padrão de [conventional commits](https://www.conventionalcommits.org/), logo todos os commits neste 
repositório deverão seguir essa convenção.

### Formato do Commit

Cada mensagem de commit pode conter um **header**, um **body** e um **footer**. O header possui um formato especial 
que pode conter um **type**, uma **task** (task ou história do jira) e um **subject**.

```
<type>(<task>): <subject>
<body>
<footer>
```

O **type** e o **subject** são obrigatórios.

Exemplo:

`docs: change README about CICD`

### Tipos de Commits

| Tipo    | Função                                                                      |
| ------- | --------------------------------------------------------------------------- |
| _feat_  | Uma nova implementação / feature                                            |
| _build_ | Modificações que afetam as ferramentas de build                             |
| _ci_    | Modificações nos arquivos e nos scripts de configuração de CI               |
| _docs_  | Modificações em documentações e afins                                       |
| _fix_   | Correção de um bug                                                          |
| _perf_  | Modificações de código para otimizar performance                            |
| _impr_  | Modificações que não necessariamente é um fix ou nova feature               |
| _style_ | Mudanças que não modifiquem lógica (white-space, formatting, prettier, etc) |
| _test_  | Testes que foram adicionados ou corrigidos                                  |
| _chore_ | Uma modificação geral que não se enquandra em nenhum outro padrão           |