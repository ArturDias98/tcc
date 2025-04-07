from flask import Blueprint, request, jsonify
from app.controllers.fuzzy_controller import FuzzyController
from app.services.fuzzy_service import FuzzyService
from app.models.fuzzy_model import setup_fuzzy_model

fuzzy_routes = Blueprint('fuzzy_routes', __name__)
fuzzy_service = FuzzyService(setup_fuzzy_model())
controller = FuzzyController(fuzzy_service)

@fuzzy_routes.route('/valve-opening', methods=['POST'])
def get_valve_opening():
    data = request.get_json()
    error_value = data.get('error')
    
    if error_value is None:
        return jsonify({'error': 'Error value is required'}), 400
    
    valve_opening = controller.get_valve_opening(error_value)
    return jsonify({'valve_opening': valve_opening})