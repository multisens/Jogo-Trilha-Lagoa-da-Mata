const fs = require('fs');
const msg = require('./messages');

// Função para gerar o CSV
function generateCSV(ws, message) {
    /*if (!message.data || !Array.isArray(message.data)) {
        ws.send(msg.Error('Incomplete JSON Message. Property "data" (array) expected.'));
        return;
    }
*/
    const data = message.data;

    console.log(data);

  /*  // Validar formato dos dados
    const isValid = data.every(item => item.name && typeof item.points === 'number');
    if (!isValid) {
        ws.send(msg.Error('Invalid data format. Each item must have "name" and "points" (numeric).'));
        return;
    }
*/
    // Gerar conteúdo do CSV
    const csvHeader = 'Name,Tipo,Origem,Pontos\n';
    const csvContent = data.map(item => `${item.name},${item.points}`).join('\n') + '\n';

    // Caminho do arquivo
    const filePath = `./frontend/public/output-PointsData.csv`;

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
