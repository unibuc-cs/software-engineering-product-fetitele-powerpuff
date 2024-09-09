from urllib.parse import quote
import warnings
import pytest
import requests
import json
from datetime import datetime

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

current_date = datetime.now()
formatted_date = current_date.strftime("%Y-%m-%d")

day_activity_id = 1
day_activity = {
    "date": formatted_date,
    "activityName": 'Activity1',
    "minutes": 20
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
    

    def post_user_profile(self, token, profile):
        return requests.post(self.base_url + 'Profiles', data=json.dumps(profile),
                            headers=self.generate_headers(token), verify=False)
    

    def delete_user_by_email(self, email, token):
        return requests.delete(self.base_url + 'ApplicationUser/dupa-email/' + email, 
                               headers=self.generate_headers(token), verify=False)
    
    
    def add_activity_to_day(self, day_activity, token):
        return requests.put(self.base_url + 'Days/add-activity', data=json.dumps(day_activity),
                            headers=self.generate_headers(token), verify=False)
    

    def get_current_day(self, formatted_date, token):
        return requests.get(self.base_url + 'Days/by-date', params={'date': formatted_date},
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


def test_post_user_profile(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            post_response = api_client.post_user_profile(user_token, profile)
            assert post_response.status_code == 200


def test_add_activity_to_day(api_client: APIClient):
    with warnings.catch_warnings():
        global admin_token
        global user_token
        global day_activity
        global formatted_date
        warnings.simplefilter("ignore", InsecureRequestWarning)

        add_new_activity_response = api_client.add_activity_to_day(day_activity, user_token)
        assert add_new_activity_response.status_code == 200


def test_add_already_existing_activity_to_day(api_client: APIClient):
    with warnings.catch_warnings():
        global admin_token
        global user_token
        global day_activity
        global formatted_date
        warnings.simplefilter("ignore", InsecureRequestWarning)

        add_activity_again_response = api_client.add_activity_to_day(day_activity, user_token)
        assert add_activity_again_response.status_code == 200


def test_added_activities_minutes(api_client: APIClient):
    with warnings.catch_warnings():
        global admin_token
        global user_token
        global day_activity
        global formatted_date
        warnings.simplefilter("ignore", InsecureRequestWarning)

        get_day_response = api_client.get_current_day(formatted_date, user_token)
        assert get_day_response.status_code == 200

        day_data = get_day_response.json()
        day_activities = day_data.get('dayPhysicalActivities', [])

        for activity in day_activities:
            if activity.get('physicalActivityId') == 1:
                minutes = activity.get('minutes')
        assert minutes == day_activity["minutes"] * 2


def test_delete_user(api_client: APIClient):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        delete_email_response = api_client.delete_user_by_email(dummy_user["email"], admin_token)
        assert delete_email_response.status_code == 200
        