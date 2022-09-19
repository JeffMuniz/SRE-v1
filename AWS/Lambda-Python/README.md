# Aws-Lambda-Python
Return random word from Lambda API and DynamoDB and some other ServLess


Yet to come:
![image](https://user-images.githubusercontent.com/34946878/128120289-0b9c7819-5281-475c-adff-fb57d96142d6.png)


HOW TO USE:
<hr size="8" width="90%" color="red">  
Windows

Install npm (from NodeJS), download, next, next finish
<hr size="8" width="40%" color="red">  
https://nodejs.org/en/
<hr size="8" width="40%" color="red">  
Install wscat application (https://github.com/websockets/wscat)

From a Administrator Power Shell terminal run:
<hr size="8" width="40%" color="red">  
npm install -g wscat
<hr size="8" width="40%" color="red">  
Connect to Lambda API:

wscat -c wss://3w0zg1oeh0.execute-api.us-east-2.amazonaws.com/production

<hr size="8" width="40%" color="red">  
Run a JSON formated query:

{"action":"word"}

<hr size="8" width="90%" color="red">  

Linux

sudo apt-get update

or maybe sudo apt-get upgrade?

sudo apt install npm

sudo npm install -g wscat

wscat -c wss://3w0zg1oeh0.execute-api.us-east-2.amazonaws.com/production

{"action":"word"}
