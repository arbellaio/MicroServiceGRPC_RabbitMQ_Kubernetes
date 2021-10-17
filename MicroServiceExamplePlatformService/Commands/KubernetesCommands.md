Kubernetes Commands

#check kubernetes version
kubectl version

#Go To K8S folder and run follwing command to apply
kubectl apply -f platforms-depl.yaml

#run following command to get deployment that were created
kubectl get deployments

#run following command to get running pods
kubectl get pods

#To delete kubernetes deployment
kubectl delete deployment platform-depl

#To create node port run following command
kubectl apply -f platforms-np-srv.yaml

#To get services run following command
kubectl get services

#To restart / refresh deployments run following command
kubectl rollout restart deployment platforms-depl

#To setup ingress nginx docker desktop
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.4/deploy/static/provider/cloud/deploy.yaml

#To get all namespaces this project is done in default
kubectl get namespace

#To get pods, deployments, services of namespace
kubectl get pods --namespace=ingress-nginx
kubectl get services --namespace=ingress-nginx

#To apply ingress yaml file
kubectl apply -f ingress-srv.yaml

#To apply localpvc for file storage
kubectl apply -f local-pvc.yaml

#To get pvc
kubectl get pvc

#To generate sqlserver password secret
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"kubectl create secret generic mssql --from-literal=SA_PASSWORD="password"

#To delete secret 
kubectl delete secret generic mssql

#To apply mssql file and generate mssql deployment and service
kubectl apply -f mssql-plat-depl.yaml


#To apply Rabbit MQ 
kubectl apply -f rabbitmq-depl.yaml

