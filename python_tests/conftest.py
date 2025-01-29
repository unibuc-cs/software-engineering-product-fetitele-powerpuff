import pytest
import warnings
import requests
import json
from urllib.parse import quote
from utils import register_user, login_user, delete_user
from urllib3.exceptions import InsecureRequestWarning

class APIClient:
    def __init__(self, base_url):
        self.base_url = base_url

    def generate_headers(self, token=None):
        headers = {
            'Content-Type': 'application/json',
        }
        if token:
            headers['Authorization'] = f'Bearer {token}'
        return headers

    def register(self, user):
        return requests.post(self.base_url + "Authentication/register-user", data=json.dumps(user), headers=self.generate_headers(), verify=False)

    def login(self, user):
        return requests.post(self.base_url + "Authentication/login", data=json.dumps(user), headers=self.generate_headers(), verify=False)
    
    
    def post_user_profile(self, token, profile):
        return requests.post(self.base_url + 'Profiles', data=json.dumps(profile), headers=self.generate_headers(token), verify=False)
    
    def get_days(self, token):
        return requests.get(self.base_url + 'Days', headers=self.generate_headers(token), verify=False)
    
    def get_user_days(self, token):
        return requests.get(self.base_url + 'Days/by-user', headers=self.generate_headers(token), verify=False)
    
    def get_current_day(self, token):
        return requests.get(self.base_url + 'Days/current-day', headers=self.generate_headers(token), verify=False)
    
    def get_day_by_date(self, token, date):
        return requests.get(self.base_url + 'Days/by-date', params={'date': date}, headers=self.generate_headers(token), verify=False)
    
    def get_day_by_date(self, token, date):
        return requests.get(self.base_url + 'Days/by-date', params={'date': date}, headers=self.generate_headers(token), verify=False)
    
    def get_day_food_calories(self, token, date):
        return requests.get(self.base_url + 'Days/food-calories', params={'date': date}, headers=self.generate_headers(token), verify=False)

    def get_day_activity_calories(self, token, date):
        return requests.get(self.base_url + 'Days/activity-calories', params={'date': date}, headers=self.generate_headers(token), verify=False)
    
    def get_day_food_calories_after_date(self, token, date):
        return requests.get(self.base_url + 'Days/food-calories-after-date', params={'date': date}, headers=self.generate_headers(token), verify=False)
    
    def get_day_activity_calories_after_date(self, token, date):
        return requests.get(self.base_url + 'Days/activity-calories-after-date', params={'date': date}, headers=self.generate_headers(token), verify=False)
    
    def get_day_average_food_calories(self, token, date):
        return requests.get(self.base_url + 'Days/average-food-calories', params={'date': date}, headers=self.generate_headers(token), verify=False)
    
    def get_day_average_activity_calories(self, token, date):
        return requests.get(self.base_url + 'Days/average-activity-calories', params={'date': date}, headers=self.generate_headers(token), verify=False)

    def put_water_to_day(self, token, water):
        return requests.put(self.base_url + 'Days/add-water', data=json.dumps(water), headers=self.generate_headers(token), verify=False)
    
    def put_food_to_day(self, token, day_food):
        return requests.put(self.base_url + 'Days/add-food', data=json.dumps(day_food), headers=self.generate_headers(token), verify=False)

    def put_activity_to_day(self, token, day_activity):
        return requests.put(self.base_url + 'Days/add-activity', data=json.dumps(day_activity), headers=self.generate_headers(token), verify=False)
    
    def put_grams(self, token, day_food):
        return requests.put(self.base_url + 'Days/change-grams', data=json.dumps(day_food), headers=self.generate_headers(token), verify=False)

    def put_minutes(self, token, day_activity):
        return requests.put(self.base_url + 'Days/change-minutes', data=json.dumps(day_activity), headers=self.generate_headers(token), verify=False)
    
    def delete_food_from_day(self, token, date, food_name):
        return requests.delete(self.base_url + 'Days/delete-food/' + date + '/' + food_name, headers=self.generate_headers(token), verify=False)
    
    def delete_activity_from_day(self, token, date, activity_name):
        return requests.delete(self.base_url + 'Days/delete-activity/' + date + '/' + activity_name, headers=self.generate_headers(token), verify=False)
    
    
    def get_all_users(self, token):
        return requests.get(self.base_url + 'ApplicationUser', headers=self.generate_headers(token), verify=False)
    
    def promote(self, email, token):
        encoded_email = quote(email)
        return requests.put(self.base_url + 'Authentication/promote/' + encoded_email, headers=self.generate_headers(token), verify=False)

    def delete_user_by_id(self, id, token):
        return requests.delete(self.base_url + 'ApplicationUser/dupa-id/' + str(id), headers=self.generate_headers(token), verify=False)
    
    def delete_user_by_email(self, email, token):
        return requests.delete(self.base_url + 'ApplicationUser/dupa-email/' + email, headers=self.generate_headers(token), verify=False)
    

    def get_article(self, token):
        return requests.get(self.base_url + 'Article', headers=self.generate_headers(token), verify=False)
    
    def post_article(self, token, article):
        return requests.post(self.base_url + 'Article', data=json.dumps(article), headers=self.generate_headers(token), verify=False)
    
    def delete_article(self, token, title):
        return requests.delete(self.base_url + 'Article/' + title, headers=self.generate_headers(token), verify=False)


    def get_food(self, token):
        return requests.get(self.base_url + 'Food', headers=self.generate_headers(token), verify=False)

    def get_food_admin(self, token):
        return requests.get(self.base_url + 'Food/for-admin', headers=self.generate_headers(token), verify=False)

    def get_food_name(self, token, name):
        return requests.get(self.base_url + 'Food/' + name, headers=self.generate_headers(token), verify=False)
    
    def post_food(self, token, food):
        return requests.post(self.base_url + 'Food', data=json.dumps(food), headers=self.generate_headers(token), verify=False)
    
    def delete_food(self, token, food_name):
        return requests.delete(self.base_url + 'Food/' + food_name, headers=self.generate_headers(token), verify=False)
    

    def get_physical_activity(self, token):
        return requests.get(self.base_url + 'PhysicalActivities', headers=self.generate_headers(token), verify=False)
    
    def get_physical_activity_admin(self, token):
        return requests.get(self.base_url + 'PhysicalActivities/for-admin', headers=self.generate_headers(token), verify=False)
    
    def post_physical_activity(self, token, activity):
        return requests.post(self.base_url + 'PhysicalActivities',  data=json.dumps(activity), headers=self.generate_headers(token), verify=False)
    
    def get_physical_activity_name(self, token, name):
        return requests.get(self.base_url + 'PhysicalActivities/' + name, headers=self.generate_headers(token), verify=False)
    
    def delete_physical_activity(self, token, name):
        return requests.delete(self.base_url + 'PhysicalActivities/' + name, headers=self.generate_headers(token), verify=False)
    

    def get_muscles(self, token):
        return requests.get(self.base_url + 'Muscles', headers=self.generate_headers(token), verify=False)
    
    def post_muscle(self, token, muscle):
        return requests.post(self.base_url + 'Muscles', data=json.dumps(muscle), headers=self.generate_headers(token), verify=False)
    
    def delete_muscle(self, token, name):
        return requests.delete(self.base_url + 'Muscles/' + name, headers=self.generate_headers(token), verify=False)
    
    def post_physical_activity(self, token, activity):
        return requests.post(self.base_url + 'PhysicalActivities', data=json.dumps(activity), headers=self.generate_headers(token), verify=False)

    def delete_physical_activity(self, token, name):
        return requests.delete(self.base_url + 'PhysicalActivities/' + name, headers=self.generate_headers(token), verify=False)
    
    def put_physical_activity_muscle(self, token, activity, muscle):
        return requests.put(self.base_url + 'PhysicalActivitiesMuscles/' + muscle + '/' + activity, headers=self.generate_headers(token), verify=False)
    
    def delete_physical_activity_muscle(self, token, activity, muscle):
        return requests.delete(self.base_url + 'PhysicalActivitiesMuscles/' + muscle + '/' + activity, headers=self.generate_headers(token), verify=False)
    

    def get_all_profiles(self, token):
        return requests.get(self.base_url + 'Profiles', headers=self.generate_headers(token), verify=False)
    
    def post_user_profile(self, token, profile):
        return requests.post(self.base_url + 'Profiles', data=json.dumps(profile), headers=self.generate_headers(token), verify=False)
    
    def put_user_profile(self, token, profile):
        return requests.put(self.base_url + 'Profiles', data=json.dumps(profile), headers=self.generate_headers(token), verify=False)
    
    def get_user_profile(self, token):
        return requests.get(self.base_url + 'Profiles/user-profile', headers=self.generate_headers(token), verify=False)

    def delete_user_profile(self, token):
        return requests.delete(self.base_url + 'Profiles', headers=self.generate_headers(token), verify=False)


    def get_recipe(self, token):
        return requests.get(self.base_url + 'Recipe', headers=self.generate_headers(token), verify=False)

    def get_recipe_admin(self, token):
        return requests.get(self.base_url + 'Recipe/for-admin', headers=self.generate_headers(token), verify=False)
    
    def post_recipe(self, token, recipe):
        return requests.post(self.base_url + 'Recipe', data=json.dumps(recipe), headers=self.generate_headers(token), verify=False)
    
    def delete_recipe(self, token, name):
        return requests.delete(self.base_url + 'Recipe/' + name, headers=self.generate_headers(token), verify=False)


    def post_recipe_food(self, token, recipe_name, food_name, grams):
        return requests.post(self.base_url + 'RecipeFood/' + recipe_name + '/' + food_name + '/' + str(grams), headers=self.generate_headers(token), verify=False)
    
    def delete_recipe_food(self, token, recipe_name, food_name):
        return requests.delete(self.base_url + f'RecipeFood?recipeName={recipe_name}&foodName={food_name}',  headers=self.generate_headers(token), verify=False)
    

    def get_tutorial(self, token):
        return requests.get(self.base_url + 'Tutorial', headers=self.generate_headers(token), verify=False)

    def post_tutorial(self, token, tutorial):
        return requests.post(self.base_url + 'Tutorial', data=json.dumps(tutorial), headers=self.generate_headers(token), verify=False)
    
    def delete_tutorial(self, token, title):
        return requests.delete(self.base_url + 'Tutorial/' + title, headers=self.generate_headers(token), verify=False)


    def get_requests(self, token):
        return requests.get(self.base_url + 'Request', headers=self.generate_headers(token), verify=False)
    
    def get_request(self, token, id):
        return requests.get(self.base_url + 'Request/' + id, headers=self.generate_headers(token), verify=False)
    
    def post_request(self, token, food_name):
        return requests.post(self.base_url + 'Request/' + food_name, headers=self.generate_headers(token), verify=False)
    
    def put_request(self, token, id):
        return requests.put(self.base_url + 'Request/' + id, headers=self.generate_headers(token), verify=False)
    
    def delete_request(self, token, id):
        return requests.delete(self.base_url + 'Request/' + id, headers=self.generate_headers(token), verify=False)
    


@pytest.fixture(scope="session")
def api_client():
    return APIClient('https://localhost:7094/api/')

@pytest.fixture(scope="session")
def setup_and_teardown(api_client):
    # Register and log in the user
    user = {
        "email": "dummy@example.com",
        "password": "Dummypa55!",
        "confirmPassword": "Dummypa55!"
    }
    admin = {
        "email": "admin1@example.com",
        "password": "P@r0la"
    }

    register_response = register_user(api_client, user)
    assert register_response.status_code == 200

    login_response = login_user(api_client, user)
    assert login_response.status_code == 200
    user_token = login_response.json()["token"]

    admin_login_response = login_user(api_client, admin)
    assert admin_login_response.status_code == 200
    admin_token = admin_login_response.json()["token"]

    yield user_token, admin_token, user, admin

    # Clean up
    delete_user(api_client, user['email'], admin_token)
