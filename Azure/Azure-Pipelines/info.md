- Consul
  - DEV/QA
      Url: http://az-us-dev-consul-01.labmac.corp:8500 
      IP: 10.186.16.6
      Status: Ok

- Azure Service Bus
  - DEV/QA
      Host: sb://macmarketplaceeventhubdev01.servicebus.windows.net
  - DEV
      Host: sb://productapisbdev.servicebus.windows.net
  - QA
      Host: sb://productapisbqa.servicebus.windows.net

- MongoDb
  - DEV 
      Host: mongodb.dev.machinacorp.com.br
      IP: 40.117.255.127
      Private: 10.186.8.5
      Status: Ok

  - QA
      Host: mongodb.qa.machinacorp.com.br
      IP: 104.211.49.135
      Private: 10.187.8.7
      Status: NOK Privado / NOK Public
    
- SQL Server
  - DEV 
      Host: macsqllivedev.webpremios.com.br
      IP: 40.114.52.141
      Private: 10.186.8.4
      Status: Ok

  - QA - 
      Host: macsqllivehml.eastus.cloudapp.azure.com
      IP: 13.82.95.56
      Private: 10.187.8.4
      Status: NOK Privado / NOK Public (Liberar NSG)

- Redis
  - DEV/QA 
      Host: MAcMkpRedisQA1.redis.cache.windows.net
  - DEV/QA
      Host: cloudloyaltyproductredisdev.redis.cache.windows.net

- SOLR
  - DEV/QA
      Host: http://solr.qa.machinacorp.com.br:8983
      IP: 20.185.40.178
      Private: 10.187.4.6
      Status: NOK Privado / NOK Public (Liberar NSG)

- Integração Magalu
  - DEV/QA
      Host: https://corporativo.magazinemacna.com.br
  - DEV/QA
      Host: https://macnaima-magazinemacna.getsandbox.com

- Integração Macnaima
  - DEV/QA
      Host: https://catalog-integration.macnaima.com.br
  - DEV/QA
      Host: https://macnaima.getsandbox.com

- Integração Catalogo Api
  - DEV
      Host: https://dev1-marketplace-integracao-catalogo-api.webpremios.com.br
      App: macmarketplaceintegracaocatalogoapidev01
  - QA
      Host: https://qa1-marketplace-integracao-catalogo-api.webpremios.com.br
      App: macmarketplaceIntegracaoCatalogoApiQA1

- Integração Partner Hub
  - DEV/QA
      Host: https://partnerhubapimqa.azure-api.net
  - DEV/QA
      Host: https://partnerhub-saga.getsandbox.com
