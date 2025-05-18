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
    app.run(host="0.0.0.0", port=5000)