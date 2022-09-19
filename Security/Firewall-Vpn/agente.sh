#!/bin/bash

DISTRIBUICAO=`cat /etc/*-release`

#################################################
#			UBUNTU			#
#################################################

echo "$DISTRIBUICAO" | egrep 'Ubuntu' > /dev/null

if [ $? = 0 ] ;
  then

echo
echo
echo "Instalando o agente Zabbix para Ubuntu 14 ou superior"
sleep 2
echo "Digite o endereço do seu Zabbix Proxy:"
read VARIAVEL
echo "O endereço do seu Zabbix Proxy é $VARIAVEL"
while true; do
    read -p "Deseja continuar? (S/N)" yn
    case $yn in
        [YySs]* ) break;;
        [Nn]* ) exit;;
        * ) echo "Selecione S(Sim) ou N(não)!";;
    esac
done


wget http://repo.zabbix.com/zabbix/3.0/ubuntu/pool/main/z/zabbix-release/zabbix-release_3.0-1+trusty_all.deb
dpkg -i zabbix-release_3.0-1+trusty_all.deb
apt-get update
apt-get install zabbix-agent
wget http://noc.vantagebrazil.com/zabbix_agentd.conf
sed -i -e "s/INSERIRVARIAVEL/$VARIAVEL/g" zabbix_agentd.conf
cat zabbix_agentd.conf > /etc/zabbix/zabbix_agentd.conf
rm -rf zabbix_agentd.conf
rm -rf zabbix-release_3.0-1+trusty_all.deb
/usr/sbin/zabbix_agentd

else

#################################################
#			DEBIAN 7		#
#################################################

echo "$DISTRIBUICAO" | egrep 'Debian 7' > /dev/null

if [ $? = 0 ] ;
  then

echo
echo
echo "Instalando o agente Zabbix para Debian 7"
sleep 2
echo "Digite o endereço do seu Zabbix Proxy:"
read VARIAVEL
echo "O endereço do seu Zabbix Proxy é $VARIAVEL"
while true; do
    read -p "Deseja continuar? (S/N)" yn
    case $yn in
        [YySs]* ) break;;
        [Nn]* ) exit;;
        * ) echo "Selecione S(Sim) ou N(não)!";;
    esac
done


wget http://repo.zabbix.com/zabbix/3.0/debian/pool/main/z/zabbix-release/zabbix-release_3.0-1+wheezy_all.deb
dpkg -i zabbix-release_3.0-1+wheezy_all.deb
apt-get update
apt-get install zabbix-agent
wget http://noc.vantagebrazil.com/zabbix_agentd.conf
cat zabbix_agentd.conf > /etc/zabbix/zabbix_agentd.conf
rm -rf zabbix_agentd.conf
rm -rf zabbix-release_3.0-1+trusty_all.deb
/usr/sbin/zabbix_agentd

else

#################################################
#			DEBIAN 8		#
#################################################

echo "$DISTRIBUICAO" | egrep 'Debian 8' > /dev/null

if [ $? = 0 ] ;
  then

echo
echo
echo "Instalando o agente Zabbix para Debian 8"
sleep 2
echo "Digite o endereço do seu Zabbix Proxy:"
read VARIAVEL
echo "O endereço do seu Zabbix Proxy é $VARIAVEL"
while true; do
    read -p "Deseja continuar? (S/N)" yn
    case $yn in
        [YySs]* ) break;;
        [Nn]* ) exit;;
        * ) echo "Selecione S(Sim) ou N(não)!";;
    esac
done

wget http://repo.zabbix.com/zabbix/3.0/debian/pool/main/z/zabbix-release/zabbix-release_3.0-1+jessie_all.deb
dpkg -i zabbix-release_3.0-1+jessie_all.deb
apt-get update
apt-get install zabbix-agent
wget http://noc.vantagebrazil.com/zabbix_agentd.conf
cat zabbix_agentd.conf > /etc/zabbix/zabbix_agentd.conf
rm -rf zabbix_agentd.conf
rm -rf zabbix-release_3.0-1+trusty_all.deb
/usr/sbin/zabbix_agentd

else

#################################################
#			CENTOS 7		#
#################################################

echo "$DISTRIBUICAO" | egrep 'CentOS Linux 7' > /dev/null

if [ $? = 0 ] ;
  then

