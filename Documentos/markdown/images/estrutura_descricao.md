# Descrição da Arquitetura do Sistema

## Componentes Principais

### 1. Planta de Nível Simulada (Matlab/Simulink)

- **Posição**: Centro-esquerda do diagrama
- **Elemento**: Tanque cilíndrico com sensor de nível
- **Componentes visuais**:
  - Tanque com líquido (azul)
  - Válvula de entrada (superior)
  - Válvula de saída (inferior)
  - Sensor de nível (lateral)
  - Logo Matlab/Simulink

### 2. Servidor OPC UA (Matlab OPC Toolbox)

- **Posição**: Adjacente à planta
- **Elemento**: Caixa retangular representando servidor
- **Componentes visuais**:
  - Ícone de servidor
  - Logo OPC UA
  - Variáveis expostas: Level, Setpoint, Control_Signal

### 3. Cliente OPC UA + IHM (C#)

- **Posição**: Centro-direita do diagrama
- **Elemento**: Interface gráfica com painel de controle
- **Componentes visuais**:
  - Tela de monitoramento com gráficos
  - Botões de controle
  - Indicadores de status
  - Logo C# / .NET

### 4. API REST Fuzzy (Python)

- **Posição**: Parte superior do diagrama
- **Elemento**: Módulo de processamento inteligente
- **Componentes visuais**:
  - Caixa com símbolo de lógica Fuzzy (∞)
  - Logo Python
  - Indicação de REST API
  - Funções de pertinência esquematizadas

## Fluxo de Comunicação

### Setas e Conexões

1. **Planta → Servidor OPC**: Linha contínua (dados de processo)
2. **Servidor OPC → Cliente OPC**: Linha bidirecional OPC UA (leitura/escrita)
3. **Cliente OPC → API Fuzzy**: Linha HTTP REST (requisições)
4. **API Fuzzy → Cliente OPC**: Linha HTTP REST (respostas)
5. **Cliente OPC → Servidor OPC**: Linha de controle (sinais de atuação)
6. **Servidor OPC → Planta**: Linha de atuação (controle da válvula)

### Protocolos de Comunicação

- **OPC UA**: Entre servidor e cliente OPC
- **HTTP/REST**: Entre cliente OPC e API Fuzzy
- **Simulink Interface**: Entre planta e servidor OPC

## Elementos Visuais Adicionais

### Legendas

- Setas vermelhas: Fluxo de dados de processo
- Setas azuis: Fluxo de comandos de controle
- Setas verdes: Comunicação HTTP/REST
- Setas roxas: Comunicação OPC UA

### Dados Trafegados

- **Variáveis de Processo**: Level_Current, Setpoint, Disturbances
- **Sinais de Controle**: Valve_Opening, Control_Signal
- **Status**: System_Status, Alarms
- **Parâmetros Fuzzy**: Membership_Functions, Rules

## Layout Sugerido

```
    [API REST Fuzzy - Python]
           ↕ HTTP/REST
    [Cliente OPC UA + IHM - C#] ←→ OPC UA ←→ [Servidor OPC UA - Matlab]
                                                      ↕
                                              [Planta de Nível - Simulink]
```

## Cores Sugeridas

- **Planta**: Azul claro (água) com cinza (estrutura)
- **Servidor OPC**: Verde escuro
- **Cliente OPC/IHM**: Azul escuro
- **API Fuzzy**: Laranja/Amarelo
- **Setas de comunicação**: Conforme legenda acima

## Texto no Diagrama

- Títulos dos componentes
- Protocolos de comunicação
- Principais variáveis trafegadas
- Indicação de tecnologias (Python, C#, Matlab, OPC UA)
