Adicionar DevOps-Tree.jpg aqui


# dev-mac-infra

Este repositório tem como objetivo guardar arquivos de configuração, imagens do Docker (utils e serviços em comum), Jenkinsfiles, Kubernetes e quaisquer outros documentos necessários pelos DevOps.

## Organização
O repositório está organizado da seguinte forma:
- **jenkinsfile**: para arquivos de pipeline do Jenkins. Pode-se compartilhar arquivos com padrões diferentes para estudos ou aplicações específicas, porém sempre levando em consideração o que foi discutido no chapter de DevOps, é possível também levar a discussão para o grupo, caso seja necessário. 

- **docker**: para arquivos referentes ao Docker e seus derivados, ou seja, pode-se adicionar arquivos do docker-compose, arquivos de imagens (*e.g.* Java/Mule) e etc. card lembrar que essa separação deve ser feita por sub-diretórios.

- **kubernetes**: para arquivos do Kubernetes. Arquivos que facilitem a configuração de componentes do cluster (*e.g.* yaml de RBAC, PVC, Roles, Services) para que tenhamos uma base de conhecimento e tornar mais ágil a criação e administração destes. **OBS: Arquivos do Rancher podem ser adicionados também, mas somente a versão a partir da 2.X.**

## Contribuição
Para contribuir com o repositório, crie uma branch a partir da **master** e faça as suas modificações, ao terminar, abra um Pull Request (PR) e adicione como revisor algum DevOps do chapter, comunicando-o previamente. 
