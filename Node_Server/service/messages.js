function Error(msg) {
	return JSON.stringify({
		action: 'error',
		message: msg
	});
}


function NewRoom(rid) {
	return JSON.stringify({
		action: 'new_room',
		room: rid
	});
}


function Welcome(rid, players) {
	return JSON.stringify({
		action: 'welcome',
		room: rid,
		players: players
	});
}


function PlayerConnected(name) {
	return JSON.stringify({
		action: 'connected',
		player: name
	});
}


function StartGame() {
	return JSON.stringify({
		action: 'start_scene',
		scene: 'SampleScene'
	});
}


function StartRound() {
	return JSON.stringify({
		action: 'start_round'
	});
}


module.exports = {
    Error,
	NewRoom,
    Welcome,
	PlayerConnected,
	StartGame,
	StartRound
}