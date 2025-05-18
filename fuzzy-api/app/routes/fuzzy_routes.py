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
    level_input = data.get('level')
    rate_input = data.get('rate')
    
    valve_opening = controller.get_valve_opening(level_input, rate_input)
    return jsonify(valve_opening)

@fuzzy_routes.route('/health', methods=['GET'])
def health_check():
    return jsonify({'status': 'healthy'}), 200