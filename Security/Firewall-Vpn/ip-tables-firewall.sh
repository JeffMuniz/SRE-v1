#!/bin/bash
#
###########################################################
#
# Firewall IS
#
###########################################################

### Executa configuracoes iniciais

#sh /etc/sysconfig/firewall.head
set=x
#sh /etc/sysconfig/rotas.sh
###########################################################
# Definicoes iptables e interface
###########################################################
fw=/sbin/iptables

ip_in=10.0.20.2
eth_in=eth1
net_in=10.0.0.0/16
log_level=warning

###########################################################

#ip Antigo Link desativado 30/07/2013 ticket 14676
#ip_out=187.62.216.58
#eth_out=eth2
#log_level=warning


#ip_out=200.155.168.26
ip_out=177.66.153.58
#ip_out2=200.155.168.30
eth_out=eth0
log_level=warning


ip_out_2=201.23.70.122
eth_out_2=eth3
log_level=warning


##########################################################
ip=/sbin/ip
###########################################################
#BALANCE
###########################################################

# Joga pacotes marcados
#------------------------------------------------------------

$ip route flush cache
$ip rule add fwmark 3 table 30 prio 30
$ip route add default via 201.23.70.121 dev eth3 table tmc





##########################################################
# Geral antenas
##########################################################

#############################################################
# Hosts internos e Redes internas
#############################################################

intersolution=189.19.43.55
intersolution2=177.189.206.232
intersolution3=174.129.216.93
intersolution_cloud=54.84.63.26

############################################################
# Redes Externas
############################################################

#############################################################
# Zerando  Tabelas
#############################################################
$fw -t filter -F
$fw -t nat -F
$fw -t mangle -F
$fw -X

#############################################
# Criando Tables
#############################################
$fw -N F_SSH                                    #22
$fw -N F_SMTP                                   #25
$fw -N F_DNS                                    #53
$fw -N F_HTTP                                   #80
$fw -N F_LDAP                                   #389
$fw -N F_HTTPS                                  #443
$fw -N F_SMB                                    #445 - 139 Samba
$fw -N F_ORA                                    #1521
$fw -N F_MYSQL                                  #3306
$fw -N F_TSERVER                                #3389
$fw -N F_GED                                    #3726
$fw -N F_ICMP                                   #
$fw -N F_GERAL                                  #controle entre redes e outros
$fw -N F_TOMCAT                                 #8080
$fw -N F_CONSOLE                                #104
$fw -N F_WSUS                                   #8530
$fw -N F_VNC                                    #5900
$fw -N F_SQUID                                    #3128
$fw -N F_WEBMIN                                    #10000



#############################################
# Bloqueio inicial
#############################################
$fw -P INPUT DROP
$fw -P FORWARD DROP
$fw -P OUTPUT ACCEPT
$fw -t nat -P PREROUTING ACCEPT
$fw -t nat -P POSTROUTING ACCEPT
$fw -t nat -P OUTPUT ACCEPT
$fw -t mangle -P PREROUTING ACCEPT
$fw -t mangle -P OUTPUT ACCEPT

#############################################
# Tabela INPUT
#############################################

#############################################
# Liberacoes Gerais
#############################################
$fw -A INPUT -i lo -j ACCEPT

#############################################
# Redirecionamentos - INPUT
#############################################
$fw -A INPUT -j F_GERAL
$fw -A FORWARD -p udp -m multiport --dports 67,68 -j DROP
$fw -A FORWARD -j F_GERAL

#############################################
# Tabela F_GERAL
#############################################
#
#  Permitir Conexoes Estabelecidas
#
$fw -A F_GERAL -m state --state ESTABLISHED,RELATED -j ACCEPT
$fw -I FORWARD -m state --state ESTABLISHED,RELATED -j ACCEPT

#############################################
#
# Liberacoes totais
#$fw -A F_GERAL -s $net_in -j ACCEPT
#$fw -A F_GERAL -d $net_out -j ACCEPT




