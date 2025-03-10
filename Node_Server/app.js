const WebSocket = require('ws');
const rooms = require('./service/rooms');
const msg = require('./service/messages');
const PointsCSV = require('./service/logPointsData');
const CardAnswerCSV = require('./service/logCardAnswers');
// WebSocket server
const _myport = process.env.SERVER_PORT || 8080; // Porta padrão: 8080
console.log(`Starting WebSocket server on port ${_myport}...`);
const wss = new WebSocket.Server({ port: _myport }, () => {
    console.log('Server started!');
});

wss.on('connection', (ws) => {
    console.log('Client connected.');
    ws.on('message', (data) => handleMessage(ws, data));
    ws.on('close', () => handleDisconnect(ws));
});

function handleMessage(ws, data) {
    try {
        const message = JSON.parse(data);

        if (!message.action) {
            ws.send(msg.Error('Incomplete JSON Message. Property "action" expected.'));
            return;
        }

        const action = message.action;
        console.log(message);

        switch (action) {
            case 'new_room':
                createRoom(ws);
                break;
            case 'connect':
                handleConnection(ws, message);
                break;
            case 'start':
                startGame(ws);
                break;
            case 'round':
                handleRound(ws, message);
                break;
            case 'csvpoints':
                PointsCSV.generateCSV(ws, message);
                break;
            case 'cardanswer':
                CardAnswerCSV.generateCSV(ws, message);
                break;
            default:
                ws.send(msg.Error(`Unknown action: ${action}.`));
                break;
        }
    } catch (error) {
        ws.send(msg.Error('Malformed JSON'));
        console.log('Error:', error);
    }
}

function handleDisconnect(ws) {
    const rid = rooms.getPlayerRoom(ws);
    if (rid != null) {
        rooms.removePlayer(rid, ws);
    }
    console.log('Client disconnected.');
}

function createRoom(ws) {
    const rid = rooms.createRoom();
    ws.send(msg.NewRoom(rid));
}

function handleConnection(ws, message) {
    if (!message.name || !message.room) {
        ws.send(msg.Error('Incomplete JSON Message. Property "name" and "room" expected.'));
        return;
    }

    const playerName = message.name;
    const roomId = message.room;
    const playerList = rooms.getRoomPlayers(roomId);

    rooms.insertPlayer(roomId, ws);
    rooms.savePlayerData(ws, 'name', playerName);
    console.log(`Player ${playerName} joins the room ${roomId}.`);

    ws.send(msg.Welcome(roomId, playerList));
    rooms.notifyRoomPlayersExcept(roomId, ws, msg.PlayerConnected(playerName));
}

function startGame(ws) {
    const rid = rooms.getPlayerRoom(ws);
    rooms.notifyRoomPlayers(rid, msg.StartGame());
    rooms.getRoundPlayer(rid).send(msg.StartRound());
}

function handleRound(ws, message) {
    if (!message.name || !message.points || !message.house) {
        ws.send(msg.Error('Incomplete JSON Message. Properties "name", "points" and "house" expected.'));
        return;
    }

    const rid = rooms.getPlayerRoom(ws);
    rooms.notifyRoomPlayersExcept(rid, ws, JSON.stringify(message));
    rooms.getRoundPlayer(rid).send(msg.StartRound());
}


