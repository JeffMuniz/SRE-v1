#!/bin/bash
###################################################################################################
# Script de Backup em Fita
# macnaima
# 25/06/2014
###################################################################################################

# Variaveis #--------------------------------------------------------------------------------------
# Atribui o dia atual a variavel dia.
dia=`date +%d-%m-%Y`

# E-mail de report
mail_aviso="ti@cecmi.com.br,cecmi@macnaima.inf.br"


# Numero de erros padrao
#n_erros_padrao="2"
#erro_padrao=`grep -c "/bin/tar: Removing leading " $log_erro`

head="/root/scripts/head.txt"
divisor="/root/scripts/divisor.txt"

# Comandos
cmd_tar="/bin/tar"
cmd_mt="/bin/mt"
#cria_arq="/bin/touch"

# Diretorio backup
bkp_banco="/root/scripts/backup_banco.txt"
bkp_rede="/root/scripts/backup_rede.txt"
tape="/dev/nst0"

# Logs
log_dir="/var/log/backup"
mes=`date +%b`
ano=`date +%Y`
hora=`date +%HH-%MM`
log_dir_data="$log_dir/$ano/$mes"
log="$log_dir_data/backup_${dia}_$hora.log"
log_erro="$log_dir_data/erros_backup_${dia}_$hora.log"
log_rpt="$log_dir_data/rpt_${dia}_$hora.log"
# Variaveis #--------------------------------------------------------------------------------------

# Cria arquivos #----------------------------------------------------------------------------------
mkdir -p $log_dir/$ano/$mes
touch $log
touch $log_erro
touch $log_rpt
# Cria arquivos #----------------------------------------------------------------------------------

# Montar Servidores Windows #----------------------------------------------------------------------
# Montar Servidores Windows #----------------------------------------------------------------------

# Arquivo de report #------------------------------------------------------------------------------
cat $head > $log_rpt
# Arquivo de report #------------------------------------------------------------------------------

echo "+- Data do backup:   $dia" >> $log_rpt
echo "+- Inicio do script de backup:   $(date +%T)" >> $log_rpt

# Diretorios do backup banco #---------------------------------------------------------------------
cat $divisor  >> $log_rpt
echo Diretorios do backup banco >> $log_rpt
while read line
do
   du -sh "$line" >> $log_rpt
done < $bkp_banco
cat $divisor  >> $log_rpt
# Diretorios do backup banco #---------------------------------------------------------------------

# Diretorios do backup rede #----------------------------------------------------------------------
#echo Diretorios do backup rede >> $log_rpt
#while read line
#do
#   du -sh "$line"/* >> $log_rpt
#done < $bkp_rede
#cat $divisor  >> $log_rpt
# Diretorios do backup rede #-----------------------------------------------------------------------

# Inicio reload unidade de fita #-------------------------------------------------------------------
echo "+- Inicio relaod unidade de fita:   $(date +%T)" >> $log_rpt
/sbin/rmmod st >> $log_rpt 2>> $log_erro ##########################################
/sbin/modprobe st >> $log_rpt 2>> $log_erro ##########################################
/bin/sleep 15
echo "+- Fim relaod unidade de fita:   $(date +%T)" >> $log_rpt
cat $divisor  >> $log_rpt
# Fim reload unidade de fita #----------------------------------------------------------------------

# Inicio montar fita #------------------------------------------------------------------------------
#Sabado e domingo nao ejeta a fita
echo "+- Inicio montar fita:   $(date +%T)" >> $log_rpt
$cmd_mt -f $tape eod >> $log_rpt 2>> $log_erro ##########################################
echo "+- Fim montar fita:   $(date +%T)" >> $log_rpt
cat $divisor  >> $log_rpt
# Fim montar fita #---------------------------------------------------------------------------------

# Inicio status fita #------------------------------------------------------------------------------
echo "+- Inicio status fita:   $(date +%T)" >> $log_rpt
$cmd_mt -f $tape status >> $log_rpt 2>> $log_erro ###########################################
echo "+- Fim status fita:   $(date +%T)" >> $log_rpt
cat $divisor  >> $log_rpt
# Fim status fita #---------------------------------------------------------------------------------

# Inicio relatorio banco #-------------------------------------------------------------------------
echo "+- Inicio do backup banco:   $(date +%T)" >> $log_rpt
# Inicio relatorio banco  #------------------------------------------------------------------------

