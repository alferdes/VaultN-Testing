#!/bin/bash

# Variables
resourceGroup="myResourceGroup"
appName="myWebApp"

# Check deployment status
deploymentStatus=$(az webapp show --name $appName --resource-group $resourceGroup --query state -o tsv)

if [ "$deploymentStatus" == "Running" ]; then
    echo "Deployment successful. Web app is running."
else
    echo "Deployment issue. Web app state: $deploymentStatus"
    exit 1
fi

# Check application health
healthCheckUrl="https://$appName.azurewebsites.net/api/health"
healthStatus=$(curl -s -o /dev/null -w "%{http_code}" $healthCheckUrl)

if [ $healthStatus -eq 200 ]; then
    echo "Application health check passed."
else
    echo "Application health check failed. Status code: $healthStatus"
    exit 1
fi

# Additional checks can be added here (e.g., verifying specific API endpoints, checking logs, etc.)