#############################################
# Redirecionamentos por servico
#############################################
$fw -A F_GERAL -p tcp                   --dport         22                      -j      F_SSH
$fw -A F_GERAL -p tcp -m multiport      --dport         25,587                  -j      F_SMTP
$fw -A F_GERAL -p udp                   --dport         53                      -j      F_DNS
$fw -A F_GERAL -p tcp                   --dport         80                      -j      F_HTTP
$fw -A F_GERAL -p tcp -m multiport      --dport         389,636,3268,3269       -j      F_LDAP
$fw -A F_GERAL -p tcp                   --dport         443                     -j      F_HTTPS
$fw -A F_GERAL -p tcp                   --dport         1521                    -j      F_ORA
$fw -A F_GERAL -p tcp                   --dport         3306                    -j      F_MYSQL
$fw -A F_GERAL -p tcp                   --dport         3389                    -j      F_TSERVER
$fw -A F_GERAL -p tcp                   --dport         3726                    -j      F_GED
$fw -A F_GERAL -p udp                   --dport         1434                    -j      F_GED
$fw -A F_GERAL -p tcp -m multiport      --dport         135,139,445             -j      F_SMB
$fw -A F_GERAL -p udp -m multiport      --dport         135,139,445             -j      F_SMB
$fw -A F_GERAL -p icmp                                                          -j      F_ICMP
$fw -A F_GERAL -p tcp -m multiport      --dport         8080,8082               -j      F_TOMCAT
$fw -A F_GERAL -p tcp                   --dport         104                     -j      F_CONSOLE
$fw -A F_GERAL -P tcp                   --dport         8530                    -j      F_WSUS
$fw -A F_GERAL -p tcp                   --dport         5900                    -j      F_VNC
$fw -A F_GERAL -p tcp                   --dport         3128                    -j      F_SQUID
$fw -A F_GERAL -p tcp                   --dport         10000                    -j      F_WEBMIN


#############################################
# Fim tabela geral
#  Logando
#
#$fw -A F_GERAL -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: Geral - "
#$fw -A F_GERAL -j LOG --log-level 1 --log-prefix "Firewall: Geral - "
#$fw -A F_GERAL -j DROP

#############################################
# Inicio das cadeias criadas
#############################################
#
#############################################
# Tabela F_WSUS
#############################################
#$fw -A F_WSUS -s $net_prados -d $lua -j ACCEPT
$fw -A F_WSUS -j LOG --log-level 1 --log-prefix "Firewall: WSUS - "
$fw -A F_WSUS -j DROP

#############################################
# Tabela F_CONSOLE
#############################################
#$fw -A F_CONSOLE -d $pacs -j ACCEPT
$fw -A F_CONSOLE -s 192.168.130.88 -d 192.168.198.151 -j ACCEPT
$fw -A F_CONSOLE -j LOG --log-level 1 --log-prefix "Firewall: CONSOLE - "
$fw -A F_CONSOLE -j DROP

#############################################
# Tabela F_SMB
#############################################
#$fw -A F_SMB -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: SMB - "
#$fw -A F_SMB -j LOG --log-level 1 --log-prefix "Firewall: SMB - "
$fw -A F_SMB -j DROP

#############################################
# Tabela F_SSH
#############################################
$fw -A F_SSH -s $intersolution -j ACCEPT
$fw -A F_SSH -s $net_in -j ACCEPT
$fw -A F_SSH -s $intersolution2 -j ACCEPT
$fw -A F_SSH -s $intersolution3 -j ACCEPT
$fw -A F_SSH -s $intersolution_cloud -j ACCEPT
$fw -A F_SSH -m limit --limit 6/m  --limit-burst 6  -j LOG  --log-level $log_level --log-prefix "Firewall: SSH - "
$fw -A F_SSH -j LOG --log-level 1 --log-prefix "Firewall: SSH - "
$fw -A F_SSH -j DROP

#############################################
# Tabela F_DNS
#############################################
$fw -A F_DNS -s $net_in -j ACCEPT
$fw -A F_DNS -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: DNS - "
$fw -A F_DNS -j LOG --log-level 1 --log-prefix "Firewall: DNS - "
$fw -A F_DNS -j DROP

