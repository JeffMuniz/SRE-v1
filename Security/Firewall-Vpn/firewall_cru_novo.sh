#!/bin/bash
###########################################################
#Variaveis
echo 1 > /proc/sys/net/ipv4/ip_forward
fw=/sbin/iptables

############################################################
#IP INTERNO
############################################################
ip_in=192.168.201.9
eth_in=eth0
net_in=192.168.201.0/255.255.255.0

############################################################
#IP DMZ
############################################################
ip_dmz=10.0.0.1
eth_dmz=eth1
net_dmz=10.0.0.0/255.255.255.0

############################################################
#IP VPN
############################################################
net_vpn=10.8.0.0/255.255.255.0

############################################################
#IPs
############################################################
haoc=186.215.164.146

############################################################
# Zerando  Tabelas
############################################################
iptables -t filter -F
iptables -t nat -F
iptables -t mangle -F
iptables  -X

###########################################################
# Criando Tables
############################################################
$fw -N F_FTP					#21
$fw -N F_SSH                                    #22
$fw -N F_DNS                                    #53
$fw -N F_HTTP                                   #80
$fw -N F_NTP					#123
$fw -N F_HTTPS                                  #443
$fw -N F_OPENVPN				#443
$fw -N F_SAMBA					#445
$fw -N F_SQUID					#3128
$fw -N F_NFS					#2049
$fw -N F_VNC					#5900
$fw -N F_DLNA					#8200
$fw -N F_AVAST					#8742
$fw -N F_OPENFIRE				#5222 5223
$fw -N F_OPENFIRE_ADM				#9000
$fw -N F_OWNCLOUD				#8443 9443
$fw -N F_TOR
$fw -N F_TORRENT
$fw -N F_ICMP
$fw -N F_MAC
$fw -N F_GERAL                                  #controle entre redes e outros
$fw -N F_LOC_VMWARE
$fw -N F_LOC_TSERVER
$fw -N F_LOC_SSH
$fw -N F_TRACERT

#############################################
# Bloqueio inicial
#############################################
$fw -P INPUT ACCEPT
$fw -P FORWARD ACCEPT
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
$fw -A INPUT -j F_GERAL
$fw -A FORWARD -j F_GERAL

#############################################
# Tabela F_GERAL
#############################################
$fw -I F_GERAL -m state --state ESTABLISHED,RELATED -j ACCEPT
$fw -I FORWARD -m state --state ESTABLISHED,RELATED -j ACCEPT

#############################################
# Redirecionamentos por servico
#############################################
$fw -A F_GERAL -s 10.0.0.9 -j ACCEPT
$fw -A F_GERAL -s 192.168.201.4 -j ACCEPT
$fw -A F_GERAL -m state --state INVALID -j DROP

$fw -A F_GERAL -p tcp                   --dport         21                      -j      F_FTP
$fw -A F_GERAL -p tcp                   --dport         22                      -j      F_SSH
$fw -A F_GERAL -p tcp                   --dport         53                      -j      F_DNS
$fw -A F_GERAL -p udp                   --dport         53                      -j      F_DNS
$fw -A F_GERAL -p tcp			--dport         80			-j      F_HTTP
$fw -A F_GERAL -p tcp                   --dport         123                     -j      F_NTP
$fw -A F_GERAL -p udp			--dport		123			-j	F_NTP
$fw -A F_GERAL -p tcp -m multiport	--dport         443,3000		-j      F_HTTPS
$fw -A F_GERAL -p tcp			--dport         8443			-j      F_OWNCLOUD
$fw -A F_GERAL -p udp			--dport		443			-j	F_OPENVPN
$fw -A F_GERAL -p tcp -m multiport	--dport		139,445			-j	F_SAMBA
$fw -A F_GERAL -p tcp			--dport		2049			-j	F_NFS
$fw -A F_GERAL -p tcp -m multiport	--dport		3128,9850		-j	F_SQUID
$fw -A F_GERAL -p tcp -m multiport	--dport		5900,5901		-j	F_VNC
$fw -A F_GERAL -p tcp			--dport		8200			-j	F_DLNA
$fw -A F_GERAL -p tcp -m multiport	--dport		5222,5223		-j	F_OPENFIRE
$fw -A F_GERAL -p tcp -m multiport	--dport         9090,9091		-j      F_OPENFIRE_ADM
$fw -A F_GERAL -p tcp -m multiport	--dport		9001,9100		-j	F_TOR
$fw -A F_GERAL -p tcp -m multiport	--dport		6881,49164		-j	F_TORRENT
$fw -A F_GERAL -p udp -m multiport	--dport         6881,49164		-j      F_TORRENT
$fw -A F_GERAL -p tcp                   --dport         8742			-j	F_AVAST
$fw -A F_GERAL -p udp			--dport		33434:33524		-j	F_TRACERT
$fw -A F_GERAL -p icmp                                                          -j      F_ICMP
$fw -A F_GERAL -s $net_vpn -j ACCEPT
$fw -A F_GERAL -s 10.0.0.5 -j ACCEPT
$fw -A F_GERAL -d 10.0.0.255 -j ACCEPT
$fw -A F_GERAL -d 192.168.201.31 -j ACCEPT
$fw -A F_GERAL -d 224.0.0.1 -j ACCEPT
$fw -A F_GERAL -d 255.255.255.255 -j ACCEPT
$fw -A F_GERAL -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: GERAL - "
#$fw -A F_GERAL -j LOG --log-prefix "Firewall: GERAL - "
$fw -A F_GERAL -j DROP

