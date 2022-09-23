
echo "option_settings:
  "aws:elasticbeanstalk:container:python":
    WSGIPath: application:application"

2&1 >> elasticBeanStalk.py




POSTMAN


aws ecr get-login --no-include-email --region sa-east-1
aws ecr get-login-password --region sa-east-1 | docker login --username AWS --password-stdin 123721783999.dkr.ecr.sa-east-1.amazonaws.com
aws eks update-kubeconfig --region sa-east-1 --name mac-dev




CUIDADO não usar esses comandos

aws eks update-cluster-config --region sa-east-1 --name MAC-DEV --resources-vpc-config 
\endpointPublicAccess=true,publicAccessCidrs="18.228.250.18/32",endpointPrivateAccess=true
aws eks describe-update --region sa-east-1 --name MAC-DEV --update-id some-entropy-id

ping 99AD65B8D177D04205B5493185C3987F.gr7.sa-east-1.eks.amazonaws.com
 18.228.250.18
 


POST /salesforce-api/estabelecimentos/indicacoes/worker HTTP/1.1
Host: apis-mac-staging-v2.devcdt.com.br
Content-Type: application/json
Accept: application/json
{
"nome": "ROSE MARIA PINTO NATEL",
"cpf": "00001044699",
"email": "macnaima@macnamara",
"telefone": "12981495686",
"razaoSocial": "LANCHES E ROTISSERIE DO MARIO LTDA.",
"cnpj": "62746110000235",
"nomeFantasia": "Nome Fantasia 1"	
}




Host: apis-mac-staging-v2.devcdt.com.br/salesforce-api/estabelecimentos/indicacoes/worker/POST
  HTTP/1.1



POST /salesforce-api/estabelecimentos/indicacoes/worker HTTP/1.1
Host: apis-mac-staging-v2.devcdt.com.br
Content-Type: application/json
Accept: application/json
{
"nome": "ROSE MARIA PINTO NATEL",
"cpf": "00001044699",
"email": "macnaima@macnamara",
"telefone": "12981495686",
"razaoSocial": "LANCHES E ROTISSERIE DO MARIO LTDA.",
"cnpj": "62746110000235",
"nomeFantasia": "Nome Fantasia 1"	
}




Host: apis-mac-staging-v2.devcdt.com.br/salesforce-api/estabelecimentos/indicacoes/worker/POST
  HTTP/1.1





# ava
 AVA
 <br>
aws s3 cp s3://eks-mac/financeiro/ ./s3files --profile default --recursive


Ativar windows linha de comando
slmgr /ipk W269N-WFGWX-YVC9B-4J6C9-T83GX slmgr /skms kms.xspace.in slmgr /ato

$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$




aws configure
aws ecr get-login-password --region sa-east-1 | docker login --username AWS --password-stdin AWSUserId.dkr.ecr.sa-east-1.amazonaws.com
docker build --file Dockerfile --pull -t localhost:tst-nr-667 --no-cache .

mvn package
mvnw clean install 
mvnw clean install -DskipTests

kubectl config
sudo apt-get update
sudo apt-get install -y apt-transport-https ca-certificates curl
sudo curl -fsSLo /usr/share/keyrings/kubernetes-archive-keyring.gpg https://packages.cloud.google.com/apt/doc/apt-key.gpg
echo "deb [signed-by=/usr/share/keyrings/kubernetes-archive-keyring.gpg] https://apt.kubernetes.io/ kubernetes-xenial main" | sudo tee /etc/apt/sources.list.d/kubernetes.list
sudo apt-get update
sudo apt-get install -y kubectl

curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"
sudo install -o root -g root -m 0755 kubectl /usr/local/bin/kubectl
kubectl version --client

kubectl get pod -A |grep post
kubectl config get-contexts
wget https://get.helm.sh/helm-v3.4.1-linux-amd64.tar.gz
tar xvf helm-v3.4.1-linux-amd64.tar.gz
sudo mv linux-amd64/helm /usr/local/bin
helm version


docker login -u AWS -p
 some-Token
 https://123721783999.dkr.ecr.sa-east-1.amazonaws.com





 aws ecr get-login-password --region sa-east-1 | docker login --username AWS --password-stdin 123721783999.dkr.ecr.sa-east-1.amazonaws.com
docker build --file Dockerfile --pull -t localhost:tst-nr-667 --no-cache .