#############################################
# Tabela F_HTTP
#############################################
$fw -A F_HTTP -s $net_in -j ACCEPT
$fw -A F_HTTP -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: HTTP - "
$fw -A F_HTTP -j LOG --log-level 1 --log-prefix "Firewall: HTTP - "
$fw -A F_HTTP -j DROP

#############################################
# Tabela F_HTTP
#############################################
$fw -A F_SQUID -s $net_in -j ACCEPT
$fw -A F_SQUID -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: HTTP - "
$fw -A F_SQUID -j LOG --log-level 1 --log-prefix "Firewall: HTTP - "
$fw -A F_SQUID -j DROP


#############################################
# Tabela F_HTTPS
#############################################
$fw -A F_HTTPS -s $net_in -j ACCEPT
$fw -A F_HTTPS -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: HTTPS - "
$fw -A F_HTTPS -j LOG --log-level 1 --log-prefix "Firewall: HTTPS - "
$fw -A F_HTTPS -j DROP

#############################################
# Tabela F_MYSQL
#############################################
#$fw -A F_MYSQL -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: MYSQL - "
$fw -A F_MYSQL -j LOG --log-level 1 --log-prefix "Firewall: MYSQL - "
$fw -A F_MYSQL -j DROP

#############################################
# Tabela F_ORA
#############################################
$fw -A F_ORA -i eth0 -j ACCEPT
#$fw -A F_ORA -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: ORACLE - "
$fw -A F_ORA -j LOG --log-level 1 --log-prefix "Firewall: ORACLE - "
$fw -A F_ORA -j DROP

#############################################
# Tabela F_TSERVER
#############################################
$fw -A F_TSERVER -s $intersolution_cloud -j ACCEPT
#$fw -A F_TSERVER -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: TSERVER - "
$fw -A F_TSERVER -j LOG --log-level 1 --log-prefix "Firewall: TSERVER - "
$fw -A F_TSERVER -j DROP

#############################################
# Tabela F_TOMCAT
#############################################
#$fw -A F_TOMCAT -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: TOMCAT - "
$fw -A F_TOMCAT -j LOG --log-level 1 --log-prefix "Firewall: TOMCAT - "
$fw -A F_TOMCAT -j DROP

#############################################
# Tabela F_SMTP
#############################################
$fw -A F_SMTP -s $net_in -j ACCEPT
#$fw -A F_SMTP -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: SMTP - "
$fw -A F_SMTP -j LOG --log-level 1 --log-prefix "Firewall: SMTP - "
$fw -A F_SMTP -j DROP

#############################################
# Tabela F_LDAP
#############################################
#$fw -A F_LDAP -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: LDAP - "
$fw -A F_LDAP -j LOG --log-level 1 --log-prefix "Firewall: LDAP - "
$fw -A F_LDAP -j DROP


#############################################
# Fim tabela geral
#  Logando
#
#$fw -A F_GERAL -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: Geral - "
$fw -A F_GERAL -j LOG --log-level 1 --log-prefix "Firewall: Geral - "
$fw -A F_GERAL -j DROP


#############################################
# Tabela F_ICMP
#############################################
$fw -A F_ICMP -p icmp --icmp-type echo-request -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type echo-reply -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type destination-unreachable      -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   network-unreachable        -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   host-unreachable           -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   protocol-unreachable       -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   port-unreachable           -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   source-route-failed         -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   network-unknown            -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   host-unknown               -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   network-prohibited          -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   host-prohibited            -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   TOS-network-unreachable    -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   TOS-host-unreachable       -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   communication-prohibited    -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type time-exceeded                -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   ttl-zero-during-transit     -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   ttl-zero-during-reassembly  -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type parameter-problem             -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   ip-header-bad               -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type   required-option-missing     -m limit --limit 1/s -j ACCEPT
#$fw -A F_ICMP -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: ICMP - "
$fw -A F_ICMP -j LOG --log-level 1 --log-prefix "Firewall: ICMP - "
$fw -A F_ICMP -j DROP