#############################################
# Tabela F_VNC
#############################################
$fw -A F_VNC -s $net_dmz -j ACCEPT
$fw -A F_VNC -s $net_vpn -j ACCEPT
$fw -A F_VNC -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: VNC - "
$fw -A F_VNC -j DROP

#############################################
# Tabela F_TORRENT
#############################################
$fw -A F_TORRENT -d 10.0.0.9 -j ACCEPT
$fw -A F_TORRENT -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: TORRENT - "
$fw -A F_TORRENT -j DROP

#############################################
# Tabela F_NFS
#############################################
$fw -A F_NFS -s 192.168.201.4 -d 10.0.0.8 -j ACCEPT
$fw -A F_NFS -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: NFS - "
$fw -A F_NFS -j DROP

#############################################
# Tabela F_FTP
#############################################
$fw -A F_FTP -s $net_dmz -j ACCEPT
$fw -A F_FTP -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: FTP - "
$fw -A F_FTP -j DROP

#############################################
# Tabela F_AVAST
#############################################
$fw -A F_AVAST -s $net_dmz -j ACCEPT
$fw -A F_AVAST -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: AVAST - "
$fw -A F_AVAST -j DROP

#############################################
# Tabela F_SSH
#############################################
$fw -A F_SSH -s $net_dmz -j ACCEPT
$fw -A F_SSH -s $net_in -j ACCEPT
$fw -A F_SSH -s $net_vpn -j ACCEPT
$fw -A F_SSH -d 10.0.0.5 -j ACCEPT
$fw -A F_SSH -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: SSH - "
$fw -A F_SSH -j DROP

#############################################
# Tabela F_DNS
#############################################
$fw -A F_DNS -s 10.0.0.2 -j ACCEPT
$fw -A F_DNS -s 10.0.0.9 -j ACCEPT
$fw -A F_DNS -s $net_dmz -d 10.0.0.2 -j ACCEPT
$fw -A F_DNS -s $net_in -d 10.0.0.2 -j ACCEPT
$fw -A F_DNS -s $net_vpn -j ACCEPT
$fw -A F_DNS -s 10.0.0.5 -j ACCEPT
$fw -A F_DNS -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: DNS - "
$fw -A F_DNS -j DROP

#############################################
# Tabela F_DLNA
#############################################
$fw -A F_DLNA -s $net_dmz -d 192.168.201.2 -j ACCEPT
$fw -A F_DLNA -s $net_in -d 192.168.201.2 -j ACCEPT
$fw -A F_DLNA -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: DLNA - "
$fw -A F_DLNA -j DROP

#############################################
# Tabela F_NTP
#############################################
$fw -A F_NTP -s 192.168.201.7 -j ACCEPT
$fw -A F_NTP -s $net_in -j ACCEPT
$fw -A F_NTP -s $net_dmz -j ACCEPT
$fw -A F_NTP -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: NTP - "
$fw -A F_NTP -j DROP

#############################################
# Tabela F_HTTP
#############################################
$fw -A F_HTTP -s $net_dmz -j ACCEPT
$fw -A F_HTTP -s $net_in -j ACCEPT
$fw -A F_HTTP -s $net_vpn -j ACCEPT
$fw -A F_HTTP -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: HTTP - "
$fw -A F_HTTP -j DROP

#############################################
# Tabela F_HTTPS
#############################################
$fw -A F_HTTPS -s $net_dmz -j ACCEPT
$fw -A F_HTTPS -s $net_in -j ACCEPT
$fw -A F_HTTPS -s $net_vpn -j ACCEPT
$fw -A F_HTTPS -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: HTTPS - "
$fw -A F_HTTPS -j DROP

