import numpy as np
import skfuzzy as fuzz
from skfuzzy import control as ctrl

# Definição das variáveis fuzzy
erro = ctrl.Antecedent(np.arange(0, 101, 1), 'erro')
abertura_valvula = ctrl.Consequent(np.arange(0, 101, 1), 'abertura_valvula')

# Funções de pertinência para o erro
erro['zero'] = fuzz.trimf(erro.universe, [0, 0, 25])
erro['positivo'] = fuzz.trimf(erro.universe, [0, 25, 50])
erro['positivo_grande'] = fuzz.trimf(erro.universe, [25, 50, 75])
erro['positivo_muito_grande'] = fuzz.trimf(erro.universe, [50, 100, 100])

# Funções de pertinência para a abertura da válvula
abertura_valvula['fechada'] = fuzz.trimf(abertura_valvula.universe, [0, 0, 50])
abertura_valvula['parcial'] = fuzz.trimf(abertura_valvula.universe, [0, 50, 75])
abertura_valvula['quase_aberta'] = fuzz.trimf(abertura_valvula.universe, [50, 75, 100])
abertura_valvula['aberta'] = fuzz.trimf(abertura_valvula.universe, [75, 100, 100])

# Regras fuzzy
rule1 = ctrl.Rule(erro['zero'], abertura_valvula['fechada'])
rule2 = ctrl.Rule(erro['positivo'], abertura_valvula['parcial'])
rule3 = ctrl.Rule(erro['positivo_grande'], abertura_valvula['quase_aberta'])
rule4 = ctrl.Rule(erro['positivo_muito_grande'], abertura_valvula['aberta'])

# Controle fuzzy
controle = ctrl.ControlSystem([rule1, rule2, rule3, rule4])
simulador = ctrl.ControlSystemSimulation(controle)

# Simulação de exemplo
simulador.input['erro'] = 45  # Exemplo de entrada
simulador.compute()

# Resultado
print(f"Abertura da válvula: {simulador.output['abertura_valvula']:.2f}")