#############################################
# Tabela F_VNC
#############################################
#$fw -A F_VNC -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: VNC - "
$fw -A F_VNC -j LOG --log-level 1 --log-prefix "Firewall: VNC - "
$fw -A F_VNC -j DROP

$fw -A F_WEBMIN -s $net_in -j ACCEPT
#$fw -A F_WEBMIN -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: VNC - "
$fw -A F_WEBMIN -j LOG --log-level 1 --log-prefix "Firewall: VNC - "
$fw -A F_WEBMIN -j DROP



#############################################
# Tabela F_GED
#############################################
#$fw -A F_GED -m limit --limit 6/m  --limit-burst 6 -j LOG  --log-level $log_level --log-prefix "Firewall: GED - "
$fw -A F_GED -j LOG --log-level 1 --log-prefix "Firewall: GED - "
$fw -A F_GED -j DROP


#LIBERACAO CONECTIVIDADE SOCIAL
#
iptables -I FORWARD -p tcp --dport 2631 -j ACCEPT
iptables -I FORWARD -p udp --dport 2631 -j ACCEPT
iptables -I FORWARD -p tcp --dport 2631 -m state --state NEW -j ACCEPT # GRRF - Conectividade Social
iptables -I FORWARD -p tcp --dport 3001 -m state --state NEW -j ACCEPT # GRRF - Cnectividade Social
iptables -I FORWARD -d 200.201.173.68 -j ACCEPT
iptables -I FORWARD -p tcp --dport 3000 -j ACCEPT



# Liberar Conectividade Social para toda a rede
CAIXA="200.147.67.0/24 200.252.47.0/24 200.201.160.0/20"
for ip in $CAIXA ; do
  iptables -t nat -I PREROUTING -p tcp -d $ip -j ACCEPT
  iptables -I FORWARD -p tcp -d $ip -j ACCEPT
  iptables -t nat -I POSTROUTING  -d $ip -j MASQUERADE
done

# Ticket 11299
ip=187.35.104.221 
  iptables -t nat -I PREROUTING -p tcp -d $ip -j ACCEPT
  iptables -I FORWARD -p tcp -d $ip -j ACCEPT
  iptables -t nat -I POSTROUTING  -d $ip -j MASQUERADE


##PROXY TRANSPARENTE

#iptables -t nat -A PREROUTING -s 10.0.0.0/16 -p tcp --dport 80 -j REDIRECT --to-port 3128


####TOMCAT CROSS
iptables -I FORWARD -p tcp -d 10.0.20.12 -j ACCEPT
iptables -t nat -I PREROUTING -p tcp --dport 8083 -d $ip_out -j DNAT --to 10.0.20.12:8083
iptables -I FORWARD -p tcp -s 10.0.20.12 -j ACCEPT

####TICKET #12030
iptables -I FORWARD -p tcp -d 10.0.20.11 -j ACCEPT
iptables -t nat -I PREROUTING -p tcp --dport 8082 -d $ip_out -j DNAT --to 10.0.20.11:8082
iptables -I FORWARD -p tcp -s 10.0.20.11 -j ACCEPT

####ACESSO SEDE
iptables -I FORWARD -p tcp -d 10.0.21.10 -j ACCEPT
iptables -t nat -I PREROUTING -p tcp --dport 3399 -d $ip_out -j DNAT --to 10.0.21.10:3389

iptables -I FORWARD -p tcp -d 10.0.20.4 -j ACCEPT
iptables -t nat -I PREROUTING -p tcp --dport 3398 -s $intersolution  -d $ip_out -j DNAT --to 10.0.20.4:3389
iptables -t nat -I PREROUTING -p tcp --dport 3398 -s $intersolution2 -d $ip_out -j DNAT --to 10.0.20.4:3389
iptables -I FORWARD -p tcp -s 10.0.20.4 -j ACCEPT

