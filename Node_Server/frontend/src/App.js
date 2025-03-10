import React, { useState, useEffect } from 'react';
import Papa from 'papaparse'; // Importa o PapaParse
import './App.css';

function App() {
  const [data, setData] = useState([]);
  const [acertos, setAcertos] = useState(0); // Estado para armazenar a soma de acertos

  useEffect(() => {
    const csvFilePath = '/output-AnswersData.csv'; // Caminho para o arquivo CSV
  
    Papa.parse(csvFilePath, {
      download: true, // Faz o download do arquivo CSV
      header: true, // Informa que a primeira linha são os nomes das colunas
      skipEmptyLines: true, // Ignora linhas vazias
      complete: (result) => {
        console.log('Dados carregados:', result.data); // Debug
        setData(result.data);
      },
    });
  }, []);

  useEffect(() => {
    if (data.length > 0) {
      contarAcertos();
    }
  }, [data]); // Chama a função sempre que 'data' for atualizado

  // Função para contar os acertos
  const contarAcertos = () => {
    const acertosCount = data.filter(row => row.Resposta === row.RespostaCorreta).length;
    setAcertos(acertosCount);
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>Dados e Soma de Acertos</h1>
        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Pergunta</th>
              <th>Resposta Correta</th>
              <th>Resposta</th>
            </tr>
          </thead>
          <tbody>
            {data.slice(0, 8).map((row, index) => (
              <tr key={index}>
                <td>{row.Name}</td>
                <td>{row.Pergunta}</td>
                <td>{row.RespostaCorreta}</td>
                <td>{row.Resposta}</td>
              </tr>
            ))}
          </tbody>
        </table>

        <div>
          <h2>Total de Acertos: {acertos}</h2>
        </div>
      </header>
    </div>
  );
}

export default App;
