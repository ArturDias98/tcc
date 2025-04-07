class FuzzyController:
    def __init__(self, fuzzy_service):
        self.fuzzy_service = fuzzy_service

    def get_valve_opening(self, error_value):
        opening = self.fuzzy_service.calculate_valve_opening(error_value)
        return {"valve_opening": opening}