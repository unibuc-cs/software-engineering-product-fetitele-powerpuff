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

muscle = {
    "name": "pythonMuscle"
}

activity = {
    "name": "pythonActivity",
    "calories": 10
}

class APIClient:
    def __init__(self, base_url):
        self.base_url = base_url

    
    def generate_headers(self, token=None):
        headers = {'Content-Type': 'application/json'}
        if token:
            headers['Authorization'] = f'Bearer {token}'
        return headers
    

    def login(self, user):
        return requests.post(self.base_url + "Authentication/login", data=json.dumps(user), 
                            headers=self.generate_headers(), verify=False)
    

    def get_muscles(self, token):
        return requests.get(self.base_url + 'Muscles', 
                            headers=self.generate_headers(token), verify=False)
    

    def post_muscle(self, token, muscle):
        return requests.post(self.base_url + 'Muscles', data=json.dumps(muscle), 
                             headers=self.generate_headers(token), verify=False)
    
    def delete_muscle(self, token, name):
        return requests.delete(self.base_url + 'Muscles/' + name, 
                               headers=self.generate_headers(token), verify=False)
    

    def post_physical_activity(self, token, activity):
        return requests.post(self.base_url + 'PhysicalActivities', 
                             data=json.dumps(activity), headers=self.generate_headers(token), verify=False)
    

    def delete_physical_activity(self, token, name):
        return requests.delete(self.base_url + 'PhysicalActivities/' + name, 
                               headers=self.generate_headers(token), verify=False)
    

    def put_physical_activity_muscle(self, token, activity, muscle):
        return requests.put(self.base_url + 'PhysicalActivitiesMuscles/' + muscle + '/' + activity, 
                            headers=self.generate_headers(token), verify=False)
    

    def delete_physical_activity_muscle(self, token, activity, muscle):
        return requests.delete(self.base_url + 'PhysicalActivitiesMuscles/' + muscle + '/' + activity, 
                            headers=self.generate_headers(token), verify=False)
    

@pytest.fixture(scope="function")
def api_client():
    return APIClient('https://localhost:7094/api/')


def test_login(api_client: APIClient):
    with warnings.catch_warnings():
        global admin_token
        warnings.simplefilter("ignore", InsecureRequestWarning)

        login_response = api_client.login(admin)
        assert login_response.status_code == 200
        admin_token = login_response.json()['token']


def test_get_muscle(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            get_response = api_client.get_muscles(admin_token)
            assert get_response.status_code == 200

        get_response = api_client.get_muscles(None)
        assert get_response.status_code == 401



def test_post_muscle(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            post_response = api_client.post_muscle(admin_token, muscle)
            assert post_response.status_code == 200

        post_response = api_client.post_muscle(None, muscle)
        assert post_response.status_code == 401


def test_post_physical_activity(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            post_response = api_client.post_physical_activity(admin_token, activity)
            assert post_response.status_code == 200

        post_response = api_client.post_physical_activity(None, activity)
        assert post_response.status_code == 401


def test_put_physical_activity(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            put_response = api_client.put_physical_activity_muscle(admin_token, activity["name"], muscle["name"])
            assert put_response.status_code == 200

        put_response = api_client.put_physical_activity_muscle(None, activity["name"], muscle["name"])
        assert put_response.status_code == 401


def test_delete_physical_activity(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            delete_response = api_client.delete_physical_activity_muscle(admin_token, 
                                                                            activity["name"], muscle["name"])
            assert delete_response.status_code == 200

        delete_response = api_client.delete_physical_activity_muscle(None, activity["name"], muscle["name"])
        assert delete_response.status_code == 401


def test_delete_physical_activity(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            delete_response = api_client.delete_physical_activity(admin_token, activity["name"])
            assert delete_response.status_code == 200

        delete_response = api_client.delete_physical_activity(None, activity["name"])
        assert delete_response.status_code == 401


def test_delete_muscle(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            delete_response = api_client.delete_muscle(admin_token, muscle["name"])
            assert delete_response.status_code == 200

        delete_response = api_client.delete_muscle(None, muscle["name"])
        assert delete_response.status_code == 401
