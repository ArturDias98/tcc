class FuzzyController:
    def __init__(self, fuzzy_service):
        self.fuzzy_service = fuzzy_service

    def get_valve_opening(self, level_input, rate_input):
        opening = self.fuzzy_service.calculate_valve_opening(level_input, rate_input)
        return {"valve_opening": opening}