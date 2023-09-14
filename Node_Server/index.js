const WebSocket = require('ws');
const wss = new WebSocket.Server({ port: 8080 }, () => {
    console.log('Servidor WebSocket iniciado na porta 8080');
});

const rooms = {};

wss.on('connection', (ws) => {
    console.log('Conectou!');

    ws.on('message', (data) => {
        try {
            const message = JSON.parse(data);

            if (!message.action) {
                console.error('Mensagem JSON incompleta. Esperava-se uma propriedade "action".');
                return;
            }

            const action = message.action;

            switch (action) {
                case 'start_scene':
                    handleStartScene(ws, message);
                    break;
                    
                default:
                    console.error('Ação desconhecida:', action);
                    break;
            }
        } catch (error) {
            console.error('Erro ao analisar JSON:', error);
        }
    });
});

function handleStartScene(ws, message) {
    if (!message.name || !message.room) {
        console.error('Mensagem JSON incompleta. Esperava-se "name" e "room".');
        return;
    }

    const room = message.room;
    const playerName = message.name;

    // Verifica se a sala já existe
    if (!rooms[room]) {
        rooms[room] = [];
    }

    // Adicionar jogador à sala
    rooms[room].push(ws);

    console.log('Player ' + playerName + ' entrou na sala: ' + room);

    // Envia uma mensagem de boas-vindas como objeto JSON
    ws.send(JSON.stringify({ action: 'welcome', message: 'Bem-vindo à sala ' + room }));

    ws.on('close', () => {
        console.log('Player desconectado');
        console.log('Player ' + playerName + ' desconectado');

        // Remove o jogador ao desconectar
        rooms[room] = rooms[room].filter(player => player !== ws);
    });

    // Envia a mensagem de cena apenas para o jogador que acabou de entrar
    ws.send(JSON.stringify({ action: 'start_scene', scene: 'SampleScene' }));
}
