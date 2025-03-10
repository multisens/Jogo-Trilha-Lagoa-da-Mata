module.exports = {
	apps: [
	  {
		name: 'TrilhaServer',
		script: 'app.js',
		env: {
		  SERVER_PORT: 7760
		}
	  },
	  {
		name: 'ReactFrontend',
		script: 'npm',
		args: 'run start',
		cwd: './frontend/', // Se o diretório do seu projeto React for chamado "frontend"
	  }
	]
  };
  