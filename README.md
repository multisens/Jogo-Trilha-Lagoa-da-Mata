#  **Trilha Lagoa da Mata**
<div align='center'>
  <\br>
Neste projeto é utilizado o Unity para compilar o código e gerar o .Apk e Node JS para o servidor.
<\br>  
</div>

# Como Utilizar

<h2 align='center'> <i>Após baixar o projeto </i></h2>


## Jogo (Unity)

* Abra-o no unity e compile o código para Android
* Após Compilar, basta baixar e instalar o arquivo gerado (.Apk) no óculos para poder jogar

## Servidor (Node JS)
#### BackEnd
* Abra a pasta Node_Server e compile o código App.js </br>
_(Para facilitar o controle do servidor foi utilizado e é recomendado o PM2 (https://pm2.keymetrics.io/)_
* O servidor emitirá logs dizendo que está no Ar e poderá ser acessado pelos Logs presentes no PM2 ou poderá ser visto também pelo FrontEnd

 #### FrontEnd
* Abra a pasta FrontEnd que está dentro de Node_Server
* Baixe as dependencias ( Foi utilizado o NPM)
* inicialize o projeto pelo NPM Start


# Funcionalidades do Servidor
## Recebimento de Mensagens
O servidor funciona com WebSocket e Trabalha com o recebimento de mensagens facilitado, logo, além das mensagens que já estão configuradas, a criação de mensagens é facilitado.
</br>
Exemplos de mensagens podem ser vistas abaixo:

### Connect
  É a primeira mensagem enviada pelo cliente para o servidor a fim de criar a relação de conexão e é necessária para o recebimento das próximas mensagens.
  
### LogCardAnswer
  É  a mensagem que salva as informações da resposta dada pelos jogadores na carta desafio no CSV que será lido pelo FrontEnd. </br> </br>
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

