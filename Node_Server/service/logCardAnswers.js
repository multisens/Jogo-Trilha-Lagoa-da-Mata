const fs = require('fs');
const msg = require('./messages');

// Função para gerar o CSV
function generateCSV(ws, message) {

    const data = message.dataAnswer;

    // Gerar conteúdo do CSV
    const csvHeader = 'Name,Pergunta,RespostaCorreta,Resposta\n';

    const csvContent = data.map(item => `${item.name},${item.answers}`).join('\n') + '\n';

    // Caminho do arquivo
    const filePath = `./frontend/public/output-AnswersData.csv`;

    // Verificar se o arquivo já existe
    if (!fs.existsSync(filePath)) {
        // Criar novo arquivo com o cabeçalho
        fs.writeFile(filePath, csvHeader + csvContent, (err) => {
            if (err) {
                console.error('Failed to write CSV file:', err);
                ws.send(msg.Error('Failed to generate CSV file.'));
            } else {
                console.log(`CSV file created: ${filePath}`);
            }
        });
    } else {
        // Adicionar novos dados ao arquivo existente
        fs.appendFile(filePath, csvContent, (err) => {
            if (err) {
                console.error('Failed to append to CSV file:', err);
                ws.send(msg.Error('Failed to append to CSV file.'));
            } else {
                console.log(`Data appended to CSV file: ${filePath}`);
            }
        });
    }
}

module.exports = { generateCSV };
