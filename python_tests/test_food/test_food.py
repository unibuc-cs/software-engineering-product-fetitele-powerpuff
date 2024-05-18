from urllib.parse import quote
import warnings
import pytest
import requests
import json

from urllib3.exceptions import InsecureRequestWarning


admin = {
    "email": "admin1@example.com",
    "password": "Admin1pa55!" 
}

dummy_user = {
    "email": "dummy@example.com",
    "password": "Dummypa55!",
    "confirmPassword": "Dummypa55!"
}

food = {
    "name": "pythonFood",
    "calories": 100,
    "carbohydrates": 14,
    "proteins": 11,
    "fats": 7
}

admin_token, user_token = None, None


class APIClient:
    def __init__(self, base_url):
        self.base_url = base_url

    
    def generate_headers(self, token=None):
        headers = {'Content-Type': 'application/json'}
        if token:
            headers['Authorization'] = f'Bearer {token}'
        return headers
    

    def register(self, user):
        return requests.post(self.base_url + 'Authentication/register-user', data=json.dumps(user), 
                            headers=self.generate_headers(), verify=False)
    

    def login(self, user):
        return requests.post(self.base_url + "Authentication/login", data=json.dumps(user), 
                            headers=self.generate_headers(), verify=False)
    

    def get_food(self, token):
        return requests.get(self.base_url + 'Food', headers=self.generate_headers(token), verify=False)
    

    def get_food_admin(self, token):
        return requests.get(self.base_url + 'Food/for-admin', headers=self.generate_headers(token), verify=False)
    

    def get_food_name(self, token, name):
        return requests.get(self.base_url + 'Food/' + name, headers=self.generate_headers(token), verify=False)
    

    def delete_user_by_email(self, email, token):
        return requests.delete(self.base_url + 'ApplicationUser/dupa-email/' + email, 
                               headers=self.generate_headers(token), verify=False)
    
    

@pytest.fixture(scope="function")
def api_client():
    return APIClient('https://localhost:7094/api/')


def test_register_login(api_client: APIClient):
    with warnings.catch_warnings():
        global admin_token
        global user_token
        warnings.simplefilter("ignore", InsecureRequestWarning)

        login_response = api_client.login(admin)
        assert login_response.status_code == 200
        admin_token = login_response.json()['token']

        register_response = api_client.register(dummy_user)
        assert register_response.status_code == 200

        login_response = api_client.login(dummy_user)
        assert login_response.status_code == 200
        user_token = login_response.json()['token']


def test_get_food(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_food(user_token)
            assert get_response.status_code == 200
    
        if admin_token is not None:
            get_response = api_client.get_food_admin(admin_token)
            assert get_response.status_code == 200

        get_response = api_client.get_food(None)
        assert get_response.status_code == 401


def test_get_food_name(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_food_name(user_token, food["name"])
            assert get_response.status_code == 200
    
            get_response = api_client.get_food_name(user_token, "wrong_name")
            assert get_response.status_code == 404

        get_response = api_client.get_food_name(None, food["name"])
        assert get_response.status_code == 401


def test_delete_user(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        delete_email_response = api_client.delete_user_by_email(dummy_user["email"], admin_token)
        assert delete_email_response.status_code == 200