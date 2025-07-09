import numpy as np

import pandas as pd

from IPython.display import display

# Função para calcular métricas de desempenho


def compute_metrics(y, t, ref=1.0, tol=0.02):
    mse = np.mean((y - ref) ** 2)
    overshoot = np.max(y) - ref
    # Determina o tempo de acomodação (entrando e permanecendo dentro da banda de tolerância)
    within_tol = np.abs(y - ref) <= tol * ref
    settling_time = np.nan
    for i in range(len(t)):
        if np.all(within_tol[i:]):
            settling_time = t[i]
            break
    return mse, overshoot, settling_time


# Supondo que y_pi, y_fuzzy e t já estejam definidos no escopo

metrics = []
# utilize o arquivo data-sample.json para popular t, y_pi e y_fuzzy. Sendo os valores de t vindos de Time e de y_pi e y_fuzzy Value
data = pd.read_json("data-sample.json")
t = data["Time"].values
y_pi = data["Value"].values
y_fuzzy = data["Value"].values

for name, y in [("GA-PID", y_fuzzy), ("Fuzzy-PID", y_fuzzy)]:
    mse, overshoot, settling = compute_metrics(y, t)
    metrics.append(
        {
            "Modelo": name,
            "MSE": mse,
            "Overshoot": overshoot,
            "Tempo de Acomodação (s)": settling,
        }
    )

df_metrics = pd.DataFrame(metrics)
display(df_metrics)
