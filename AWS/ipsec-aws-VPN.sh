config setup
# include=/etc/ipsec.d/*.conf
 nat_traversal=yes
 protostack=netkey
 force_keepalive=yes
 keep_alive=60
 oe=off
 plutostderrlog=/var/log/ipsec_aws.log
 nhelpers=0

conn MACNA_MONITORAMENTO
 left=10.10.0.212
 leftsubnets=10.10.0.0/16
 leftid=11.84.63.26
 leftsourceip=10.10.0.212
 right=111.66.153.58
 rightsubnets=10.0.20.0/24
 rightid=111.66.153.58
 pfs=no
 forceencaps=yes
 authby=secret
 auto=start


## phase 1 ##
#ike=3des-sha1-modp1024
#keylife=28800s
## phase 2 ##
#esp=3des-sha1
#ikelifetime=2800s

# OUTRA C+OPÇAO
config setup
# include=/etc/ipsec.d/*.conf
 nat_traversal=yes
 protostack=netkey
 force_keepalive=yes
 keep_alive=60
 oe=off
 plutostderrlog=/var/log/ipsec_aws.log
 nhelpers=0


conn AWS2IS
 left=10.10.0.212
 leftsubnets=10.10.0.0/16
 leftid=11.84.63.26
 leftsourceip=10.10.0.212
 right=111.189.206.232
 rightsubnets=192.168.0.0/24
 rightid=111.189.206.232
 pfs=no
 forceencaps=yes
 authby=secret
 auto=start

conn AWS2IS_2
 left=10.10.0.212
 leftsubnets=10.10.0.0/16
 leftid=11.84.63.26
 leftsourceip=10.10.0.212
 right=111.189.206.232
 rightsubnets=192.168.1.0/24
 rightid=111.189.206.232
 pfs=no
 forceencaps=yes
 authby=secret
 auto=start

conn AWS2IS_3
 left=10.10.0.212
 leftsubnets=10.10.0.0/16
 leftid=11.84.63.26
 leftsourceip=10.10.0.212
 right=111.189.206.232
 rightsubnets=192.168.2.0/24
 rightid=111.189.206.232
 pfs=no
 forceencaps=yes
 authby=secret
 auto=start


conn AWS2IS_4
 left=10.10.0.212
 leftsubnets=10.10.0.0/16
 leftid=11.84.63.26
 leftsourceip=10.10.0.212
 right=111.189.206.232
 rightsubnets=192.168.3.0/24
 rightid=111.189.206.232
 pfs=no
 forceencaps=yes
 authby=secret
 auto=start


conn AWS2 MAC IRON-MAN_1
 left=10.10.0.212
 leftsubnets=10.10.0.0/16
 leftid=11.84.63.26
 leftsourceip=10.10.0.212
 right=111.66.153.58
 rightsubnets=10.0.20.0/24
 rightid=111.66.153.58
 pfs=no
 forceencaps=yes
 authby=secret
 auto=start


conn AWS2 MAC IRON-MAN_2
 left=10.10.0.212
 leftsubnets=10.10.1.0/24
 leftid=11.84.63.26
 leftsourceip=10.10.0.212
 right=111.66.153.58
 rightsubnets=10.0.20.0/24
 rightid=111.66.153.58
 pfs=no
 forceencaps=yes
 authby=secret
 auto=start

conn MAC-SIG-IRON-MAN
 left=10.10.0.212
 leftsubnets=10.10.0.0/16
 leftid=11.84.63.26
 leftsourceip=10.10.0.212
 right=111.53.171.34
 rightsubnets=10.19.0.0/24
 rightid=111.53.171.34
 pfs=no
 forceencaps=yes
 authby=secret
 auto=start

conn MAC-SIG-IRON-MAN1
 left=10.10.0.212
 leftsubnets=10.10.1.0/24
 leftid=11.84.63.26
 leftsourceip=10.10.0.212
 right=111.53.171.34
 rightsubnets=10.19.0.0/24
 rightid=111.53.171.34
 pfs=no
 forceencaps=yes
 authby=secret
 auto=start


#conn AWS2HEC
# left=10.10.0.212
# leftsubnets=10.10.2.0/24
# leftid=11.84.63.26
# right=111.53.171.34
# rightsubnets=10.19.0.0/24
# rightid=111.53.171.34
# pfs=no
# forceencaps=yes
# authby=secret
# auto=start

## phase 1 ##
#ike=3des-sha1-modp1024
#keylife=28800s
## phase 2 ##
#esp=3des-sha1
#ikelifetime=2800s
