import numpy as np
import skfuzzy as fuzz
from skfuzzy import control as ctrl

# Definição das variáveis fuzzy
level = ctrl.Antecedent(np.arange(-1.1, 1.1, 0.001), 'level')
rate = ctrl.Antecedent(np.arange(-0.35, 0.35, 0.001), 'rate')
valve = ctrl.Consequent(np.arange(-1, 1, 0.001), 'valve')

# Funções de pertencimento para 'level' (gaussiana)
level['high'] = fuzz.gaussmf(level.universe, -1, 0.3)
level['okay'] = fuzz.gaussmf(level.universe, 0, 0.3)
level['low'] = fuzz.gaussmf(level.universe, 1, 0.3)

# Funções de pertencimento para 'rate' (gaussiana)
rate['negative'] = fuzz.gaussmf(rate.universe, -0.1, 0.03)
rate['none'] = fuzz.gaussmf(rate.universe, 0, 0.03)
rate['positive'] = fuzz.gaussmf(rate.universe, 0.1, 0.03)

# Funções de pertencimento para 'valve' (triangular)
valve['close_fast'] = fuzz.trimf(valve.universe, [-1, -0.9, -0.8])
valve['close_slow'] = fuzz.trimf(valve.universe, [-0.6, -0.5, -0.4])
valve['no_change'] = fuzz.trimf(valve.universe, [-0.1, 0, 0.1])
valve['open_slow'] = fuzz.trimf(valve.universe, [0.2, 0.3, 0.4])
valve['open_fast'] = fuzz.trimf(valve.universe, [0.8, 0.9, 1])

# Regras fuzzy
rule1 = ctrl.Rule(level['okay'], valve['no_change'])
rule2 = ctrl.Rule(level['low'], valve['open_fast'])
rule3 = ctrl.Rule(level['high'], valve['close_fast'])
rule4 = ctrl.Rule(level['okay'] & rate['positive'], valve['close_slow'])
rule5 = ctrl.Rule(level['okay'] & rate['negative'], valve['open_slow'])

# Sistema de controle fuzzy
valve_ctrl = ctrl.ControlSystem([rule1, rule2, rule3, rule4, rule5])
valve_sim = ctrl.ControlSystemSimulation(valve_ctrl)

# Exemplo de simulação
def simulate_fuzzy(level_input, rate_input):
    valve_sim.input['level'] = level_input
    valve_sim.input['rate'] = rate_input
    valve_sim.compute()
    return valve_sim.output['valve']

# Teste do sistema
if __name__ == "__main__":
    level_input = -0.44  # Exemplo de entrada para 'level'
    rate_input = 0.11  # Exemplo de entrada para 'rate'
    output_valve = simulate_fuzzy(level_input, rate_input)
    print(f"Saída para valve: {output_valve}")