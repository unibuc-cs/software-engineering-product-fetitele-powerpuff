import warnings
from datetime import datetime
from urllib3.exceptions import InsecureRequestWarning

profile = {
  "name": "dummy name",
  "birthdate": "2004-05-18",
  "weight": 60,
  "height": 175,
  "goal": 0
}

food = {
    "name": "pythonFood",
    "calories": 100,
    "carbohydrates": 14,
    "proteins": 11,
    "fats": 7
}

activity = {
    "name": "pythonActivity",
    "calories": 10
}

current_date = datetime.now()
formatted_date = current_date.strftime("%Y-%m-%d")
invalid_date = '2000-01-28'

day_water = {
    "date": formatted_date,
    "waterIntake": 300
}

day_water_invalid = {
    "date": formatted_date,
    "waterIntake": -300
}

day_water_invalid_date = {
    "date": invalid_date,
    "waterIntake": 300
}

day_food = {
    "date": formatted_date,
    "foodName": food["name"],
    "grams": 50
}

day_food_invalid = {
    "date": formatted_date,
    "foodName": 'wrong_name',
    "grams": 50
}

day_food_invalid_date = {
    "date": invalid_date,
    "foodName": food["name"],
    "grams": 50
}

day_food_different_grams = {
    "date": formatted_date,
    "foodName": food["name"],
    "grams": 80
}

day_activity = {
    "date": formatted_date,
    "activityName": activity["name"],
    "minutes": 20
}

day_activity_invalid = {
    "date": formatted_date,
    "activityName": 'wrong_name',
    "minutes": 20
}

day_activity_invalid_date = {
    "date": invalid_date,
    "activityName": activity["name"],
    "minutes": 20
}

day_activity_different_minutes = {
    "date": formatted_date,
    "activityName": activity["name"],
    "minutes": 30
}