# Comando de Backup banco #------------------------------------------------------------------------
$cmd_tar -cvp -T $bkp_banco -f $tape >> $log 2>> $log_erro ##########################################
# Comando de Backup banco #------------------------------------------------------------------------

# Fecha relatorio banco #--------------------------------------------------------------------------
echo "+- Fim do backup banco:   $(date +%T)" >> $log_rpt
cat $divisor  >> $log_rpt
# Fecha relatorio banco #--------------------------------------------------------------------------


# Inicio relatorio rede #---------------------------------------------------------------------------
#echo "+- Inicio do backup rede:   $(date +%T)" >> $log_rpt
# Inicio relatorio rede #---------------------------------------------------------------------------

# Comando de Backup rede #-------------------------------------------------------------------------
#$cmd_tar -cvp -T $bkp_rede -f $tape >> $log 2>> $log_erro ###########################################
# Comando de Backup rede #-------------------------------------------------------------------------

# Fecha relatorio rede #---------------------------------------------------------------------------
#echo "+- Fim do backup rede:   $(date +%T)" >> $log_rpt
#cat $divisor  >> $log_rpt
# Fecha relatorio rede #---------------------------------------------------------------------------

# Desmontar Servidores Windows #-------------------------------------------------------------------
# Desmontar Servidores Windows #-------------------------------------------------------------------

# Inicio status fita #------------------------------------------------------------------------------
echo "+- Inicio status fita:   $(date +%T)" >> $log_rpt
$cmd_mt -f $tape status >> $log_rpt 2>> $log_erro ###########################################
echo "+- Fim status fita:   $(date +%T)" >> $log_rpt
cat $divisor  >> $log_rpt
# Fim status fita #---------------------------------------------------------------------------------

# Inicio ejetar fita #-----------------------------------------------------------------------------
#Sabado e domingo nao ejeta a fita
#echo "+- Inicio ejetar fita:   $(date +%T)" >> $log_rpt
#$cmd_mt -f $tape eject >> $log 2>> $log_erro #######################################################
#echo "+- Fim  ejetar fita:   $(date +%T)" >> $log_rpt
#cat $divisor  >> $log_rpt
# Fim ejetar fita #---------------------------------------------------------------------------------

# Adiciona Erros no relatorio #--------------------------------------------------------------------
echo "+- Erros (Se houver)"  >> $log_rpt
cat $divisor  >> $log_rpt
echo " ">> $log_rpt
tac $log_erro >> $log_rpt
echo " ">> $log_rpt
cat $divisor  >> $log_rpt
echo "+ Fim dos erros (Se houver)"  >> $log_rpt
cat $divisor  >> $log_rpt
# Adiciona Erros no relatorio #--------------------------------------------------------------------

# Verifica numero de erros #-----------------------------------------------------------------------
# Numero de erros padrao
#n_erros_padrao="1"
#erro_padrao=`grep -c "/bin/tar: Removing leading " $log_erro`
#n_erros=`wc -l  $log_erro | cut -f1 -d' '`

# Se numero de erros (1) diferente de erros padrao, ou erro padrao diferente de STRING_XPTO, adicionar ABRIR TICKET no relatorio
#if [ "$n_erros_padrao" != "$n_erros" ] || [ "$erro_padrao" != "$n_erros_padrao" ]; then
#   sed -i '1i.                                  ' $log_rpt
#   sed -i '1i.   +++++++++++++++++++++++++++++++' $log_rpt
#   sed -i '1i.   ++++++++  ABRIR TICKET ++++++++' $log_rpt
#   sed -i '1i.   +++++++++++++++++++++++++++++++' $log_rpt
#   sed -i '1i.                                  ' $log_rpt
#   sed -i '1i.                                  ' $log_rpt
#fi
# Verifica numero de erros #-----------------------------------------------------------------------

# Envia email de backup #--------------------------------------------------------------------------
echo "+- Enviar e-mail de backup:   $(date +%T)" >> $log_rpt
mail -s "Backup ora_cecmi:: $dia" $mail_aviso < $log_rpt
# Envia email de backup #--------------------------------------------------------------------------

echo "+- Fim do script de backup:   $(date +%T)" >> $log_rpt

### Fim
