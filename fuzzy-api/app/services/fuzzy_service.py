class FuzzyService:
    def __init__(self, fuzzy_model):
        self.simulator = fuzzy_model.simulator

    def calculate_valve_opening(self, level_input, rate_input):
        self.simulator.input['level'] = level_input
        self.simulator.input['rate'] = rate_input
        self.simulator.compute()
        return self.simulator.output['valve']