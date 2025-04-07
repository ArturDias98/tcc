from flask import Flask

def create_app():
    app = Flask(__name__)
    
    # Here you can set up any necessary configurations for the app
    # e.g., app.config['DEBUG'] = True

    # Import and register routes
    from .routes.fuzzy_routes import register_routes
    register_routes(app)

    return app