def test_post_user_profile(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            post_response = api_client.post_user_profile(user_token, profile)
            assert post_response.status_code == 200


def test_get_days(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            get_response = api_client.get_days(admin_token)
            assert get_response.status_code == 200

        if user_token is not None:
            get_response = api_client.get_days(user_token)
            assert get_response.status_code == 403

        get_response = api_client.get_days(None)
        assert get_response.status_code == 401


def test_get_user_days(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_user_days(user_token)
            assert get_response.status_code == 200

            get_response = api_client.get_current_day(user_token)
            assert get_response.status_code == 200

            get_response = api_client.get_day_by_date(user_token, formatted_date)
            assert get_response.status_code == 200

            get_response = api_client.get_day_by_date(user_token, invalid_date)
            assert get_response.status_code == 404

        get_response = api_client.get_user_days(None)
        assert get_response.status_code == 401

        get_response = api_client.get_current_day(None)
        assert get_response.status_code == 401

        get_response = api_client.get_day_by_date(None, formatted_date)
        assert get_response.status_code == 401


def test_add_water_to_day(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            put_response = api_client.put_water_to_day(user_token, day_water_invalid)
            assert put_response.status_code == 400

            put_response = api_client.put_water_to_day(user_token, day_water_invalid_date)
            assert put_response.status_code == 404

            put_response = api_client.put_water_to_day(user_token, day_water)
            assert put_response.status_code == 200

            get_response = api_client.get_day_by_date(user_token, formatted_date)
            assert get_response.status_code == 200

            day_data = get_response.json()
            water_data = day_data.get('waterIntake', 0)

            assert water_data == 300

            put_response = api_client.put_water_to_day(user_token, day_water)
            assert put_response.status_code == 200

            get_response = api_client.get_day_by_date(user_token, formatted_date)
            assert get_response.status_code == 200

            day_data = get_response.json()
            water_data = day_data.get('waterIntake', 0)

            assert water_data == 600

        put_response = api_client.put_water_to_day(None, day_water)
        assert put_response.status_code == 401


def test_add_food_to_day(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None and user_token is not None:
            post_response = api_client.post_food(admin_token, food)
            assert post_response.status_code == 200

            put_response = api_client.put_food_to_day(user_token, day_food_invalid_date)
            assert put_response.status_code == 404

            put_response = api_client.put_food_to_day(user_token, day_food_invalid)
            assert put_response.status_code == 404

            put_response = api_client.put_food_to_day(user_token, day_food)
            assert put_response.status_code == 200

            get_response = api_client.get_day_by_date(user_token, formatted_date)
            assert get_response.status_code == 200

            day_data = get_response.json()
            day_foods = day_data.get('dayFoods', [])
            assert len(day_foods) == 1

            day_food_data = day_foods[0]
            assert day_food_data.get('grams') == day_food['grams']

            put_response = api_client.put_grams(user_token, day_food_different_grams)
            assert put_response.status_code == 200

            get_response = api_client.get_day_by_date(user_token, formatted_date)
            assert get_response.status_code == 200

            day_data = get_response.json()
            day_foods = day_data.get('dayFoods', [])
            assert len(day_foods) == 1
            
            day_food_data = day_foods[0]
            assert day_food_data.get('grams') == day_food_different_grams["grams"]
        
        put_response = api_client.put_food_to_day(None, day_food)
        assert put_response.status_code == 401

        put_response = api_client.put_grams(None, day_food_different_grams)
        assert put_response.status_code == 401


def test_add_activity_to_day(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None and user_token is not None:
            post_response = api_client.post_physical_activity(admin_token, activity)
            assert post_response.status_code == 200

            put_response = api_client.put_activity_to_day(user_token, day_activity)
            assert put_response.status_code == 200

            put_response = api_client.put_activity_to_day(user_token, day_activity_invalid_date)
            assert put_response.status_code == 404

            put_response = api_client.put_activity_to_day(user_token, day_activity_invalid)
            assert put_response.status_code == 404

            put_response = api_client.put_activity_to_day(user_token, day_activity)
            assert put_response.status_code == 200

            get_response = api_client.get_day_by_date(user_token, formatted_date)
            assert get_response.status_code == 200

            day_data = get_response.json()
            day_activities = day_data.get('dayPhysicalActivities', [])
            assert len(day_activities) == 1

            day_activity_data = day_activities[0]
            assert day_activity_data.get('minutes') == day_activity['minutes'] * 2

            put_response = api_client.put_minutes(user_token, day_activity_different_minutes)
            assert put_response.status_code == 200

            get_response = api_client.get_day_by_date(user_token, formatted_date)
            assert get_response.status_code == 200

            day_data = get_response.json()
            day_activities = day_data.get('dayPhysicalActivities', [])
            assert len(day_activities) == 1

            day_activity_data = day_activities[0]
            assert day_activity_data.get('minutes') == day_activity_different_minutes['minutes']

        put_response = api_client.put_activity_to_day(None, day_activity)
        assert put_response.status_code == 401
        
        put_response = api_client.put_minutes(None, day_activity_different_minutes)
        assert put_response.status_code == 401


def test_calories(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_day_food_calories(user_token, formatted_date)
            assert get_response.status_code == 200

            food_calories = get_response.json()
            assert food_calories == 80

            get_response = api_client.get_day_food_calories(user_token, invalid_date)
            assert get_response.status_code == 404

            get_response = api_client.get_day_activity_calories(user_token, formatted_date)
            assert get_response.status_code == 200

            activity_calories = get_response.json()
            assert activity_calories == 300

            get_response = api_client.get_day_activity_calories(user_token, invalid_date)
            assert get_response.status_code == 404

            get_response = api_client.get_day_food_calories_after_date(user_token, formatted_date)
            assert get_response.status_code == 200

            food_calories = get_response.json()
            assert food_calories[0].get('calories') == 80

            get_response = api_client.get_day_activity_calories_after_date(user_token, formatted_date)
            assert get_response.status_code == 200

            activity_calories = get_response.json()
            assert activity_calories[0].get('calories') == 300

            get_response = api_client.get_day_average_food_calories(user_token, formatted_date)
            assert get_response.status_code == 200

            food_calories = get_response.json()
            assert food_calories == 80

            get_response = api_client.get_day_average_activity_calories(user_token, formatted_date)
            assert get_response.status_code == 200

            activity_calories = get_response.json()
            assert activity_calories == 300

        get_response = api_client.get_day_food_calories(None, formatted_date)
        assert get_response.status_code == 401

        get_response = api_client.get_day_activity_calories(None, formatted_date)
        assert get_response.status_code == 401

        get_response = api_client.get_day_food_calories_after_date(None, formatted_date)
        assert get_response.status_code == 401

        get_response = api_client.get_day_activity_calories_after_date(None, formatted_date)
        assert get_response.status_code == 401

        get_response = api_client.get_day_average_food_calories(None, formatted_date)
        assert get_response.status_code == 401

        get_response = api_client.get_day_average_activity_calories(None, formatted_date)
        assert get_response.status_code == 401


def test_delete_from_day(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None and user_token is not None:
            delete_response = api_client.delete_food_from_day(user_token, invalid_date, food["name"])
            assert delete_response.status_code == 404

            delete_response = api_client.delete_food_from_day(user_token, formatted_date, 'wrong_name')
            assert delete_response.status_code == 404

            delete_response = api_client.delete_food_from_day(user_token, formatted_date, food["name"])
            assert delete_response.status_code == 200

            delete_response = api_client.delete_food_from_day(user_token, formatted_date, food["name"])
            assert delete_response.status_code == 400

            delete_response = api_client.delete_activity_from_day(user_token, invalid_date, activity["name"])
            assert delete_response.status_code == 404

            delete_response = api_client.delete_activity_from_day(user_token, formatted_date, 'wrong_name')
            assert delete_response.status_code == 404

            delete_response = api_client.delete_activity_from_day(user_token, formatted_date, activity["name"])
            assert delete_response.status_code == 200

            delete_response = api_client.delete_activity_from_day(user_token, formatted_date, activity["name"])
            assert delete_response.status_code == 400

            get_response = api_client.get_day_food_calories(user_token, formatted_date)
            assert get_response.status_code == 200

            food_calories = get_response.json()
            assert food_calories == 0

            get_response = api_client.get_day_activity_calories(user_token, formatted_date)
            assert get_response.status_code == 200

            activity_calories = get_response.json()
            assert activity_calories == 0

            delete_response = api_client.delete_food(admin_token, food["name"])
            assert delete_response.status_code == 200

            delete_response = api_client.delete_physical_activity(admin_token, activity["name"])
            assert delete_response.status_code == 200

        delete_response = api_client.delete_food_from_day(None, formatted_date, food["name"])
        assert delete_response.status_code == 401

        delete_response = api_client.delete_activity_from_day(None, formatted_date, activity["name"])
        assert delete_response.status_code == 401