#############################################
# Tabela F_OWNCLOUD
#############################################
$fw -A F_OWNCLOUD -d 192.168.201.3 -j ACCEPT
$fw -A F_OWNCLOUD -j DROP

#############################################
# Tabela F_SQUID
#############################################
$fw -A F_SQUID -s $net_dmz -j ACCEPT
$fw -A F_SQUID -s $net_in -j ACCEPT
$fw -A F_SQUID -s $net_vpn -j ACCEPT
$fw -A F_SQUID -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: SQUID - "
$fw -A F_SQUID -j DROP

#############################################
# Tabela F_OPENVPN
#############################################
$fw -A F_OPENVPN -d 192.168.201.9 -j ACCEPT
$fw -A F_OPENVPN -j DROP

#############################################
# Tabela F_SAMBA
#############################################
$fw -A F_SAMBA -s $net_dmz -d $net_in -j ACCEPT
$fw -A F_SAMBA -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: SAMBA - "
$fw -A F_SAMBA -j DROP

#############################################
# Tabela F_OPENFIRE
#############################################
$fw -A F_OPENFIRE -d 10.0.0.8 -j ACCEPT
$fw -A F_OPENFIRE -s 10.0.0.8 -j ACCEPT
$fw -A F_OPENFIRE -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: OPENFIRE - "
$fw -A F_OPENFIRE -j DROP

#############################################
# Tabela F_OPENFIRE_ADM
#############################################
$fw -A F_OPENFIRE_ADM -s $net_vpn -j ACCEPT
$fw -A F_OPENFIRE_ADM -s $net_in -j ACCEPT
$fw -A F_OPENFIRE_ADM -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: OPENFIRE_ADM - "
$fw -A F_OPENFIRE_ADM -j DROP

#############################################
# Tabela F_TRACERT
#############################################
$fw -A F_TRACERT -s $net_dmz -j ACCEPT
$fw -A F_TRACERT -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: TRACERT - "
$fw -A F_TRACERT -j DROP

#############################################
# Tabela F_TOR
#############################################
$fw -A F_TOR -m limit --limit 6/m --limit-burst 6 -j LOG --log-prefix "Firewall: TOR - "
$fw -A F_TOR -j DROP

#############################################
# Tabela F_ICMP
#############################################
$fw -A F_ICMP -p icmp --icmp-type echo-request -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type echo-reply -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type destination-unreachable	-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type network-unreachable		-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type host-unreachable		-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type protocol-unreachable		-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type port-unreachable		-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type source-route-failed		-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type network-unknown		-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type host-unknown			-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type network-prohibited		-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type host-prohibited		-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type TOS-network-unreachable	-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type TOS-host-unreachable		-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type communication-prohibited	-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type time-exceeded			-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type ttl-zero-during-transit	-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type ttl-zero-during-reassembly	-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type parameter-problem             -m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type ip-header-bad			-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -p icmp --icmp-type required-option-missing	-m limit --limit 1/s -j ACCEPT
$fw -A F_ICMP -j DROP

#############################################
# Tabela F_MAC
#############################################
$fw -A F_MAC -s $net_in -d 10.0.0.2 -p udp --dport 53 -j ACCEPT
$fw -A F_MAC -j DROP

################################################
#Redirecionamentos Externos
###############################################

################################################
#TORRENT
###############################################
$fw -t nat -A PREROUTING -d 192.168.201.9 -p tcp --dport 6881 -j DNAT --to 10.0.0.9
$fw -t nat -A PREROUTING -d 192.168.201.9 -p udp --dport 6881 -j DNAT --to 10.0.0.9
$fw -t nat -A PREROUTING -d 192.168.201.9 -p tcp --dport 49164 -j DNAT --to 10.0.0.9
$fw -t nat -A PREROUTING -d 192.168.201.9 -p udp --dport 49164 -j DNAT --to 10.0.0.9

################################################
#VPN
###############################################
$fw -t nat -A PREROUTING -d 192.168.201.9 -p udp --dport 443 -j DNAT --to 192.168.201.9

################################################
#WEB
###############################################
$fw -t nat -A PREROUTING -d 192.168.201.9 -p tcp --dport 8443 -j DNAT --to 192.168.201.3:8443

################################################
#OPENFIRE
###############################################
$fw -t nat -A PREROUTING -d 192.168.201.9 -p tcp --dport 5223 -j DNAT --to 10.0.0.8:5223
$fw -t nat -A PREROUTING -d 192.168.201.9 -p tcp --dport 5222 -j DNAT --to 10.0.0.8:5222

$fw -t nat -A POSTROUTING -o eth0 -j MASQUERADE
