
SRE

* Desenhar a arquitetura alto nível do sistema: Data Flow, Requests Flow para ajudar no estabelecimento do SLO
* Defininção dos SLI's
    * Latência de Requisição, Taxa de Erro (requests), Throughput (requests por segundo - TPS)
* Definir o Documento de SLO
    * Tipo do SLO (sli) e o Objetivo (SLO)
//     * Definir Error Budget
     
* Levantamento do TOIL
    * Consulta quantitativa e qualitativa da base de chamados mensal da SQUAD até a entrega em produção
    * Mapeamento das atividades que podem ser automatizadas e ou eliminadas
* Conduzir os Postmortens para trativa de problemas
    * Envolver a pessoas acompanhar o tratamento e a causa raiz
* Encontrar os limitantes das aplicações, com eventuais testes de carga.
// * Identificar e corrigir pontos de possíveis falhas na resiliência, inclusive projetando mudanças na arquitetura ou infra-estrutura

* Identificar pontos fracos na entrega de software ou gerenciamento de incidentes
* Acompanhar as rotinas de Operação e Desenvolvimento, criando uma base de conhecimento para direcionar pessoas/times na solução de problemas: Definição de Papéis e Responsabilidade
///
* Validar o Data Flow End to End (fluxo de dados fim à fim incluindo UX), após novas implemnetações ou se o PostMorten definir necessário.
* Validar processo de implementação e manutenção que cruzam times distintos 
* Validar integração entre ferramentas e recursos utilizados contra os disponíveis, incluuindo as  que cruzam times distintos 




Questões que podem ajudar

Qual é e como se dá a relação dessa área e dos times que a compõe com o time de desenvolvedores? 												
Quais os pricipais desafios e pontos fortes encontrados no dia-a-dia nessa interação?												
A.	Para produtos novos											
B. 	Para produtos já existentes.											
2												
Essa área ou seus times participam de quais fases no desenvolvimento de novos produtos?												
3												
Qual a participação de vocês quando existem indisponibilidades nos ambientes?												
Quais os pricipais desafios e pontos fortes encontrados nesses momentos?												
Qual tratativa adotada após o fim dessa indisponibilidade?												
4												
Vocês usam Pipelines,  CI/CD e automação (incusive de qualidade e segurança), sejam ferramentas e processos, caso sim quais? Vocês acreditam que isso agrega no trabalho de vocês?												
5												
    Você utiliza seus dashboards de Infra e Negócio para tomada de decisões? Você tem uma cultura baseada em dados?​												
6												
    Como você garante a confiabilidade (observabilidade e monitoramento) de sua aplicação crítica? Como você avalia se seu usuário está feliz?​												
7												
    Como vocês aprendem com as falhas e incidentes críticos de produção?​												
8												
    Como vocês descobrem as fraquezas de suas aplicações críticas?​ Como fazem as validaçoes de qualidade do código?												
9												
    Como você gerencia os débitos técnicos? Como o PO é envolvido?​												
10												
    Como você está integrando sua infraestrutura na sua transformação Agile / DevOps?​												
11												
    Como vocês entregam os serviços de infraestrutura para seus consumidores, exemplo: desenvolvedores?​												
12												
    O quão confiável está seu portal de indicadores?​ Quais indicadores vocês tem hoje?												
13												
    Como você garante o hardening do seu ambiente?​ Quais projetos existem voltados à essa questão hoje?												
14												
    Qual a sua infraestrutura? On-premise, Cloud pública ou Cloud privada?												
15												
Existe orientação clara, por parte das lideranças, sobre a execução do Gerenciamento de Incidentes / Mudanças / Problemas?												
Existe matriz de comunicação com a indicação dos responsáveis, quando essas tarefas falham?												
												
												






