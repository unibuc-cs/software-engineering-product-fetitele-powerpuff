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

profile = {
  "name": "dummy name",
  "birthdate": "2004-05-18",
  "weight": 60,
  "height": 175,
  "goal": 0
}

profile_wrong_date = {
  "name": "dummy name",
  "birthdate": "2104-05-18",
  "weight": 60,
  "height": 175,
  "goal": 0
}

profile_wrong_weight = {
  "name": "dummy name",
  "birthdate": "2004-05-18",
  "weight": -60,
  "height": 175,
  "goal": 0
}

put_profile = {
  "name": "dummy name",
  "birthdate": "2004-05-18",
  "weight": 62,
  "height": 175,
  "goal": 0
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
    

    def delete_user_by_email(self, email, token):
        return requests.delete(self.base_url + 'ApplicationUser/dupa-email/' + email, 
                               headers=self.generate_headers(token), verify=False)
    

    def get_all_profiles(self, token):
        return requests.get(self.base_url + 'Profiles', 
                            headers=self.generate_headers(token), verify=False)
    

    def post_user_profile(self, token, profile):
        return requests.post(self.base_url + 'Profiles', data=json.dumps(profile),
                            headers=self.generate_headers(token), verify=False)
    

    def put_user_profile(self, token, profile):
        return requests.put(self.base_url + 'Profiles', data=json.dumps(profile),
                            headers=self.generate_headers(token), verify=False)
    
    
    def get_user_profile(self, token):
        return requests.get(self.base_url + 'Profiles/user-profile', 
                            headers=self.generate_headers(token), verify=False)
    

    def delete_user_profile(self, token):
        return requests.delete(self.base_url + 'Profiles', 
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


def test_get_all_profiles(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if admin_token is not None:
            get_response = api_client.get_all_profiles(admin_token)
            assert get_response.status_code == 200

        get_response = api_client.get_all_profiles(None)
        assert get_response.status_code == 401


def test_post_user_profile_wrong(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            post_response = api_client.post_user_profile(user_token, profile_wrong_date)
            assert post_response.status_code == 400

            post_response = api_client.post_user_profile(user_token, profile_wrong_weight)
            assert post_response.status_code == 400


def test_post_user_profile(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            post_response = api_client.post_user_profile(user_token, profile)
            assert post_response.status_code == 200

        post_response = api_client.post_user_profile(None, profile)
        assert post_response.status_code == 401


def test_post_user_profile_twice(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            post_response = api_client.post_user_profile(user_token, profile)
            assert post_response.status_code == 400


def test_put_user_profile(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            put_response = api_client.put_user_profile(user_token, put_profile)
            assert put_response.status_code == 200

        put_response = api_client.put_user_profile(None, put_profile)
        assert put_response.status_code == 401


def test_get_user_profile(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            get_response = api_client.get_user_profile(user_token)
            assert get_response.status_code == 200

        get_response = api_client.get_user_profile(None)
        assert get_response.status_code == 401


def test_delete_user_profile(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            delete_response = api_client.delete_user_profile(user_token)
            assert delete_response.status_code == 200

        delete_response = api_client.delete_user_profile(None)
        assert delete_response.status_code == 401


def test_delete_user(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        delete_email_response = api_client.delete_user_by_email(dummy_user["email"], admin_token)
        assert delete_email_response.status_code == 200