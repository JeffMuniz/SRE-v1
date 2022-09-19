##############################################################################
# ARQUIVO SHELL QUE EXECUTA A AUTENTICACAO E CHAMADA DAS FUNCOES DO SERVICE
# DA SOLUCAO DO PBM - REPOSICAO DE MEDICAMENTOS
#
# ESTE SCRIPT EH EXECUTADO PELO CRON DO OPENSHIFT
# http://gitlab.macna.local/infraestrutura/PBM/p1m-reposicao-medicamento-batch/blob/master/yaml/cronjob-batch-DEV.yml
#
# Macnaima Macnamara
# 11/FEV/2020
#
# AMBIENTE: OEPNSHIFT DEV
##############################################################################


# Variáveis Globais
APIMAN_AUTH='http://albacete.macna.local:8080/api/sso/pbm-repomed-auth-appoz/1.0/openid-connect/token'
X_API_Key_AUTH='some-token'


URL_GERAR_PEDIDO='http://albacete.macna.local:8080/api/pbm/repomed-batch/1.0/pedidos/gerar-pedido'
X_API_Key_repomed=some-token


access_token=""

# Dia de início de execução
dia_inicio=$(date -I)

# Array com as chamadas com falhas
declare -a falhas_distribuidoras


function autenticar() {
	# Autenticando no REALM aplicacoes-macna
	jwt=$(curl -s -L -X POST $APIMAN_AUTH \
				-H 'Content-Type: application/x-www-form-urlencoded' \
				-H "X-API-Key: ${X_API_Key_AUTH}" \
				--data-urlencode 'grant_type=client_credentials' \
				--data-urlencode 'client_id=pbm-reposicao-medicamentos' \
				--data-urlencode 'client_secret=some-passwd'
	      ) ;

	echo -n $jwt | jq .access_token | sed 's/"//g'
}


function gerar_pedido() {
	local ID_distribuidora=$1

	local gerar_pedido_http_code=$(curl -s -o /dev/null -w '%{http_code}' -L -X POST ${URL_GERAR_PEDIDO} \
	               -H "X-API-Key: ${X_API_Key_repomed}"       \
	               -H "Content-Type: application/json"        \
	               -H "Authorization: Bearer ${access_token}" \
	               --data-raw '{
					    "idDistribuidor": '${ID_distribuidora}',
					    "operacaoOrigem":{
					        "key":"Automatica",
					        "code":"A"
					    }
					}' ) ;

	if [ "${gerar_pedido_http_code}" = "202" ]; then
		echo "$(date) - Distribuidora ${distribuidora}: Sucesso [${gerar_pedido_http_code}]" >&2
	else
		echo "$(date) - Falha - Distribuidora ${distribuidora} - HTTP_CODE diferente de 202: [${gerar_pedido_http_code}]" >&2
		falhas_distribuidoras+=(${distribuidora})
	fi;
}



# Para as chamadas que retornaram código diferente de 202
function gerar_pedido_falhas() {
	local -a falhas=("${falhas_distribuidoras[@]}")
	unset falhas_distribuidoras

	# repete enquanto nao terminam os códigos com falha até terminar o dia
	while [ "${#falhas[@]}" -gt 0 ] && [ "${dia_inicio}" = "$(date -I)" ]
	do
		echo "" >&2
		echo "$(date) - Gerando Pedidos com Falhas... ${#falhas[@]}" >&2

		for distribuidora in "${falhas[@]}"
		do
			gerar_pedido ${distribuidora}
		done

		falhas=("${falhas_distribuidoras[@]}")
		unset falhas_distribuidoras

		sleep 60
	done
}


access_token=$(autenticar)


if [ -z "${access_token}" ]
then
	echo "$(date) - Falha de autenticação..." >&2
	exit 1
fi


# O Script recebe como parametro no JOB os valores recuperados do ConfigMap de Distribuidoras
for distribuidora in "$@"
do
	gerar_pedido ${distribuidora}
done


gerar_pedido_falhas


exit 0