iptables -t nat -I PREROUTING -p tcp --dport 3398 -s $intersolution2  -d $ip_out_2 -j DNAT --to 10.0.20.4:3389


####ACESSO NCI
iptables -I FORWARD -p tcp -d 10.0.20.11 -j ACCEPT
iptables -t nat -I PREROUTING -p tcp --dport 8080 -d $ip_out -j DNAT --to 10.0.20.11:8080
iptables -I FORWARD -p tcp -s 10.0.20.11 -j ACCEPT

####ACESSO VNC Breno 19/02 Ticket: 13440
iptables -I FORWARD -p tcp -d 10.0.20.32 -j ACCEPT
iptables -t nat -I PREROUTING -p tcp --dport 5909 -d $ip_out -j DNAT --to 10.0.20.32:5900

iptables -I FORWARD -p tcp -d 10.0.20.44 -j ACCEPT
iptables -t nat -I PREROUTING -p tcp --dport 3389 -d $ip_out -j DNAT --to 10.0.20.44:3389
iptables -t nat -I POSTROUTING -d 10.0.20.44 -j SNAT --to $ip_in


###Servidor Banco 
iptables -I FORWARD -s 10.0.20.3 -j ACCEPT
iptables -t nat -I PREROUTING -s 10.0.20.3 -j ACCEPT
iptables -t mangle -I PREROUTING -s 10.0.20.3 -j MARK --set-mark 2
iptables -t mangle -I  OUTPUT  -s 10.0.20.3 -j MARK --set-mark 2
iptables -t mangle -I FORWARD -s 10.0.20.3 -j MARK --set-mark 2
iptables -t mangle -I POSTROUTING -s 10.0.20.3 -j MARK --set-mark 2




iptables -I FORWARD -s 10.0.20.245 -j ACCEPT
iptables -t nat -I PREROUTING -s 10.0.20.245 -j ACCEPT
iptables -t mangle -I PREROUTING -s 10.0.20.245 -j MARK --set-mark 2
iptables -t mangle -I  OUTPUT  -s 10.0.20.245 -j MARK --set-mark 2
iptables -t mangle -I FORWARD -s 10.0.20.245 -j MARK --set-mark 2
iptables -t mangle -I POSTROUTING -s 10.0.20.245 -j MARK --set-mark 2

###LIBERACAO CARDIOSNET### TICKET-14428

iptables -I FORWARD -d 186.202.95.6 -j ACCEPT
iptables -t nat -I PREROUTING -d 186.202.95.6 -j ACCEPT
iptables -t mangle -I PREROUTING -d 186.202.95.6 -j MARK --set-mark 2
iptables -t mangle -I  OUTPUT  -d 186.202.95.6 -j MARK --set-mark 2
iptables -t mangle -I FORWARD -d 186.202.95.6 -j MARK --set-mark 2
iptables -t mangle -I POSTROUTING -d 186.202.95.6 -j MARK --set-mark 2

iptables -I FORWARD -d 186.202.132.169 -j ACCEPT
iptables -t nat -I PREROUTING -d 186.202.132.169 -j ACCEPT
iptables -t mangle -I PREROUTING -d 186.202.132.169 -j MARK --set-mark 2
iptables -t mangle -I  OUTPUT  -d 186.202.132.169 -j MARK --set-mark 2
iptables -t mangle -I FORWARD -d 186.202.132.169 -j MARK --set-mark 2
iptables -t mangle -I POSTROUTING -d 186.202.132.169 -j MARK --set-mark 2


modprobe ip_nat_tftp
modprobe ip_nat_ftp
modprobe ip_conntrack_ftp
modprobe ip_conntrack_tftp


iptables -I FORWARD -d 200.219.204.115 -j ACCEPT
iptables -t nat -I PREROUTING -d 200.219.204.115 -j ACCEPT
iptables -t mangle -I PREROUTING -d 200.219.204.115 -j MARK --set-mark 2
iptables -t mangle -I  OUTPUT  -d 200.219.204.115 -j MARK --set-mark 2
iptables -t mangle -I FORWARD -d 200.219.204.115 -j MARK --set-mark 2
iptables -t nat -I POSTROUTING -d 200.219.204.115 -j MASQUERADE



