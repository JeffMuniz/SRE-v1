FROM jenkins/jenkins:lts

# Plugins que a serem instalados durante o build com dependendencias.
ARG PLUGINS='\
        bitbucket \
        cloudbees-bitbucket-branch-source \
        matrix-auth \
        workflow-aggregator \
        aws-credentials \
        ws-cleanup \
        keycloak \
        ansicolor \
        greenballs \
        openJDK-native-plugin \
        sonar \
        slack'

RUN install-plugins.sh $PLUGINS

# Entrypoint mais outros scripts em potencial.
COPY scripts/*.sh /usr/local/bin/

# Jenkins CasC (nao deve ser copiado diretamente para JENKINS_HOME)
COPY jenkins.yaml /usr/share/jenkins/ref/

USER root

RUN apt-get update && apt-get install sudo -y \
    && rm -rf /var/lib/apt/lists/* \	
    && usermod -a -G sudo jenkins \
    && echo "jenkins ALL=(ALL) NOPASSWD:ALL" >> /etc/sudoers

WORKDIR /var/jenkins_home
ENTRYPOINT [ "entrypoint.sh" ]
CMD [ "/sbin/tini", "--", "/usr/local/bin/jenkins.sh" ]
