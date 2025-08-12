import numpy as np


def compute_metrics(y, t, ref=1.0, tol=0.02):
    mse = np.mean((y - ref) ** 2)
    overshoot = np.max(y) - ref
    undershoot = ref - np.min(y)
    # Determina o tempo de acomodação (entrando e permanecendo dentro da banda de tolerância)
    within_tol = np.abs(y - ref) <= tol * ref
    settling_time = np.nan
    for i in range(len(t)):
        if np.all(within_tol[i:]):
            settling_time = t[i]
            break
    return mse, overshoot, undershoot, settling_time
