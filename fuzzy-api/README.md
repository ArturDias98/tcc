# Fuzzy API

This project implements a fuzzy logic system to determine the opening of a valve based on an error value received via an API. The system is structured using Flask and follows a modular approach for better organization and maintainability.

## Project Structure

```
fuzzy-api
├── app
│   ├── __init__.py
│   ├── controllers
│   │   └── fuzzy_controller.py
│   ├── services
│   │   └── fuzzy_service.py
│   ├── models
│   │   └── fuzzy_model.py
│   └── routes
│       └── fuzzy_routes.py
├── main.py
├── requirements.txt
└── README.md
```

## Setup Instructions

1. **Clone the repository:**
   ```bash
   git clone <repository-url>
   cd fuzzy-api
   ```

2. **Create a virtual environment:**
   ```bash
   python -m venv venv
   source venv/bin/activate  # On Windows use `venv\Scripts\activate`
   ```

3. **Install the required packages:**
   ```bash
   pip install -r requirements.txt
   ```

4. **Run the application:**
   ```bash
   python main.py
   ```

## Usage

Once the application is running, you can access the API at `http://localhost:5000`. 

To get the valve opening based on an error value, send a POST request to the `/valve-opening` endpoint with a JSON body containing the error value:

```json
{
  "error": 45
}
```

## Publish Api

```shell
docker build -t fuzzy-api . #Cria a imagem
docker run -d -p 40123:5000 --name fuzzy-api-container fuzzy-api #Cria o container
```

The response will include the calculated valve opening based on the fuzzy logic rules defined in the application.

## License

This project is licensed under the MIT License.