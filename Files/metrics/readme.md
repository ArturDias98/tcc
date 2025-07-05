# Métricas de Desempenho de Modelos de Controle

Este projeto contém um script para calcular métricas de desempenho de diferentes modelos de controle (GA-PID, Fuzzy-PID, RNA-PID).

## Como configurar o ambiente

1. **Crie um ambiente virtual (.venv):**

No terminal, execute:
```powershell
python -m venv .venv
```

2. **Ative o ambiente virtual:**

No Windows:
```powershell
.venv\Scripts\activate
```

No Linux/Mac:
```bash
source .venv/bin/activate
```

3. **Instale as dependências:**

```powershell
pip install -r requirements.txt
```

## Executando o script

Certifique-se de que as variáveis `y_ga`, `y_fuzzy`, `y_nn` e `t` estejam definidas antes de rodar o `metics.py`.

```powershell
python metics.py
```

---
**Obs:** As bibliotecas necessárias estão listadas em `requirements.txt`.