def setup_fuzzy_model():
    import numpy as np
    import skfuzzy as fuzz
    from skfuzzy import control as ctrl

    # Definição das variáveis fuzzy
    level = ctrl.Antecedent(np.arange(-1.1, 1.1, 0.001), "level")
    rate = ctrl.Antecedent(np.arange(-0.35, 0.35, 0.001), "rate")
    valve = ctrl.Consequent(np.arange(-1, 1, 0.001), "valve")

    # Funções de pertencimento para 'level' (gaussiana)
    level["high"] = fuzz.gaussmf(level.universe, -1, 0.3)
    level["okay"] = fuzz.gaussmf(level.universe, 0, 0.3)
    level["low"] = fuzz.gaussmf(level.universe, 1, 0.3)

    # Funções de pertencimento para 'rate' (gaussiana)
    rate["negative"] = fuzz.gaussmf(rate.universe, -0.1, 0.03)
    rate["none"] = fuzz.gaussmf(rate.universe, 0, 0.03)
    rate["positive"] = fuzz.gaussmf(rate.universe, 0.1, 0.03)

    # Funções de pertencimento para 'valve' (triangular)
    valve["close_fast"] = fuzz.trimf(valve.universe, [-1, -0.9, -0.8])
    valve["close_slow"] = fuzz.trimf(valve.universe, [-0.6, -0.5, -0.4])
    valve["no_change"] = fuzz.trimf(valve.universe, [-0.1, 0, 0.1])
    valve["open_slow"] = fuzz.trimf(valve.universe, [0.2, 0.3, 0.4])
    valve["open_fast"] = fuzz.trimf(valve.universe, [0.8, 0.9, 1])

    # Configurar método de defuzzificação do consequente
    valve.defuzzify_method = "centroid"  # Método padrão no MATLAB

    # Regras fuzzy com métodos customizados
    rule1 = ctrl.Rule(
        level["okay"],
        valve["no_change"],
        and_func=np.multiply,
        or_func=lambda x, y: x + y - x * y,
    )
    rule2 = ctrl.Rule(
        level["low"],
        valve["open_fast"],
        and_func=np.multiply,
        or_func=lambda x, y: x + y - x * y,
    )
    rule3 = ctrl.Rule(
        level["high"],
        valve["close_fast"],
        and_func=np.multiply,
        or_func=lambda x, y: x + y - x * y,
    )
    rule4 = ctrl.Rule(
        level["okay"] & rate["positive"],
        valve["close_slow"],
        and_func=np.multiply,
        or_func=lambda x, y: x + y - x * y,
    )
    rule5 = ctrl.Rule(
        level["okay"] & rate["negative"],
        valve["open_slow"],
        and_func=np.multiply,
        or_func=lambda x, y: x + y - x * y,
    )

    # Sistema de controle fuzzy
    valve_ctrl = ctrl.ControlSystem([rule1, rule2, rule3, rule4, rule5])

    # Configurar métodos de inferência para coincidir com MATLAB
    valve_ctrl.and_func = np.multiply  # And method: prod
    valve_ctrl.or_func = lambda x, y: x + y - x * y  # Or method: probor
    valve_ctrl.implication_func = np.multiply  # Implication: prod
    valve_ctrl.aggregate_func = np.maximum  # Aggregation: max (padrão MATLAB)

    valve_sim = ctrl.ControlSystemSimulation(valve_ctrl)

    return type("FuzzyModel", (object,), {"simulator": valve_sim})

    # # Exemplo de simulação
    # def simulate_fuzzy(level_input, rate_input):
    #     valve_sim.input['level'] = level_input
    #     valve_sim.input['rate'] = rate_input
    #     valve_sim.compute()
    #     return valve_sim.output['valve']
