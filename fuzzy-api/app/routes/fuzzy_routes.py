from flask import Blueprint, request, jsonify
from app.controllers.fuzzy_controller import FuzzyController
from app.services.fuzzy_service import FuzzyService
from app.models.fuzzy_model import setup_fuzzy_model
import time
from app.models.metrics_model import compute_metrics
import numpy as np

fuzzy_routes = Blueprint("fuzzy_routes", __name__)
fuzzy_service = FuzzyService(setup_fuzzy_model())
controller = FuzzyController(fuzzy_service)


@fuzzy_routes.route("/valve-opening", methods=["POST"])
def get_valve_opening():
    data = request.get_json()
    level_input = data.get("level")
    rate_input = data.get("rate")

    valve_opening = controller.get_valve_opening(level_input, rate_input)
    return jsonify(valve_opening)


@fuzzy_routes.route("/performance-metrics", methods=["POST"])
def calculate_performance_metrics():
    start_time = time.time()

    try:
        data = request.get_json()

        # Validação dos dados de entrada
        if not data:
            return jsonify({"error": "JSON body is required"}), 400

        ref = data.get("ref")
        tol = data.get("tol")
        y = data.get("y")
        t = data.get("t")

        # Validação dos campos obrigatórios
        if ref is None or tol is None or y is None or t is None:
            return jsonify({"error": "Fields ref, tol, y, and t are required"}), 400

        # Validação dos tipos
        if not isinstance(ref, (int, float)) or not isinstance(tol, (int, float)):
            return jsonify({"error": "ref and tol must be numbers"}), 400

        if not isinstance(y, list) or not isinstance(t, list):
            return jsonify({"error": "y and t must be lists"}), 400

        # Validação do tamanho das listas
        if len(y) != len(t):
            return jsonify({"error": "y and t must have the same length"}), 400

        if len(y) == 0:
            return jsonify({"error": "y and t cannot be empty"}), 400

        # Validação dos elementos das listas
        if not all(isinstance(val, (int, float)) for val in y):
            return jsonify({"error": "All elements in y must be numbers"}), 400

        if not all(isinstance(val, (int, float)) for val in t):
            return jsonify({"error": "All elements in t must be numbers"}), 400

        # Conversão para numpy arrays
        y_array = np.array(y)
        t_array = np.array(t)

        # Cálculo das métricas
        mse, overshoot, settling_time = compute_metrics(y_array, t_array, ref, tol)

        # Preparação da resposta
        response = {
            "mse": float(mse) if not np.isnan(mse) else None,
            "overshoot": float(overshoot) if not np.isnan(overshoot) else None,
            "settling_time": (
                float(settling_time) if not np.isnan(settling_time) else None
            ),
        }

        end_time = time.time()
        elapsed_time = end_time - start_time
        print(
            f"Tempo gasto para executar o endpoint '/performance-metrics': {elapsed_time:.4f} segundos"
        )

        return jsonify(response), 200

    except Exception as e:
        return jsonify({"error": f"Internal server error: {str(e)}"}), 500


@fuzzy_routes.route("/health", methods=["GET"])
def health_check():
    return jsonify({"status": "healthy"}), 200
