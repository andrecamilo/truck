
# Truck
A solução foi feita no **Visual Studio 2017**, usando **.NET Core 2.2**

## Comandos do teclado
As teclas númericas utilizadas para escolha dos itens no console deverão ser os números acima das letras e abaixo das teclas de função (F1, F2, ...) do teclado. Pois o código interno de cada tecla é diferente do teclado númerico. 

## Estrutura principal
A composição dos projetos da solução seguem os seguintes padrões:

* **Truck.Presentation.Console**: é um projeto .NET Core 2.2 do tipo Console que é o ponto de entrada da aplicação.
* **Truck.Domain.Register**: é um projeto .NET Core 2.2 do tipo class library que contem o Bouded context de registro da aplicação.
* **Truck.Infra.Database**: projeto específico para a camada de acesso dados.
* **Truck.Infra.Helper**: projeto específico para classes Helper que poderão ser utilizadas nas demais camadas da aplicação.
* **Truck.Tests**: testes unitários do projeto principal.

## Arquitetura da solução
No projeto existem algumas premissas do DDD, como Bouded Contexts e CQS (register), onde as classes de Command são separados das classes de Query

* **Camada da Apresentação**: projeto `Truck.Presentation.Console`, é onde esta localizado o startup da aplicação console.
* **Camada de Dominio**: projeto `Truck.Domain.Register` a camada de serviços cria uma abstração para as operações que podem ser realizadas na aplicação, e foi baseada no conceito de _Command/Query_. Cada ação que opera sobre o negócio é representado por um comando, e cada ação que consulte informações na camada de negócios é representada por uma Query.
* **Infraestrutura - Repositorio de dados**: representada pelo projeto `Truck.Infra.Database`, declara as entidades, elementos de modelo auxiliares e o contexto do EntityFramework para manipulação da camada. Sua implementação não específica o banco de dados usado para persistência, ficando a cargo de quem for instanciar o contexto determinar.
* **Infraestrutura - Helpers**: projeto `Truck.Infra.Helper`, contem interfaces e classes base.
* **Testes**: projeto `Truck.Tests`, contem os testes das classes de domínio da solução

Informações detalhadas sobre os elementos da arquitetura:

### Modelo
É composto de 2 entidades:

* **Truck**, representando o registro dos Caminhãos da frota.
* **ColorOption**, sendo uma entidade de apoio para registrar a cor de um Caminhão, em uma relação de 1:N * 
com a entidade anterior.

### Arquitetura de Command/Query (CQS)
Esse tipo de arquitetura cria uma estrutura bastante simples e eficaz para abstração das operações da camada de negócios/serviços da aplicação. No proojeto principal foram declaradas duas interfaces: `ICommand<TCommand>` e `IQuery<TQuery, TResult>`, representando um comando e uma pesquisa respectivamente.

Eles são baseados em tipos genéricos para descrever seu comportamento, e definem um único método, chamado `ExecuteAsync`.

 Com relação a taxonomia, as abstrações são decalaradas na pasta `Services`, em suas respecrivas subpastas `Commands` e `Queries`, de acordo com o tipo de interação para o qual esta se criando a abstração.

Já as implementações padrões estão contidas na pasta `Components` que também tem as subpastas correspondentes para cada tipo. Portanto:

* `Services` declaram as abstrações que são usadas na camada de aplicação para determinar as dependencias de um módulo.
* `Components` declaram as implementações de cada abstração, e são usadas pela raiz de composição da aplicação na hora de injetar as dependencias.

Vantagens da arquitetura:
* Implementação simples
* Como os Camandos são separados das Queries o encapsulamento das regras de negócio ficam mais claros
* Estrutura padronizada de execução de componentes
* Estrutura padronizada de resposta da execução dos componentes
* Fácil de ser integrada com Injeção de Dependências
* Simples para implementar os testes unitários

### Validações
Para esse projeto inclui um mecanismo simples de validações baseado no uso de uma interface `IValidator` onde cada tipo de validação é uma implementação dessa interface. No projeto foram usadas apenas dois tipos de validação:

* **RequiredValidator**: verifica se uma string tem algum valor digitado.
* **ChassisExistsValidator**: verifica se uma string representando o chassi de um Caminhão existe no repositório.
* **ModelYearValidator**: aplica as regras de ano do modelo.
* **ManufactureYearValidator**: aplica as regras de ano de fabricação.

Na forma como a aplicação foi construída, os _Validators_ são usados diretamente pela camada de aplicação, para assegurar que os dados que o usuário esta digitando no terminal estão de acordo com o necessário para poderem ser repassados à camada de serviço.

## Testes Unitários
O projeto dos testes unitários inclui classes para verificar as interações de todos os comandos e queries da aplicação. 

## Dependências do Projeto
As seguintes dependências foram usadas na elaboração desse projeto:
* **NUnit**: Usado para fazer os testes.
* **Autofac**: usado como container para injeção de dependencias.
* **Entity Framework Core**: usado para criar a camada de persistência de dados.
* **Colorful.Console**: Usado para formatação de cores no Console.