####

iptables -I FORWARD -d 200.155.164.243 -j ACCEPT
iptables -t nat -I PREROUTING -d 200.155.164.243 -j ACCEPT
iptables -t mangle -I PREROUTING -d 200.155.164.243 -j MARK --set-mark 2
iptables -t mangle -I  OUTPUT  -d 200.155.164.243 -j MARK --set-mark 2
iptables -t mangle -I FORWARD -d 200.155.164.243 -j MARK --set-mark 2
iptables -t nat -I POSTROUTING -d 200.155.164.243 -j MASQUERADE


iptables -I FORWARD -s 10.0.21.35 -j ACCEPT
iptables -t nat -I PREROUTING -s 10.0.21.35 -j ACCEPT
iptables -t mangle -I PREROUTING -s 10.0.21.35 -j MARK --set-mark 2
iptables -t mangle -I  OUTPUT  -s 10.0.21.35 -j MARK --set-mark 2
iptables -t mangle -I FORWARD -s 10.0.21.35 -j MARK --set-mark 2
iptables -t mangle -I POSTROUTING -s 10.0.21.35 -j MARK --set-mark 2


#iptables -I FORWARD -s 177.189.206.232 -j ACCEPT
#iptables -t nat -I PREROUTING -s 177.189.206.232 -j ACCEPT
#iptables -t mangle -I PREROUTING -s 177.189.206.232 -j MARK --set-mark 2
#iptables -t mangle -I  OUTPUT  -s 177.189.206.232 -j MARK --set-mark 2
#iptables -t mangle -I FORWARD -s 177.189.206.232 -j MARK --set-mark 2
#iptables -t mangle -I POSTROUTING -s 177.189.206.232 -j MARK --set-mark 2


iptables -I FORWARD -s 10.0.20.37 -j ACCEPT
iptables -t nat -I PREROUTING -s 10.0.20.37 -j ACCEPT
iptables -t mangle -I PREROUTING -s 10.0.20.37 -j MARK --set-mark 2
iptables -t mangle -I  OUTPUT  -s 10.0.20.37 -j MARK --set-mark 2
iptables -t mangle -I FORWARD -s 10.0.20.37 -j MARK --set-mark 2
iptables -t mangle -I POSTROUTING -s 10.0.20.37 -j MARK --set-mark 2

iptables -I FORWARD -s 10.0.20.36 -j ACCEPT
iptables -t nat -I PREROUTING -s 10.0.20.36 -j ACCEPT
iptables -t mangle -I PREROUTING -s 10.0.20.36 -j MARK --set-mark 2
iptables -t mangle -I  OUTPUT  -s 10.0.20.36 -j MARK --set-mark 2
iptables -t mangle -I FORWARD -s 10.0.20.36 -j MARK --set-mark 2
iptables -t mangle -I POSTROUTING -s 10.0.20.36 -j MARK --set-mark 2

####ACESSO TEMPORARIO INTERNO - INTERSOLUTION
iptables -I FORWARD -s 10.0.20.150 -j ACCEPT
iptables -t nat -I PREROUTING -s 10.0.20.150 -j ACCEPT
iptables -t mangle -I PREROUTING -s 10.0.20.150 -j MARK --set-mark 2
iptables -t mangle -I  OUTPUT  -s 10.0.20.150 -j MARK --set-mark 2
iptables -t mangle -I FORWARD -s 10.0.20.150 -j MARK --set-mark 2
iptables -t mangle -I POSTROUTING -s 10.0.20.150 -j MARK --set-mark 2



# ACESSO FTP ACSC ftpsenior.acsc.org.br
iptables -I FORWARD -d 200.160.124.70 -j ACCEPT

# kpi.acsc.org.br
iptables -I FORWARD -d 200.160.124.68 -j ACCEPT


