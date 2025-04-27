import os
from flask import Flask
from app.routes.fuzzy_routes import fuzzy_routes

def create_app():
    app = Flask(__name__)
    register_routes(app)
    return app

def register_routes(app):
    app.register_blueprint(fuzzy_routes, url_prefix='/api')

if __name__ == '__main__':
    app = create_app()
    debug_mode = os.getenv('FLASK_DEBUG', 'False').lower() == 'true'
    port = int(os.getenv('FLASK_PORT', 40123))
    app.run(debug=True, port=40123)