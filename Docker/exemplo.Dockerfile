FROM 112123451111.dkr.ecr.sa-east-1.amazonaws.com/maquiinaedu:alpine-3.12.0

ENV USER=appuser
ENV UID=16000
ENV GID=16001

RUN mkdir /app && \
    addgroup -g $GID $USER && \
    adduser --disabled-password --gecos "" --home /app --ingroup $USER --no-create-home --uid $UID $USER

COPY --chown=appuser: teste.txt /app

USER appuser