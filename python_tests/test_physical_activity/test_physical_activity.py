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

activity = {
    "name": "pythonActivity",
    "calories": 10
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
    
    
    def get_physical_activity(self, token):
        return requests.get(self.base_url + 'PhysicalActivities', 
                            headers=self.generate_headers(token), verify=False)
    

    def get_physical_activity_admin(self, token):
        return requests.get(self.base_url + 'PhysicalActivities/for-admin', 
                            headers=self.generate_headers(token), verify=False)
    

    def post_physical_activity(self, token, activity):
        return requests.post(self.base_url + 'PhysicalActivities', 
                             data=json.dumps(activity), headers=self.generate_headers(token), verify=False)
    

    def get_physical_activity_name(self, token, name):
        return requests.get(self.base_url + 'PhysicalActivities/' + name, 
                            headers=self.generate_headers(token), verify=False)
    

    def delete_physical_activity(self, token, name):
        return requests.delete(self.base_url + 'PhysicalActivities/' + name, 
                               headers=self.generate_headers(token), verify=False)
    

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


def test_get_physical_activity(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_physical_activity(user_token)
            assert get_response.status_code == 200
    
        if admin_token is not None:
            get_response = api_client.get_physical_activity_admin(admin_token)
            assert get_response.status_code == 200

        get_response = api_client.get_physical_activity(None)
        assert get_response.status_code == 401
        
        get_response = api_client.get_physical_activity_admin(None)
        assert get_response.status_code == 401


def test_post_physical_activity(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            post_response = api_client.post_physical_activity(admin_token, activity)
            assert post_response.status_code == 200

        post_response = api_client.post_physical_activity(None, activity)
        assert post_response.status_code == 401


def test_post_physical_activity_twice(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            post_response = api_client.post_physical_activity(admin_token, activity)
            assert post_response.status_code == 400

        post_response = api_client.post_physical_activity(None, activity)
        assert post_response.status_code == 401


def test_get_physical_activity_name(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_physical_activity_name(user_token, activity["name"])
            assert get_response.status_code == 200
    
            get_response = api_client.get_physical_activity_name(user_token, "wrong_name")
            assert get_response.status_code == 404

        get_response = api_client.get_physical_activity_name(None, activity["name"])
        assert get_response.status_code == 401


def test_delete_physical_activity(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            delete_response = api_client.delete_physical_activity(admin_token, activity["name"])
            assert delete_response.status_code == 200

        delete_response = api_client.delete_physical_activity(None, activity["name"])
        assert delete_response.status_code == 401



def test_delete_user(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        delete_email_response = api_client.delete_user_by_email(dummy_user["email"], admin_token)
        assert delete_email_response.status_code == 200