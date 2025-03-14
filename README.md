#  **Trilha Lagoa da Mata**
<div align='center'>

O jogo Trilha Lagoa da Mata é um jogo em realidade virtual desenvolvido em parceria com a equipe do laboratório de Limnologia da UFRJ, tendo como base o jogo de tabuleiro que traz as características dos diferentes biomas presentes na trilha paraense de mesmo nome.

Neste projeto é utilizado o Unity para compilar o código e gerar o .Apk e para o servidor é utilizado Node JS e React.

</div>

# Como Utilizar

<h2 align='center'> <i>Após baixar o projeto </i></h2>


## Jogo (Unity)

* Abra-o no unity e compile o código para Android
* Após Compilar, basta baixar e instalar o arquivo gerado (.Apk) no óculos de realidade virtual.

## Servidor (Node JS)
#### BackEnd
* Abra a pasta Node_Server e compile o código App.js </br>
_(Para facilitar o monitoramento do servidor foi utilizado o PM2. Recomendamos sua instalação para execução do código (https://pm2.keymetrics.io/)_
* O servidor emitirá logs dizendo que está no Ar. Esta visualização do servidor em operação poderá ser observada através dos logs gerados pelo PM2 ou poderão ser vistas também pelo FrontEnd.

 #### FrontEnd
* Abra a pasta FrontEnd que está dentro da pasta Node_Server
* Baixe as dependencias (Foi utilizado o NPM)
* inicialize o projeto pelo NPM Start


# Funcionalidades do Servidor
## Recebimento de Mensagens
O servidor funciona com WebSocket e Trabalha com o recebimento de mensagens facilitado, logo, além das mensagens que já estão configuradas, a criação de mensagens é facilitada.
</br>
Exemplos de mensagens enviadas pelo cliente podem ser vistas abaixo:

### Connect
  É a primeira mensagem enviada pelo cliente para o servidor a fim de criar a relação de conexão, sendo esta necessária para o recebimento das próximas mensagens.
 
### LogCardAnswer
  É a mensagem que salva as informações da resposta dada pelos jogadores na carta desafio no CSV que será lido pelo FrontEnd. </br> </br>
 <b> A mensagem tem o seguinte formato: "Nome do Jogador, Questão, Resposta dada pelo jogador, Resposta Correta" </br> </br> </b>
A mensagem é enviada da seguinte maneira:
```
new WS_Client.PlayerData { new WS_Client.CardAnswer { name = "Player", answers = "Questão 3, A, B" } }
```

### LogPointsData
  É  a mensagem que salva as informações da pontuação dos jogadores no CSV que será lido pelo FrontEnd. </br> </br>
 <b> A mensagem tem o seguinte formato: "Nome do Jogador, Origem dos pontos, Nome da referencia da origem, Pontuação Obtida" </br> </br> </b>
A mensagem é enviada da seguinte maneira:
```
new WS_Client.PlayerData { name = "Player", points = "Objeto Explorativo, Flor de Carajás, 70" }
```
# Regras do jogo

<h3> <i> Para jogar basta iniciar o servidor e iniciar o jogo no óculos </i> </h3>

O jogo começará na tela de menu, onde o jogador colocará o seu nome e sala onde o jogo ocorrerá ( atualmente a sala está como default, logo, para ativar este recurso deve-se ativar o canvas respectivo e tirar o deafult do código)

Após colocar o nome e a sala o jogo começará.

## O jogo
<div>
O jogo se passa no formato de trilha, onde o jogador deverá jogar o dado e movimentar seu peão seguindo a trilha.

Ao longo da trilha haverão curiosidades e perguntas interativas, onde o jogador poderá responder a mesma e ganhar pontos.

O ambiente virtual conta com objetos explorativos que contém informações escondidas e que poderão dar pontos, então explorar é importante.

As placas podem render dicas e informações relevantes para você prosseguir e ganhar mais pontos.

o Jogo Acabará quando o jogador chegar no último ladrilho, então explore e divirta-se tanto quanto puder!

$${\color{darkorange} Mas \space lembre-se: \space Você \space sempre \space poderá  \space  jogar \space novamente! }$$

</div>
