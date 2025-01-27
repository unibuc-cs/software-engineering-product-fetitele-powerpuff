from urllib.parse import quote
import warnings
import pytest
import requests
import json
from datetime import datetime

from urllib3.exceptions import InsecureRequestWarning

profile = {
  "name": "dummy name",
  "birthdate": "2004-05-18",
  "weight": 60,
  "height": 175,
  "goal": 0
}

current_date = datetime.now()
formatted_date = current_date.strftime("%Y-%m-%d")

day_activity_id = 4008
day_activity = {
    "date": formatted_date,
    "activityName": 'Activity1',
    "minutes": 20
}


def test_post_user_profile(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            post_response = api_client.post_user_profile(user_token, profile)
            assert post_response.status_code == 200


def test_add_activity_to_day(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        global day_activity
        global formatted_date
        warnings.simplefilter("ignore", InsecureRequestWarning)

        add_new_activity_response = api_client.add_activity_to_day(day_activity, user_token)
        assert add_new_activity_response.status_code == 200


def test_add_already_existing_activity_to_day(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        global day_activity
        global formatted_date
        warnings.simplefilter("ignore", InsecureRequestWarning)

        add_activity_again_response = api_client.add_activity_to_day(day_activity, user_token)
        assert add_activity_again_response.status_code == 200


def test_added_activities_minutes(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        global day_activity
        global formatted_date
        warnings.simplefilter("ignore", InsecureRequestWarning)

        get_day_response = api_client.get_current_day(formatted_date, user_token)
        assert get_day_response.status_code == 200

        day_data = get_day_response.json()
        day_activities = day_data.get('dayPhysicalActivities', [])

        for activity in day_activities:
            if activity.get('physicalActivityId') == day_activity_id:
                minutes = activity.get('minutes')
        assert minutes == day_activity["minutes"] * 2