echo
echo
echo "Instalando o agente Zabbix para CentOS Linux 7"
sleep 2
echo "Digite o endereço do seu Zabbix Proxy:"
read VARIAVEL
echo "O endereço do seu Zabbix Proxy é $VARIAVEL"
while true; do
    read -p "Deseja continuar? (S/N)" yn
    case $yn in
        [YySs]* ) break;;
        [Nn]* ) exit;;
        * ) echo "Selecione S(Sim) ou N(não)!";;
    esac
done

rpm -ivh http://repo.zabbix.com/zabbix/3.0/rhel/7/x86_64/zabbix-release-3.0-1.el7.noarch.rpm
yum install zabbix-agent
curl -OL http://noc.vantagebrazil.com/zabbix_agentd.conf
cat zabbix_agentd.conf > /etc/zabbix/zabbix_agentd.conf
rm -rf zabbix_agentd.conf
/usr/sbin/zabbix_agentd

else

#################################################
#			ORACLE 6.5		#
#################################################

echo "$DISTRIBUICAO" | egrep 'Oracle Linux Server release 6.5' > /dev/null

if [ $? = 0 ] ;
  then

echo
echo
echo "Instalando o agente Zabbix para Oracle Linux 6.5"
sleep 2
echo "Digite o endereço do seu Zabbix Proxy:"
read VARIAVEL
echo "O endereço do seu Zabbix Proxy é $VARIAVEL"
while true; do
    read -p "Deseja continuar? (S/N)" yn
    case $yn in
        [YySs]* ) break;;
        [Nn]* ) exit;;
        * ) echo "Selecione S(Sim) ou N(não)!";;
    esac
done

rpm -ivh http://repo.zabbix.com/zabbix/2.2/rhel/6/x86_64/zabbix-release-2.2-1.el6.noarch.rpm
yum install zabbix-agent
curl -OL http://noc.vantagebrazil.com/zabbix_agentd.conf
cat zabbix_agentd.conf > /etc/zabbix/zabbix_agentd.conf
rm -rf zabbix_agentd.conf
/usr/sbin/zabbix_agentd

else

#################################################
#			CENTOS 5		#
#################################################

echo "$DISTRIBUICAO" | egrep 'CentOS release 5' > /dev/null

if [ $? = 0 ] ;
  then

echo
echo
echo "Instalando o agente Zabbix para CentOS 5"
sleep 2
echo "Digite o endereço do seu Zabbix Proxy:"
read VARIAVEL
echo "O endereço do seu Zabbix Proxy é $VARIAVEL"
while true; do
    read -p "Deseja continuar? (S/N)" yn
    case $yn in
        [YySs]* ) break;;
        [Nn]* ) exit;;
        * ) echo "Selecione S(Sim) ou N(não)!";;
    esac
done

rpm -ivh http://repo.zabbix.com/zabbix/2.2/rhel/5/x86_64/zabbix-release-2.2-1.el5.noarch.rpm
yum install zabbix-agent
curl -OL http://noc.vantagebrazil.com/zabbix_agentd.conf
cat zabbix_agentd.conf > /etc/zabbix/zabbix_agentd.conf
rm -rf zabbix_agentd.conf
/usr/sbin/zabbix_agentd


else

#################################################
#			OUTROS/RHEL 7		#
#################################################

if [ $? = 1 ] ;
  then


echo
echo
echo "Não encontrei o Zabbix para seu sistema, você está usando RedHat 7?"
sleep 2
echo "Digite o endereço do seu Zabbix Proxy:"
read VARIAVEL
echo "O endereço do seu Zabbix Proxy é $VARIAVEL"
while true; do
    read -p "Deseja continuar? (S/N)" yn
    case $yn in
        [YySs]* ) break;;
        [Nn]* ) exit;;
        * ) echo "Selecione S(Sim) ou N(não)!";;
    esac
done



rpm -ivh http://repo.zabbix.com/zabbix/3.0/rhel/7/x86_64/zabbix-release-3.0-1.el7.noarch.rpm
yum install zabbix-agent
curl -OL http://noc.vantagebrazil.com/zabbix_agentd.conf
cat zabbix_agentd.conf > /etc/zabbix/zabbix_agentd.conf
rm -rf zabbix_agentd.conf
/usr/sbin/zabbix_agentd

fi
fi
fi
fi
fi
fi
fi