iptables -I FORWARD -d 200.160.124.66 -j ACCEPT
iptables -t nat -I PREROUTING -d 200.160.124.66 -j ACCEPT
iptables -t mangle -I PREROUTING -d 200.160.124.66 -j MARK --set-mark 2
iptables -t mangle -I  OUTPUT  -d 200.160.124.66 -j MARK --set-mark 2
iptables -t mangle -I FORWARD -d 200.160.124.66 -j MARK --set-mark 2
iptables -t mangle -I POSTROUTING -d 200.160.124.66  -j MARK --set-mark 2

# VOIP BLOQUEIO SIP
iptables -I OUTPUT -p tcp --dport 5060 -j DROP
iptables -I INPUT -p tcp --dport 5060 -j DROP

iptables -I FORWARD -p tcp --dport 5060 -j DROP

iptables -t nat -I PREROUTING -p tcp --dport 5060 -j DROP
iptables -t nat -I POSTROUTING -p tcp --dport 5060 -j DROP

iptables -t mangle -I OUTPUT -p tcp --dport 5060 -j DROP
iptables -t mangle -I INPUT -p tcp --dport 5060 -j DROP

iptables -I OUTPUT -p tcp --sport 5060 -j DROP
iptables -I INPUT -p tcp --sport 5060 -j DROP

iptables -I FORWARD -p tcp --sport 5060 -j DROP

iptables -t nat -I PREROUTING -p tcp --sport 5060 -j DROP
iptables -t nat -I POSTROUTING -p tcp --sport 5060 -j DROP

iptables -t mangle -I OUTPUT -p tcp --sport 5060 -j DROP
iptables -t mangle -I INPUT -p tcp --sport 5060 -j DROP

iptables -I OUTPUT -p udp --dport 5060 -j DROP
iptables -I INPUT -p udp --dport 5060 -j DROP

iptables -I FORWARD -p udp --dport 5060 -j DROP

iptables -t nat -I PREROUTING -p udp --dport 5060 -j DROP
iptables -t nat -I POSTROUTING -p udp --dport 5060 -j DROP

iptables -t mangle -I OUTPUT -p udp --dport 5060 -j DROP
iptables -t mangle -I INPUT -p udp --dport 5060 -j DROP

iptables -I OUTPUT -p udp --sport 5060 -j DROP
iptables -I INPUT -p udp --sport 5060 -j DROP

iptables -I FORWARD -p udp --sport 5060 -j DROP

iptables -t nat -I PREROUTING -p udp --sport 5060 -j DROP
iptables -t nat -I POSTROUTING -p udp --sport 5060 -j DROP

iptables -t mangle -I OUTPUT -p udp --sport 5060 -j DROP
iptables -t mangle -I INPUT -p udp --sport 5060 -j DROP

iptables -I OUTPUT -p tcp --dport 5061 -j DROP
iptables -I INPUT -p tcp --dport 5061 -j DROP

iptables -I FORWARD -p tcp --dport 5061 -j DROP

iptables -t nat -I PREROUTING -p tcp --dport 5061 -j DROP
iptables -t nat -I POSTROUTING -p tcp --dport 5061 -j DROP

iptables -t mangle -I OUTPUT -p tcp --dport 5061 -j DROP
iptables -t mangle -I INPUT -p tcp --dport 5061 -j DROP

iptables -I OUTPUT -p tcp --sport 5061 -j DROP
iptables -I INPUT -p tcp --sport 5061 -j DROP

iptables -I FORWARD -p tcp --sport 5061 -j DROP

iptables -t nat -I PREROUTING -p tcp --sport 5061 -j DROP
iptables -t nat -I POSTROUTING -p tcp --sport 5061 -j DROP

iptables -t mangle -I OUTPUT -p tcp --sport 5061 -j DROP
iptables -t mangle -I INPUT -p tcp --sport 5061 -j DROP

iptables -I OUTPUT -p udp --dport 5061 -j DROP
iptables -I INPUT -p udp --dport 5061 -j DROP

