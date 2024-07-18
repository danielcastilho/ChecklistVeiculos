# Checklist de Veiculos
## Code Features
 - ### DevContainers 
   Padroniza o desenvolvimento do projeto, facilitando a configuração do ambiente de desenvolvimento. Evitando diferenças entre os ambientes de desenvolvimento de diferentes desenvolvedores.
 - ### Docker
   Utiliza o Docker para facilitar a execução do projeto em diferentes ambientes.
 - ### Swagger
   Documentação da API utilizando o Swagger. Facilitando o entendimento da API e a execução de testes.
 - ### Clean Architecture e Design Patterns
   Utiliza a Clean Architecture para facilitar a manutenção do código e a adição de novas funcionalidades. Utiliza Design Patterns para facilitar a implementação de funcionalidades comuns como repositórios, serviços e injeção de dependências.

## Como executar o projeto
### Pré-requisitos
 - Docker
 - Docker Compose
 - .NET 8
 - Visual Studio Code
 - Visual Studio Code Remote - Containers
 - Visual Studio Code Remote - Containers

 ### Amboente de Desenvolvimento
    1. Clone o repositório
    2. Abra o projeto no Visual Studio Code
    3. Execute o comando `Remote-Containers: Reopen in Container`
    4. execute o utilitário efbundle para criar o banco de dados
    5. Execute o comando `dotnet run` para executar o projeto
    6. Abra o link sugerido visual studio code para acessar a documentação da API

 ## Todo
    - [ ] Implementar a funcionalidade de negócio com validações
    - [ ] Implementar testes unitários
    - [ ] Implementar testes de integração
    - [ ] Implementar autenticação e autorização usando JWT
    - [ ] Extrair o usuario logado do token JWT para uso na API
    - [ ] Implementar CI/CD
    - [ ] Implementar o Frontend usando Angular