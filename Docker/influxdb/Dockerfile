#FROM alpine:3.12
FROM 112123451111.dkr.ecr.sa-east-1.amazonaws.com/maquiinaedu:alpine-3.12.0

RUN addgroup --system influxdb --gid=1000 && \
    adduser --system -g influxdb --uid=1000 --home=/home/influxdb --shell=/bin/bash influxdb && \
    mkdir -p /home/influxdb && \
    chown -R influxdb:influxdb /home/influxdb

RUN echo 'hosts: files dns' >> /etc/nsswitch.conf
RUN apk add --update --no-cache --virtual .build-deps wget gnupg tar tzdata bash ca-certificates run-parts && \
    update-ca-certificates

ENV ARCH amd64
ENV GOSU_VER 1.12
RUN set -eux; \
	wget -O /usr/local/bin/gosu "https://github.com/tianon/gosu/releases/download/$GOSU_VER/gosu-${ARCH}" && \
	wget -O /usr/local/bin/gosu.asc "https://github.com/tianon/gosu/releases/download/$GOSU_VER/gosu-${ARCH}.asc" && \
	export GNUPGHOME="$(mktemp -d)" && \
	gpg --batch --keyserver hkps://keys.openpgp.org --recv-keys B42F6819007F00F88E364FD4036A9C25BF357DD4 && \
	gpg --batch --verify /usr/local/bin/gosu.asc /usr/local/bin/gosu && \
	gpgconf --kill all && \
	rm -rf "$GNUPGHOME" /usr/local/bin/gosu.asc && \
	chmod +x /usr/local/bin/gosu && \
	gosu --version && \
	gosu nobody true

ENV INFLUXDB_VERSION 2.0.7
ENV ARCH amd64
RUN set -eux && \
    wget --no-verbose https://dl.influxdata.com/influxdb/releases/influxdb2-${INFLUXDB_VERSION}-linux-${ARCH}.tar.gz.asc && \
    wget --no-verbose https://dl.influxdata.com/influxdb/releases/influxdb2-${INFLUXDB_VERSION}-linux-${ARCH}.tar.gz && \
    export GNUPGHOME="$(mktemp -d)" && \
    echo "disable-ipv6" >> $GNUPGHOME/dirmngr.conf && \
    gpg --batch --keyserver keys.openpgp.org --recv-keys 8C2D403D3C3BDB81A4C27C883C3E4B7317FFE40A && \
    gpg --batch --verify influxdb2-${INFLUXDB_VERSION}-linux-${ARCH}.tar.gz.asc influxdb2-${INFLUXDB_VERSION}-linux-${ARCH}.tar.gz && \
    tar xzf influxdb2-${INFLUXDB_VERSION}-linux-${ARCH}.tar.gz && \
    cp influxdb2-${INFLUXDB_VERSION}-linux-${ARCH}/influx* /usr/local/bin/ && \
    # rm -rf "$GNUPGHOME" influxdb2.key influxdb2-${INFLUXDB_VERSION}-linux-${ARCH}* && \
    influxd version && \
    influx version

COPY default-config.yml /etc/defaults/influxdb2/config.yml
COPY entrypoint.sh /entrypoint.sh

RUN mkdir /docker-entrypoint-initdb.d && \
    mkdir -p /var/lib/influxdb2 && \
    chown -R influxdb:influxdb /var/lib/influxdb2 && \
    mkdir -p /etc/influxdb2 && \
    chmod +x /entrypoint.sh && \
    chown -R influxdb:influxdb /etc/influxdb2

VOLUME /var/lib/influxdb2 /etc/influxdb2

ENTRYPOINT ["/entrypoint.sh"]
CMD ["influxd"]

EXPOSE 8086

ENV INFLUX_CONFIGS_PATH /etc/influxdb2/influx-configs
ENV INFLUXD_INIT_PORT 9999
ENV INFLUXD_INIT_PING_ATTEMPTS 600