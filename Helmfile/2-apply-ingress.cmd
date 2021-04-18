REM kubectl apply -f ./ingress-files/apm-server-nodeport.yaml
kubectl apply -f ./ingress-files/elasticsearch-nodeport.yaml
kubectl apply -f ./ingress-files/kibana-nodeport.yaml
kubectl apply -f ./ingress-files/rabbitmq-amqp-nodeport.yaml
kubectl apply -f ./ingress-files/rabbitmq-ui-ingress.yaml

set /p DUMMY=Hit ENTER to continue...

