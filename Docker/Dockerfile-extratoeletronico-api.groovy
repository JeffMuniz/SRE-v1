FROM adoptopenjdk/openjdk8-openj9:ubi

ENV USER=appuser
ENV UID=16000
ENV GID=16001

RUN yum update -y
RUN groupadd -g $GID $USER && \
    adduser $USER -u $UID -g $GID 

RUN chown -R appuser:appuser /opt

COPY --chown=appuser:appuser build/libs/*.jar /opt/mac/
USER appuser

COPY build/libs/*.jar /opt/mac/
EXPOSE 8080
CMD java -XX:+UseG1GC -Xms64m -Xmx256m -jar /opt/mac/*.jar
WORKDIR /opt/mac/