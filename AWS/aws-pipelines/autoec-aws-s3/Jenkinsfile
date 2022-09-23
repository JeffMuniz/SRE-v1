def slackNotifier(String buildResult) {
  if ( buildResult == "SUCCESS" ) {
    slackSend ( color: "good", 
    teamDomain: 'macmaceficios', 
    channel: '#team_ec-build',
    tokenCredentialId: 'mac-slack',
    message: "Job: ${env.JOB_NAME} with buildnumber ${env.BUILD_NUMBER} was successful")
  }
  else if( buildResult == "FAILURE" ) { 
    slackSend( color: "danger", 
    teamDomain: 'macmaceficios', 
    channel: '#team_ec-build',
    tokenCredentialId: 'mac-slack',
    message: "Job: ${env.JOB_NAME} with buildnumber ${env.BUILD_NUMBER} was failed")
  }
  else if( buildResult == "UNSTABLE" ) { 
    slackSend( color: "warning", 
      teamDomain: 'macmaceficios', 
      channel: '#team_ec-build',
      tokenCredentialId: 'mac-slack',
      message: "Job: ${env.JOB_NAME} with buildnumber ${env.BUILD_NUMBER} was unstable")
  }
  else if( buildResult == "null" ) { 
    slackSend( color: "warning", 
      teamDomain: 'macmaceficios', 
      channel: '#team_ec-build',
      tokenCredentialId: 'mac-slack',
      message: "Job: ${env.JOB_NAME} with buildnumber ${env.BUILD_NUMBER} finished with unknown status")
  }
}

def startMsg() {
  commit_desc = sh(script: "git log -1 --format='format:%s'", returnStdout: true).trim()
  slackSend(teamDomain: 'macmaceficios', 
    channel: '#team_ec-build',
    tokenCredentialId: 'mac-slack',
    message: "Job ${env.JOB_NAME} <${env.BUILD_URL}|build ${env.BUILD_DISPLAY_NAME}> started by ${env.BUILD_USER}\n====\n${commit_desc}")

}

@NonCPS

def configFromBranch(branch) {
    def env_dev = [
    ]
    def env_qa = [
    ]
    def env_stg = [
    ]
    def env_prd = [
    ]
    if (branch ==~ /(develop)/) { 
        return [
            shouldTest: true,
            shouldDeploy: true,
            env: 'dev',
            tag: 'dev',
            deployments: [env_dev]
        ]
    }    
    if (branch ==~ /(qa)/) { 
        return [
            shouldTest: true,
            shouldDeploy: true,
            env: 'qa',
            tag: 'qa',
            deployments: [env_qa]
        ]
    }
    if (branch ==~ /(staging)/) {
        return [
            shouldTest: true,
            shouldDeploy: true,
            env: 'stg',
            tag: 'stg',
            deployments: [env_stg]
        ]
    }
    else if (branch ==~ /(master)/) {
        return [
            shouldTest: true,
            shouldDeploy: false,
            env: 'prd',
            tag: 'prd',
            deployments: [env_prd]
        ]
    }
    else {
        return [
            shouldTest: false,
            shouldDeploy: false,
            tag: '-',
            deployments: []
        ]
    }
}

pipeline {
    agent none
    
    environment {
        CONFIG = configFromBranch(BRANCH_NAME)
        SHOULD_TEST = "${CONFIG.shouldTest}"
        SHOULD_DEPLOY = "${CONFIG.shouldDeploy}"
        ENV = "${CONFIG.env}"
        TAG = "${CONFIG.tag}"

        PROJECT_NAME="est-frt-autoec"
        GIT_URL="https://maquiinaedu@bitbucket.org/maquiinaedu/${PROJECT_NAME}.git"

    }

    stages {
        stage ('CI') {
            agent {
                label 'TestContainer'
            }
            stages {
                stage('SCM - Checkout') {
                    steps{
                        cleanWs()
                        git branch: BRANCH_NAME, 
                        credentialsId: "bitb-mac", 
                        url: GIT_URL
                        echo 'SCM Checkout'
                    }
                }
                stage ('Test & Publish QA Reports'){
                    when {
                      expression { SHOULD_TEST == 'true' }
                    }
                    steps{
                        echo 'Test & Publish QA Reports'
                    }
                }
            }
        }
        stage ('CD') {
            stages {
                stage ('Deploy') {
                    agent {
                        label 'Build'
                    }
                    stages{
                        stage ('YARN Build'){
                            steps{
                                sh "eval \$(ssh-agent); ssh-add ~/.ssh/id_rsa"
                                sh "yarn install"
                                sh "yarn build:${ENV}"
                            }
                        }
                        stage ('AWS s3 Bucket Deploy Updates') {
                            when {
                              expression { SHOULD_DEPLOY == 'true' }
                            }                  
                            steps {
                                withCredentials([[$class: 'AmazonWebServicesCredentialsBinding', accessKeyVariable: 'AWS_ACCESS_KEY_ID', credentialsId: 'mac-ecr', secretKeyVariable: 'AWS_SECRET_ACCESS_KEY']]) {
                                    sh "aws s3 cp build s3://${PROJECT_NAME}-${ENV}/ --recursive"
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    post {
        success {
            script{
                slackNotifier('SUCCESS')
            }
        }
        failure{
            script{
                slackNotifier('FAILURE')
            }
        }
        unstable{
            script{
                slackNotifier('UNSTABLE')
            }
        }
        aborted{
            script{
                slackNotifier('ABORTED')
            }
        }
    }
}