iptables -I FORWARD -p udp --dport 5061 -j DROP

iptables -t nat -I PREROUTING -p udp --dport 5061 -j DROP
iptables -t nat -I POSTROUTING -p udp --dport 5061 -j DROP

iptables -t mangle -I OUTPUT -p udp --dport 5061 -j DROP
iptables -t mangle -I INPUT -p udp --dport 5061 -j DROP

iptables -I OUTPUT -p udp --sport 5061 -j DROP
iptables -I INPUT -p udp --sport 5061 -j DROP

iptables -I FORWARD -p udp --sport 5061 -j DROP

iptables -t nat -I PREROUTING -p udp --sport 5061 -j DROP
iptables -t nat -I POSTROUTING -p udp --sport 5061 -j DROP

iptables -t mangle -I OUTPUT -p udp --sport 5061 -j DROP
iptables -t mangle -I INPUT -p udp --sport 5061 -j DROP
# VOIP BLOQUEIO SIP


### Padrã$fw -t nat -A POSTROUTING -o $eth_out  -j SNAT --to $ip_out

#PROXY TRANSPARENTE PARA  WIRELESS
#ROTEADOR ANEXO_SUPERIOR 10.0.20.36
iptables -t nat -I PREROUTING -s 10.0.20.36 -p tcp --dport 80 -j REDIRECT --to-port 3128
iptables -t nat -I PREROUTING -s 10.0.20.36 -p tcp --dport 80 -j DNAT --to 10.0.20.2:3128
#ROTEADOR INTERNET 10.0.20.37
iptables -t nat -I PREROUTING -s 10.0.20.37 -p tcp --dport 80 -j REDIRECT --to-port 3128
iptables -t nat -I PREROUTING -s 10.0.20.37 -p tcp --dport 80 -j DNAT --to 10.0.20.2:3128

$fw -A F_GERAL -m state --state ESTABLISHED,RELATED -j ACCEPT
$fw -I FORWARD -m state --state ESTABLISHED,RELATED -j ACCEPT

### CAT 4
iptables -I FORWARD -p tcp -d  200.15remoteShellVulner2.148 -j ACCEPT
iptables -I FORWARD -p tcp -s  200.15remoteShellVulner2.148 -j ACCEPT
iptables -I OUTPUT -p tcp -d  200.15remoteShellVulner2.148 -j ACCEPT
iptables -I  INPUT -p tcp  -s  200.15remoteShellVulner2.148 -j ACCEPT
iptables -t nat -I POSTROUTING  -d 200.15remoteShellVulner2.148 -j MASQUERADE
iptables -t nat -I POSTROUTING  -s 200.15remoteShellVulner2.148 -j MASQUERADE




#####bloqueio facebook https#####
#iptables -I FORWARD -m string --algo bm --string "facebook.com" -j DROP
#iptables -I OUTPUT -m string --algo bm --string "facebook.com" -j DROP
#iptables -t mangle -I PREROUTING -s 10.0.0.0/16 -p tcp -m string --algo kmp --string "facebook.com" -j DROP
#iptables -t nat -I PREROUTING -s 10.0.0.0/16 -p tcp -m string --algo kmp --string "facebook.com" -j DROP
#iptables -t nat -I PREROUTING -p udp --destination-port 53 -m string --algo kmp --string="facebook" -j DROP
#iptables -I FORWARD -p tcp -d 173.252.110.27 -j DROP
#iptables -I OUTPUT -p tcp -d 173.252.110.27 -j DROP

#iptables -t mangle -I FORWARD -s 0.0.0.0/0.0.0.0 -p tcp --dport 443 -m string --algo bm --string "facebook.com" -j DROP
#iptables -t mangle -I FORWARD -s 0.0.0.0/0.0.0.0 -p tcp --sport 443 -m string --algo bm --string "facebook.com" -j DROP



$fw -t nat -A POSTROUTING -o $eth_out  -j SNAT --to $ip_out
$fw -t nat -A POSTROUTING -o $eth_out_2  -j SNAT --to $ip_out_2
