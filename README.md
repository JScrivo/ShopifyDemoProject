# ShopifyDemoProject

This project is meant to demonstrate my ability of making a CRUD capable backend API. This project is made in ASP.NET Core and uses a Microsoft SQL server for storage.
For this project I wanted to ability to expand upon it to make it more scalable so it is implemented with Docker and Docker compose.
The database uses an image of TSQL that I modified and uploaded to dockerhub so that it is preconfigured for the project and comes with seed data.


# Compatability
This application was developed and tested on Windows using the Linux Subsystem and Swagger.
Swagger provides an auto-generated frontend which makes it significantly easier to test the API so I recommend testing project in its debugging mode.

# Technologies used
- ASP.Net Core
- Docker
- Docker Compose
- Microsoft SQL Server
- Swagger

# Install and Run
If your system does not have virtualization enabled, do the following:
- Enter your system bios
- On AMD system: Enable VT-x/AMD-V
- On Intel system: Enable Intel(R) Virtualization Technology

If your system does not have the Linux Subsystem install it can be install by the following:
- Run Command Prompt in Administator Mode
- Run the command "wsl --install"
- Restart the system
- Additional Documentation can be found here: https://docs.microsoft.com/en-us/windows/wsl/install

If your system does not have visual studio installed, do the following:
- Download and install visual studio, download can be found here: https://visualstudio.microsoft.com/downloads/
- Select the ASP.NET and Web Developement Workload during installation

If your system does not have Docker Desktop installed, do the following:
- Download and install Docker Desktop, download can be found here: https://www.docker.com/products/docker-desktop
- During installation ensure install additional components for WSL2 is checked

Testing the project:
- Clone the project
- Open the project and double click the solution file
- Wait for Visual Studio to download the nessessary packages
- Set the build target to docker compose
- Run the project
- A browser with swagger with will automatically open and you can test the API