Como é composta a Squad
AE PO BO, desenvolvimento
    Como passam as demandas
        backlog e novo endereça para PO, alinha com BO e leva no ritos para priorização
        Depois de priorizado vai para os Devs

        Teste é feito por time de qualidade, no ambiete de homologação historia por historia por tete regressivo, ajuda também na validação.
        Ferramenta usadano no gitlab, CI/CD testes automatizados 

        Trabalha com modelo ágil, bem maduro na realização dos ritos, discução com clientes, bem maduro em relação ao modelo
        Tem suporte pela AE (Agile Expert) , cada squad tem a figura do AE

        Relacionamento com as outras áreas, acompanha a sustentação que ficam justas, fazem triagem dos incidentes por níveis n1, n2 e n3

        Sustentação sistemas e infrastrutura

        Gerente de porfifolio 

        System team desenv, sustentação e suporte para o código até chegar na Produção
    
        Sustentação, tem indicadores de indisponibilidade do site, com gestão de incidentes, tempo de atendimento, quantos incidentes atende no SLA
        tempo do incidente, está próximo dos 100% de atendimento 

        * Foi solicitado que tipo de indicadores são coletados


        Teste sonarqube
        ansiblem tower
        terraform Enterprise


        Como que é a comunição com as squads

   System team usa todas as automações (Ter uma visão mais ampla para garantir mais autonomia para s squades)
        
        Monitoração app dynamics

        Forma para medir as atividades

        Cada equipe 

Gerencia de suporte 4 cooredenação e dois consultores, com muita profundidade técnico

 - Cuida de infra strutura básica, 14 pessoas 
Middleware Ricardo Monolina - Container, weblogic,filas
Cordenação de banco Tarcio, banco de baixa plataforma, core Oracle com SQL, mongo DB

, Performance e capacidade, estress teste carga, gestão de capacidade de infra estrutura
Sustentação da TIVIT, 80 pessoas, entre terceitos e funcioárois 120, pessos 3000 servidores

SAST, DAST tem somente dois projetos novos
Parte do cloud entra com, na evolução acontece o controle

Ferramenta 

Qua documentado estas este procedimento para recuperação de site

- Tem sistem de Se, Arch Site
- Se sub utilizado, não está integrado
- Te muitas ferramentas, mas precisa saber se são usadas, pois não estão integradas
- Regras de telecom, vies técnico 
- Falar com Telecom 
- Usa qualis, no passado tenable, usa os dois em paralelo
- Fazem pantest
***** Dor muito grande *****
Muito cilos, ferramentas não se falam, falta padronização

Estrutura da área
Diretorio de infra estrutua Nima, william, mais tres coordenadores

Celendai gestão de incidentes, 
Calendai faz gestão da rotina

Sabrina, recurso da calendai e ciele, gestão de problemas, cielo e terceiro e gestão de indisponibilidade, celula de qualidade que gera relario para clienes externos e internos. 

Como controlam adesão ao metodo, tem indicadores dos processos, 
usa MTTR, 

Gestão de mudança e problemas, 

Bonesi: Dor, processo é chato, tem que invertir um bom papo para convencimento, existe possibilidade para melhorias
        - compreenção, 30 tem um percepção boa 70% não, se não seguir vai ser pontuado. 
        - Quando squad trabalho sem controle deixa acarreta vários problemas. 
        - Ferramenta ITSM, SDM Tableu, GMUD validada por pessoal
        - Acorde de nível de serviços funciona bem, está em acordo com as demais áreas
        - Não respira SLA, não se baseia nela, não cumprem SLA, cumprem no grito, A TIVIT trabalham por SLA
        - Cliente interno sim é notificado, externo depende da gravidade
        - Divulgação é feita para todas a áreas, sãi feito workshopps para deivulgação dos processos
        - Metas de Go dos indicadores
        - O processos estão bem desenhados a muito tempo. 
        - Não concordam com procedimentos, tem que ser algo mais fácial de usar, uma forma prática de seber tudo que faze ma ti e sua  
          obrigação
        - Quantidade de colaboradores são suficiente para os processo na GSTI são adequados





3 Times, plataformas, cuida do core de cloud, implementação de infra estrutura, sutenta o cloud forma
  - Cluster to go, faz primeiro e segundo nível
  - Suporte Iterprese da AWS
  - Operation Fechado em CI/CD levar até produção Suporte 24x7 cluster to Go
  - Arquitetura cloud e on-primeses, openshift cloud privada

  - Avaliação de maturidade geral das squads, e um pouco melhor segue gitflow
  - Desenvolvimento de código
  - Relacionamento com outras áreas, se aproximou bastante com workshops 
  - Sistema star 
  - Na steira nova tem todo o processo de CI/CD com os devidos controles
  - Falta padronização de desenvolvimento
  - Não tem post mortem definido
  - Aws usa secret como segurança, valt
 


--------------------------------------------------
