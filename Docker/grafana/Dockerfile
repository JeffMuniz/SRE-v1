ARG BASE_IMAGE=112123451111.dkr.ecr.sa-east-1.amazonaws.com/maquiinaedu:alpine-3.12.0
FROM ${BASE_IMAGE}

#ARG GRAFANA_TGZ="grafana-latest.linux-x64-musl.tar.gz"
ARG GRAFANA_TGZ="grafana-8.0.3.linux-amd64.tar.gz"

# Make sure we have Gnu tar
RUN apk add --no-cache tar wget && \
    # wget https://dl.grafana.com/oss/release/grafana-latest.linux-x64-musl.tar.gz -O /tmp/grafana.tar.gz
    wget https://dl.grafana.com/oss/release/${GRAFANA_TGZ} -O /tmp/grafana.tar.gz

#   COPY ${GRAFANA_TGZ} /tmp/grafana.tar.gz

# Change to tar xfzv to make tar print every file it extracts
RUN mkdir /tmp/grafana && tar xzf /tmp/grafana.tar.gz --strip-components=1 -C /tmp/grafana

FROM ${BASE_IMAGE}

ARG GF_UID="472"
ARG GF_GID="0"

ENV PATH=/usr/share/grafana/bin:/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin \
    GF_PATHS_CONFIG="/etc/grafana/grafana.ini" \
    GF_PATHS_DATA="/var/lib/grafana" \
    GF_PATHS_HOME="/usr/share/grafana" \
    GF_PATHS_LOGS="/var/log/grafana" \
    GF_PATHS_PLUGINS="/var/lib/grafana/plugins" \
    GF_PATHS_PROVISIONING="/etc/grafana/provisioning"

WORKDIR $GF_PATHS_HOME

RUN apk update && \
    apk add --no-cache ca-certificates bash tzdata && \
    apk add --no-cache openssl musl-utils busybox-extras vim curl

# Oracle Support for x86_64 only
RUN if [ `arch` = "x86_64" ]; then \
      apk add --no-cache libaio libnsl && \
      ln -s /usr/lib/libnsl.so.2 /usr/lib/libnsl.so.1 && \
      wget https://github.com/sgerrand/alpine-pkg-glibc/releases/download/remoteShellVulner0-r0/glibc-remoteShellVulner0-r0.apk \
        -O /tmp/glibc-remoteShellVulner0-r0.apk && \
      wget https://github.com/sgerrand/alpine-pkg-glibc/releases/download/remoteShellVulner0-r0/glibc-bin-remoteShellVulner0-r0.apk \
        -O /tmp/glibc-bin-remoteShellVulner0-r0.apk && \
      apk add --allow-untrusted /tmp/glibc-remoteShellVulner0-r0.apk /tmp/glibc-bin-remoteShellVulner0-r0.apk && \
      rm -f /tmp/glibc-remoteShellVulner0-r0.apk && \
      rm -f /tmp/glibc-bin-remoteShellVulner0-r0.apk && \
      rm -f /lib/ld-linux-x86-64.so.2 && \
      rm -f /etc/ld.so.cache; \
    fi

COPY --from=0 /tmp/grafana "$GF_PATHS_HOME"

RUN if [ ! $(getent group "$GF_GID") ]; then \
      addgroup -S -g $GF_GID grafana; \
    fi

RUN export GF_GID_NAME=$(getent group $GF_GID | cut -d':' -f1) && \
    mkdir -p "$GF_PATHS_HOME/.aws" && \
    adduser -S -u $GF_UID -G "$GF_GID_NAME" grafana && \
    mkdir -p "$GF_PATHS_PROVISIONING/datasources" \
             "$GF_PATHS_PROVISIONING/dashboards" \
             "$GF_PATHS_PROVISIONING/notifiers" \
             "$GF_PATHS_PROVISIONING/plugins" \
             "$GF_PATHS_LOGS" \
             "$GF_PATHS_PLUGINS" \
             "$GF_PATHS_DATA" && \
    cp "$GF_PATHS_HOME/conf/sample.ini" "$GF_PATHS_CONFIG" && \
    cp "$GF_PATHS_HOME/conf/ldap.toml" /etc/grafana/ldap.toml && \
    chown -R "grafana:$GF_GID_NAME" "$GF_PATHS_DATA" "$GF_PATHS_HOME/.aws" "$GF_PATHS_LOGS" "$GF_PATHS_PLUGINS" "$GF_PATHS_PROVISIONING" && \
    chmod -R 777 "$GF_PATHS_DATA" "$GF_PATHS_HOME/.aws" "$GF_PATHS_LOGS" "$GF_PATHS_PLUGINS" "$GF_PATHS_PROVISIONING"

EXPOSE 3000

COPY --chown=$GF_UID:$GF_GID ./run.sh /run.sh

RUN chmod +x /run.sh

USER "$GF_UID"
ENTRYPOINT [ "/run.sh" ]
