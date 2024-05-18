from urllib.parse import quote
import warnings
import pytest
import requests
import json

from urllib3.exceptions import InsecureRequestWarning


dummy_user = {
    "email": "dummy@example.com",
    "password": "Dummypa55!",
    "confirmPassword": "Dummypa55!"
}

dummy_user2 = {
    "email": "dummy2@example.com",
    "password": "Dummy2pa55!",
    "confirmPassword": "Dummy2pa55!"
}

mismatched_user = {
    "email": "dummy3@example.com",
    "password": "Dummy3pa55!",
    "confirmPassword": "Dummypa55!"
}

admin = {
    "email": "admin1@example.com",
    "password": "Admin1pa55!" 
}

admin_wrong = {
    "email": "admin1@example.com",
    "password": "Admin1pa555!" 
}


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
    

    def get_all_users(self, token):
        return requests.get(self.base_url + 'ApplicationUser', 
                            headers=self.generate_headers(token), verify=False)
    

    def promote(self, email, token):
        encoded_email = quote(email)
        return requests.put(self.base_url + 'Authentication/promote/' + encoded_email, 
                            headers=self.generate_headers(token), verify=False)
    

    def delete_user_by_id(self, id, token):
        return requests.delete(self.base_url + 'ApplicationUser/dupa-id/' + str(id), 
                               headers=self.generate_headers(token), verify=False)
    

    def delete_user_by_email(self, email, token):
        return requests.delete(self.base_url + 'ApplicationUser/dupa-email/' + email, 
                               headers=self.generate_headers(token), verify=False)


@pytest.fixture(scope="function")
def api_client():
    return APIClient('https://localhost:7094/api/')


def test_register(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
        
        # Register users
        register_response = api_client.register(dummy_user)
        assert register_response.status_code == 200

        register_response = api_client.register(dummy_user2)
        assert register_response.status_code == 200


def test_register_same_email(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
        
        register_response = api_client.register(dummy_user)
        assert register_response.status_code == 500


def test_register_different_passwords(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        register_response = api_client.register(mismatched_user)
        assert register_response.status_code == 400


def test_login_admin_wrong(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        # Login admin
        login_response = api_client.login(admin_wrong)
        assert login_response.status_code == 401


def test_admin_workflow(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        # Login admin
        login_response = api_client.login(admin)
        assert login_response.status_code == 200
        token = login_response.json()['token']

        users_response = api_client.get_all_users(token)
        assert users_response.status_code == 200
        users = users_response.json()

        dummy_user_id = (list(filter(lambda u: u["email"] == dummy_user["email"], users))[0])["id"]
        delete_id_response = api_client.delete_user_by_id(dummy_user_id, token)
        assert delete_id_response.status_code == 200

        promote_response = api_client.promote(dummy_user2["email"], token)
        assert promote_response.status_code == 200

        delete_email_response = api_client.delete_user_by_email(dummy_user2["email"], token)
        assert delete_email_response.status_code == 200