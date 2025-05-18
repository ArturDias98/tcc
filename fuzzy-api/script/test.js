const axios = require('axios');

async function makeRequest() {
    const url = 'http://localhost:40123/api/valve-opening';
    const data = {
        level: 0.6,
        rate: 0.3
    };

    const startTime = Date.now(); // Marca o início da requisição

    try {
        const response = await axios.post(url, data);
        const endTime = Date.now(); // Marca o término da requisição

        const elapsedTime = endTime - startTime; // Calcula o tempo gasto
        console.log('Resposta da API:', response.data);
        console.log(`Tempo gasto na requisição: ${elapsedTime} ms`);
    } catch (error) {
        console.error('Erro ao fazer a requisição:', error.message);
    }
}

makeRequest();