# HackerNewsScrapper

# Dependencies 
This project depends on the following frameworks :
Automapper:  to do the mapping between entity and view model
Structurmap: an IoC container
* Moq - mocking implementation in unit tests
* Newton soft json - for everything json
* Microsoft.Extensions.Configuration.Json - config provider from Json file
* Microsoft.Extensions.Configuration.Commandline - config provider from argument
* dcsoup - html parsing and filtering
* Polly - for retry policy
* xUnit - unit test framework

# Running
First to build it using docker, execute:
docker build --pull -t hackernews .

To run it :
docker run hackernews -it --posts 20
