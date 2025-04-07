class FuzzyService:
    def __init__(self, fuzzy_model):
        self.simulator = fuzzy_model.simulator

    def calculate_valve_opening(self, error_value):
        self.simulator.input['erro'] = error_value
        self.simulator.compute()
        return self.simulator.output['abertura_valvula']