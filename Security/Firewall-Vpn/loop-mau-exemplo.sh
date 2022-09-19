while [ 1 = 1 ]; do 
  date
  ethtool eth0 | grep -E \(Link\|eth\)
  ethtool eth1 | grep -E \(Link\|eth\)
  traceroute 8.8.8.8 -n
  sleep 1
  ping 10.0.20.1 -c 1 -q |grep packet
  echo ""
  sleep 10
done
