# escape=\
FROM debian:9

# Some comment
RUN apt-get update && apt-get install libunwind8 libicu57

COPY * /scripts

CMD /scripts/run.sh -p1 -p2
