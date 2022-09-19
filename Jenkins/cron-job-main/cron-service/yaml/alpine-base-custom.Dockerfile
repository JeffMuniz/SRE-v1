FROM alpine:3.10

RUN apk add --no-cache \
    bash    \
    bash-doc \
    bash-completion \
    util-linux \
    pciutils   \
    usbutils   \
    coreutils  \
    binutils   \
    findutils  \
    grep    \
    curl    \
    jq      \
    less    \
    vim     \
    sed