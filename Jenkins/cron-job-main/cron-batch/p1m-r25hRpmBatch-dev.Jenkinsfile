pipeline {

    agent {
        node { label 'maven' }
    }


    options {
        gitLabConnection('Gitlab')
    }

    parameters {
        extendedChoice bindings: '', description: '', groovyClasspath: '', groovyScript: '''import groovy.json.JsonSlurper
        
        
        def Map getJson(String url){
        
          try {
        
            println "\\nURL : " + url + "\\n"
        
            def object = new JsonSlurper().parseText(
                url.toURL().getText(requestProperties: [ \'Accept\': \'application/json\' , \'User-Agent\': \'groovy-2.4.4\' ])
            )
        
            assert object instanceof Map
            assert object.items instanceof List
        
            return object
        
          } catch (Exception e) {
        
            return ["[getJson] :: Ocorreu uma falha ao carregar lista de artefatos do link $url " ]
        
          }
        
        }
        
        
        def List getLista(o){
        
          println "\\nProcurando link do download do binario\\n"
        
          List<String> lista = new ArrayList<>()
        
          for ( Object i :  o.items  ) {
        
             assert i.assets instanceof List
           
             for( Object u :  i.assets ) {
        
               url = "" + u.downloadUrl
        
               if ( url.endsWith("${i.version}.jar") 
                    &&  "${i.version}" ==~ /^[0-9]+\\.[0-9]+\\.[0-9]+.*$/  
                    && !i.version.toUpperCase().contains("SNAPSHOT") 
                ) { 
        
                  println "  > ${i.version} | ${u.downloadUrl}" 
        
                  lista.add("${i.version}")
        
                  break
               }
             }
          }
        
          return lista 
        }
        
        
        List<String> artefatos = new ArrayList<>()
        artefatos.add("Desabilitado")
        
        def url_repo = "http://nexus.macna.local/service/rest/v1/search?" +
             "repository=pbm-mvn" +
             "&maven.groupId=br.com.macna.pbm.reposicaomedicamentos" +
             "&maven.artifactId=reposicao-medicamento-batch" +
             "&maven.extension=jar"
        
        def url_next = url_repo + "&continuationToken="
        
        
        List<String> listaAll = new ArrayList<>()
        
        try {
        
          def object = getJson(url_repo)
          
          listaAll.addAll(getLista(object))
        
          while (object.continuationToken != null) {
        
            object = getJson(url_next + object.continuationToken)
            listaAll.addAll(getLista(object))
        
          }
        
          listaAll = listaAll.sort(){a,b -> a <=> b}.reverse()
        
          artefatos.addAll(listaAll)  
        
          return artefatos 
        
        } catch (Exception e) {
        
           return ["[main] :: Ocorreu uma falha ao carregar lista do link $url_repo :: " + e ]
        }''', multiSelectDelimiter: ',', name: 'VERSION', quoteValue: false, saveJSONParameterToFile: false, type: 'PT_SINGLE_SELECT', visibleItemCount: 5
        choice choices: ['dev', 'hml'], description: 'Selecione o ambiente', name: 'ENV'

    }


    environment
    {
        OPENSHIFT_PROJECT_NAME = "p1m-reposicao-medicamentos"
        APPLICATION_NAME='repomedbatch'
        REPO_NEXUS="reposicao-medicamento-batch"
        JKS_PRODUCT = "pbm"
        JKS_PROJECT = "pbm-reposicao-medicamentos"
        ARTIFACT_DOWNLOAD_URL = "http://nexus.macna.local/repository/pbm-mvn/br/com/macna/pbm/reposicaomedicamentos/${REPO_NEXUS}/${VERSION}/${REPO_NEXUS}-${VERSION}.jar"
        
        CRONJOB_YAML = "yaml/cronjob-batch-DEV.yml"
        CRONJOB_NAME = "Post-GerarPedido"
        CONFIGMAP_CRONJOB_SCRIPT_SH = "configmap/gerarpedido-DEV.sh"
        CONFIGMAP_NAME="cronjob-gerarpedido"
        
        CONFIGMAP_DISTRIBUIDORAS = "configmap/distribuidoras-DEV.txt"
        CONFIGMAP_DISTRIB_NAME="distribuidoras"
    }

    stages {


        stage('Download Artfacts') {
            steps {
                script {
                    sh 'mkdir -p ocp/deployments'
                    sh 'curl -o "ocp/deployments/${APPLICATION_NAME}-${VERSION}.jar" "${ARTIFACT_DOWNLOAD_URL}"'
                }
            }
        }

        stage('[CD] - Create Base Image ') 
        {
            when {
                expression {
                    openshift.withCluster() {
                        openshift.withProject("${OPENSHIFT_PROJECT_NAME}-${ENV}") {
                            echo "Checkinf if imagestream ${APPLICATION_NAME} exist on project ${openshift.project()}"                            
                            return !openshift.selector("bc", "${APPLICATION_NAME}").exists();
                        }
                    }
                }
            }
            steps {
                script {
                    openshift.withCluster() {                        
                        openshift.withProject("${OPENSHIFT_PROJECT_NAME}-${ENV}") {
                            echo "Creating new image"
                            openshift.newBuild("--name=${APPLICATION_NAME}", "--image-stream=cicd-tools/openjdk18-openshift", "--binary=true")
                        }
                    }
                }
            }
        }
        stage('[CD] - StartBuild ') {
            steps {
                script {
                    openshift.withCluster() {
                        openshift.verbose()
                        openshift.logLevel(3)
                        echo "Starting Build: ${APPLICATION_NAME}"
                        openshift.withProject("${OPENSHIFT_PROJECT_NAME}-${ENV}") {
                            openshift.selector("bc", "${APPLICATION_NAME}").startBuild("--from-dir=./ocp", "--wait=true")
                        }
                        openshift.verbose(false)
                    }
                }
            }
        }
        stage('[CD] - Tag to latest') {
            steps {
                script {
                    openshift.withCluster() {
                        openshift.verbose()
                        openshift.logLevel(3)
                        openshift.withProject("${OPENSHIFT_PROJECT_NAME}-${ENV}") {
                            echo "Create TAG : ${APPLICATION_NAME}:${VERSION}"
                            openshift.tag("${APPLICATION_NAME}:latest", "${APPLICATION_NAME}:${VERSION}")
                        }
                        openshift.verbose(false)
                    }
                }
            }
        }
        stage('[CD] - Deploy Configmap - CronJob') {
            steps {
                script {
                    openshift.withCluster() {
                        openshift.withProject("${OPENSHIFT_PROJECT_NAME}-${ENV}") {
                        
                            //CONFIGMAP DO SCRIPT EXECUTADO PELO CRONJOB
                            echo "Configmap: ${CONFIGMAP_NAME} - ${CONFIGMAP_CRONJOB_SCRIPT_SH}"
                            if (openshift.selector("configmap", "${CONFIGMAP_NAME}").exists()) {
                                openshift.delete("configmap", "${CONFIGMAP_NAME}")
                            }
                            def cm1 = openshift.create("configmap", "${CONFIGMAP_NAME}", "--from-file=gerarpedido.sh=${CONFIGMAP_CRONJOB_SCRIPT_SH}")
                            
                            
                            //CONFIGMAP DA LISTA DE DISTRIBUIDORAS PARA DISPARAR O PEDIDO PELO CRON
                            echo "Configmap: ${CONFIGMAP_DISTRIB_NAME} - ${CONFIGMAP_DISTRIBUIDORAS}"
                            if (openshift.selector("configmap", "${CONFIGMAP_DISTRIB_NAME}").exists()) {
                                openshift.delete("configmap", "${CONFIGMAP_DISTRIB_NAME}")
                            }
                            def cm2 = openshift.create("configmap", "${CONFIGMAP_DISTRIB_NAME}", "--from-file=distribuidoras=${CONFIGMAP_DISTRIBUIDORAS}")

                            //CRIANDO O JOB A PARTIR DO YAML
                            if (openshift.selector("cronjob", "${CRONJOB_NAME}").exists()) {
                                openshift.delete("cronjob", "${CRONJOB_NAME}")
                            }
                            echo "Criando o CronJob ${CRONJOB_YAML}"
                            openshift.create("-f ${CRONJOB_YAML}")
                            
                            //CASO NECESSÁRIO PARALIZAR/CANCELAR UM CRONJOB:
                            // $ oc delete cronjob/<cronjob_name>
                            
                            //PARA CRIAR E LISTAR TODOS OS CRONJOBS:
                            // >> $ oc create -f cronjob.yaml
                            // cronjob.batch/post-gerarpedido created
                            // >> $ oc get cronjob
                            // NAME               SCHEDULE    SUSPEND   ACTIVE    LAST SCHEDULE   AGE
                            // post-gerarpedido   0 1 * * *   False     0         <none>          13s
                            
                        }
                    }
                }
            }
        }
        
        stage('[CD] - Deploy App on Dev') {
            steps {
                script {
                    openshift.withCluster() {
                        openshift.verbose()
                        openshift.logLevel(3)
                        openshift.withProject("${OPENSHIFT_PROJECT_NAME}-${ENV}") {
							echo "Create APP : ${APPLICATION_NAME}"
                                if (!openshift.selector('dc', "${APPLICATION_NAME}").exists()) {   
                                    echo 'Creating New App and Service on DEV'
                                        def app = openshift.newApp("${APPLICATION_NAME}:latest",  "--name=${APPLICATION_NAME}")
							    	    app.narrow("svc").expose()
                                    echo "Mounting Volumes on  dc/${APPLICATION_NAME}"
                                        def result1 = openshift.set("volume", "dc/${APPLICATION_NAME}", "--add", "--name=v1-${APPLICATION_NAME}-logs-${ENV}", "-t pvc", "--claim-name=pvc-${APPLICATION_NAME}-logs-${ENV}", "-m /deployments/logs")
                                        //def result1 = openshift.set("volume", "dc/${APPLICATION_NAME}", "--add", "--name=nfs-${APPLICATION_NAME}", "-t pvc", "--claim-name=pvc-${APPLICATION_NAME}-nfs", "-m /mnt/nfs")
                                        echo "Status: ${result1.operation}"
                                        echo "Sub-actions: ${result1.status}"
                                        echo "Operation: ${result1.out}" 
                                /*    echo "Configuring Requests and Limits"
                                        def hpa_limit_request = openshift.set("resources", "dc/${APPLICATION_NAME}", "--limits=cpu=512m,memory=512Mi", "--requests=cpu=256m,memory=256Mi")
                                        echo "Status: ${hpa_limit_request.operation}"
                                        echo "Sub-actions: ${hpa_limit_request.status}"
                                        echo "Operation: ${hpa_limit_request.out}" 
                                    echo "Configmap realms-configmap not found. Let's create!"         a6e-sso-manager/configmap/dev_hml/fature
                                        openshift.create("configmap", "realms-configmap", "--from-file=a6e-sso-manager/configmap/dev_hml/fature", "--from-file=a6e-sso-manager/configmap/dev_hml/autorize")
                                        openshift.set("volume", "dc/${APPLICATION_NAME}", "--add", "--name=v1-realms", "-t configmap", "--configmap-name=realms-configmap", "-m /deployments/realms/") */
                                    echo "Configmap ${APPLICATION_NAME}-configmap not found. Let's create!" 
                                        openshift.create("configmap", "${APPLICATION_NAME}-configmap", "--from-env-file=configmap/${APPLICATION_NAME}-configmap.env")
                                        openshift.set("env", "--from=cm/${APPLICATION_NAME}-configmap", "dc/${APPLICATION_NAME}")
                                /*    echo "configure Readiness Probe"
                                        openshift.set("probe", "dc/${APPLICATION_NAME}", "--readiness", "--get-url=http://:8080/actuator/health", "--initial-delay-seconds=40", "--timeout-seconds=5")
                                    echo "configure Readiness Probe"
                                        openshift.set("probe", "dc/${APPLICATION_NAME}", "--liveness", "--get-url=http://:8080/actuator/health", "--initial-delay-seconds=90", "--timeout-seconds=3") */
                                }
                                else {
                                    echo "Updating ENV configmap: ${APPLICATION_NAME}-configmap"
                                        openshift.delete("configmap", "${APPLICATION_NAME}-configmap")
                                        openshift.create("configmap", "${APPLICATION_NAME}-configmap", "--from-env-file=configmap/${APPLICATION_NAME}-configmap.env")
                                        openshift.set("env", "--from=cm/${APPLICATION_NAME}-configmap", "dc/${APPLICATION_NAME}")                                }
                            def dc = openshift.selector("dc", "${APPLICATION_NAME}")
                             while (dc.object().spec.replicas != dc.object().status.availableReplicas) 
                            {
                                echo "Waiting for openshift to provision your application."
                                sleep 5
                            }

                        openshift.verbose(false)
                        }
                    }
                }
            }
        }
        stage('[CD] - Reports-KPI')
        {
            steps
            {
                script
                {
                    wrap([$class: 'BuildUser']) {
                      echo "Stage: ${STAGE_NAME}"
                      def result = "${currentBuild.currentResult}"
                      dt_dev_hml = new Date(currentBuild.startTimeInMillis)
                      startbuild_dev_hml = dt_dev_hml.format("yyyy-MM-dd HH:mm:ss", TimeZone.getTimeZone('America/Sao_Paulo'))
                      def newENV = "${ENV.toUpperCase()}"
                      print newENV
                      ansibleTower(
                        towerServer: 'Ansible_Tower',
                        templateType: 'job',
                        jobTemplate: "TPT-ANS-REPORTS-KPIS-${newENV}",
                        importTowerLogs: true,
                        removeColor: false,
                        verbose: true,
                        extraVars: """---
                        jks_user: "${BUILD_USER}"
                        jks_result: "${currentBuild.currentResult}"
                        jks_build_url: "${BUILD_URL}"
                        jks_job_name: "${JOB_BASE_NAME}"
                        jks_id_task: "${currentBuild.number}"
                        jks_project: "${JKS_PROJECT}"
                        jks_startbuild: "${startbuild_dev_hml}"
                        jks_product: "${JKS_PRODUCT}"
                        """,
                        async: false
                      )
                    }
                }
            }
        }
    }
}
