FROM rabbitmq:3.8.1-management
ENV default_vhost /
ENV default_user guest
ENV default_password guest
ADD rabbitmq.config /etc/rabbitmq/
ADD definitions.json /etc/rabbitmq/

EXPOSE 5672
EXPOSE 15672
