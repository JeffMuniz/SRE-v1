# Macnaima

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
