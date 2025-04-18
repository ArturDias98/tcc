def setup_fuzzy_model():
    import numpy as np
    import skfuzzy as fuzz
    from skfuzzy import control as ctrl

    # Definição das variáveis fuzzy
    erro = ctrl.Antecedent(np.arange(0, 101, 1), 'erro')
    abertura_valvula = ctrl.Consequent(np.arange(0, 101, 1), 'abertura_valvula')

    # Funções de pertinência para o erro
    erro['zero'] = fuzz.trimf(erro.universe, [-3, 0, 0])
    erro['positivo'] = fuzz.trimf(erro.universe, [10, 30, 60])
    erro['positivo_grande'] = fuzz.trimf(erro.universe, [50, 65, 80])
    erro['positivo_muito_grande'] = fuzz.trimf(erro.universe, [70, 100, 100])

    # Funções de pertinência para a abertura da válvula
    abertura_valvula['fechada'] = fuzz.trimf(abertura_valvula.universe, [0, 0, 0])
    abertura_valvula['parcial'] = fuzz.trimf(abertura_valvula.universe, [25, 35, 50])
    abertura_valvula['quase_aberta'] = fuzz.trimf(abertura_valvula.universe, [50, 65, 75])
    abertura_valvula['aberta'] = fuzz.trimf(abertura_valvula.universe, [75, 100, 100])

    # Regras fuzzy
    rule1 = ctrl.Rule(erro['zero'], abertura_valvula['fechada'])
    rule2 = ctrl.Rule(erro['positivo'], abertura_valvula['parcial'])
    rule3 = ctrl.Rule(erro['positivo_grande'], abertura_valvula['quase_aberta'])
    rule4 = ctrl.Rule(erro['positivo_muito_grande'], abertura_valvula['aberta'])

    # Controle fuzzy
    controle = ctrl.ControlSystem([rule1, rule2, rule3, rule4])
    simulator = ctrl.ControlSystemSimulation(controle)
    return type('FuzzyModel', (object,), {'simulator': simulator})