FROM alpine

RUN apk update  && apk add --no-cache \
    curl \
    jq

COPY entrypoint.sh /entrypoint.sh

ENTRYPOINT "/entrypoint.sh" $PAT $REPO $ENDPOINT $GROUP
